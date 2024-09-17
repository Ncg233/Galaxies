using Galaxias.Client;
using Galaxias.Core.Networking;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Gen;
using Galaxias.Core.World.Particles;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using LiteNetLib;
using SharpDX;
using System;
using System.Collections.Generic;

namespace Galaxias.Core.World;
public abstract class AbstractWorld
{
    public int Width { get; private set; } = 192;
    public int Height { get; private set; } = 256;
    private readonly List<Entity> entities = [];
    
    private readonly Dictionary<TileLayer, TileState[]> blockStateGrid = [];
    private readonly Dictionary<LightType, byte[]> lightGrid = [];
    #region SERVER_ONLY

    protected HeightGen heightGen;
    protected readonly List<IChunkGenerator> generators;

    #endregion
    private int tutolTime = 1440;
    public float currnetTime { get; protected set; } = 8 * 60;//8:00
    private float sunRotation, skyLight;
    
    protected int seed;
    protected Random rand;
    public bool IsClient { get; private set; }
    protected bool isGenerated = true;
    public AbstractWorld(bool isClient)
    {
        IsClient = isClient; 
        seed = new Random().Next(-10000, 10000);
        rand = new Random(seed);
 
        lightGrid.Add(LightType.Sky, new byte[Width * Height]);
        lightGrid.Add(LightType.Tile, new byte[Width * Height]);
        blockStateGrid.Add(TileLayer.Background, new TileState[Width * Height]);
        blockStateGrid.Add(TileLayer.Main, new TileState[Width * Height]);
        if (!isClient)
        {
            heightGen = new HeightGen(seed, rand);
            generators = [new TileGen(seed, rand), new TreeGen(seed, rand)];
        }

    }
    public void Generate()
    {
        isGenerated = true;
        foreach (var generator in generators)
        {
            generator.Generate(this);
        }
        isGenerated = false;
        InitLight();
    }
    public void Update(float dTime)
    {
        currnetTime = (currnetTime + dTime) % 1440;
        sunRotation = (float)(currnetTime / tutolTime * 2 * Math.PI - Math.PI / 2);
        skyLight = (float)(Math.Sin(sunRotation) + 1) / 2;
        
        entities.ForEach(e => e.Update(dTime));
        
    }
    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }
    public List<Entity> GetAllEntities()
    {
        return entities;
    }
    private int GetTileIndex(int x, int y)
    {
        while(x > Width -1)
        {
            x -= Width;
        }
        while (x < 0)
        {
            x += Width;
        }
        return y * Width + x;
    }

    public TileState GetTileState(TileLayer layer, int x, int y)
    {
        if (IsInWorld(y))
        {
            var grid = blockStateGrid.GetValueOrDefault(layer);
            var tilestate = grid[GetTileIndex(x, y)];
            if(tilestate != null)
            {
                return tilestate;
            }
        }
        return AllTiles.Air.GetDefaultState();
    }
    public void SetTileState(TileLayer layer, int x, int y, TileState id)
    {
        if (IsInWorld(y))
        {
            var grid = blockStateGrid.GetValueOrDefault(layer);
            grid[GetTileIndex(x, y)] = id;
            if (!isGenerated) { 
                CauseLightUpdate(x, y);
                NotifyNeighbors(x, y, id.GetTile());
                if (!IsClient)
                {
                    NetPlayManager.SendToAllClients(new S2CTileChangePacket(x, y, id));
                }
            }

            
        }
    }
    //private int GetChunkX(int worldX)
    //{
    //    while (worldX > Width / 2 - 1)
    //    {
    //        worldX -= Width;
    //    }
    //    while (worldX < -Width / 2)
    //    {
    //    
    //        worldX += Width;
    //    
    //    }
    //    return Utils.ToGridPos(worldX);
    //}
    public void NotifyNeighbors(int x, int y, Tile tile)
    {
        foreach (var dir in Direction.Adjacent)
        {
            NotifyNeighbor(x + dir.X, y + dir.Y, tile);
        }
        
    }
    public void NotifyNeighbor(int x, int y, Tile changedTile)
    {
        if (!IsClient)
        {
            var state = GetTileState(TileLayer.Main, x, y);
            state.OnNeighborChanged(this, x, y, changedTile);
        }   
    }

    public double GetGenSuerfaceHeight(TileLayer layer ,int x)
    {
        return heightGen.GetHeight(layer, x);
    }
    public void InitLight()
    {
        for (int x = Width - 1; x >= 0; x--)
        {
            for (int y = Height - 1; y >= 0; y--)
            {
                byte light = CalcLight(x, y, true);
                SetSkyLight(x, y, light);
            }
        }
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                byte light = CalcLight(x, y, true);
                SetSkyLight(x, y, light);
            }
        }
    }
    public void CauseLightUpdate(int x, int y)
    {
        foreach (Direction direction in Direction.SurroundingIncludNone)
        {
            int dirX = x + direction.X;
            int dirY = y + direction.Y;

            if (IsInWorld(dirY)){
                bool change = false;

                byte skylightThere = GetSkyLight(dirX, dirY);
                byte calcedSkylight = CalcLight(dirX, dirY, true);
                if (calcedSkylight != skylightThere)
                {
                    SetSkyLight(dirX, dirY, calcedSkylight);
                    change = true;
                }

                byte tilelightThere = GetTileLight(dirX, dirY);
                byte calcedTilelight = CalcLight(dirX, dirY, false);
                if (calcedTilelight != tilelightThere)
                {
                    SetTileLight(dirX, dirY, calcedTilelight);
                    change = true;
                }
                if (change)
                {
                    CauseLightUpdate(dirX, dirY);
                }
            }
        }

    }
    public byte CalcLight(int x, int y, bool isSky)
    {
        byte maxLight = 0;

        foreach (Direction direction in Direction.Surrounding)
        {
            int dirX = x + direction.X;
            int dirY = y + direction.Y;

            if (IsInWorld(dirY))
            {
                byte light = isSky ? GetSkyLight(dirX, dirY) : GetTileLight(dirX, dirY);
                if (light > maxLight)
                {
                    maxLight = light;
                }
            }


        }

        maxLight = (byte)(maxLight * GetTileModifier(x, y, isSky));

        byte emitted = GetTileLight(x, y, isSky);
        if (emitted > maxLight)
        {
            maxLight = emitted;
        }

        return maxLight;
    }
    public byte GetTileLight(int x, int y, bool isSky)
    {
        int highestLight = 0;
        bool nonAir = false;

        foreach(TileLayer layer in Utils.GetAllLayers())
        {
            TileState tilestate = GetTileState(layer, x, y);
            if (!tilestate.IsAir())
            {
                int light = tilestate.GetLight();
                if (light > highestLight)
                {
                    highestLight = light;
                }

                nonAir = true;
            }
        }
        

        if (nonAir)
        {
            if (!isSky)
            {
                return (byte)highestLight;
            }
        }
        else if (isSky)
        {
            return GameConstants.MaxLight;
        }
        return 0;
    }

    public float GetTileModifier(int x, int y, bool isSky)
    {
        float smallestMod = 1F;
        bool nonAir = false;


        foreach (TileLayer layer in Utils.GetAllLayers())
        {

            Tile tile = GetTileState(layer, x, y).GetTile();
            if (!tile.IsAir())
            {
                float mod = tile.GetTranslucentModifier(this, x, y, layer, isSky);
                if (mod < smallestMod)
                {
                    smallestMod = mod;
                }

                nonAir = true;
            }
        }
        if (nonAir)
        {
            return smallestMod;
        }
        else
        {
            return isSky ? 1F : 0.8F;
        }
    }
    //public bool IsChunkLoaded(int dirX)
    //{
    //    return chunksLookup.GetValueOrDefault(GetChunkX(dirX)) != null;
    //}
    public float GetSunRotation()
    {
        return sunRotation;
    }
    public float GetSkyLightModify(bool doMin) {
        if (doMin)
        {
            return Math.Min(1F, skyLight + 0.15F);
        }
        return skyLight;

    }
    public void SetSkyLight(int x, int y, byte light)
    {
        if (IsInWorld(y))
        {
            var grid = lightGrid[LightType.Sky];
            grid[GetTileIndex(x, y)] = light;
        }
    }
    public byte GetSkyLight(int x, int y)
    {
        if (IsInWorld(y))
        {
            var grid = lightGrid[LightType.Sky];
            return grid[GetTileIndex(x, y)];
        }
        return GameConstants.MaxLight;
    }
    public void SetTileLight(int x, int y, byte light)
    {
        if (IsInWorld(y))
        {
            var grid = lightGrid[LightType.Tile];
            grid[GetTileIndex(x, y)] = light;
        }
    }
    public byte GetTileLight(int x, int y)
    {
        if (IsInWorld(y))
        {
            var grid = lightGrid[LightType.Tile];
            return grid[GetTileIndex(x, y)];
        }
        return GameConstants.MaxLight;
    }
    public byte GetCombinedLight(int x, int y){
        byte skyLight = (byte)(GetSkyLight(x, y) * GetSkyLightModify(true));
        return (byte)Math.Min(GameConstants.MaxLight, skyLight + GetTileLight(x, y));
    }
    public int[] GetInterpolateLight(int x, int y)
    {
        Direction[] dirs = Direction.SurroundingIncludNone;
        byte[] lightAround = new byte[dirs.Length];
        for (int i = 0; i < dirs.Length; i++)
        {
            Direction dir = dirs[i];
            if (true)
            {
                lightAround[i] = GetCombinedLight(x + dir.X, y + dir.Y);
            }
        }

        int[] light =
        [
            (lightAround[0] + lightAround[8] + lightAround[1] + lightAround[2]) / 4,
            (lightAround[0] + lightAround[6] + lightAround[7] + lightAround[8]) / 4,
            (lightAround[0] + lightAround[2] + lightAround[3] + lightAround[4]) / 4,
            (lightAround[0] + lightAround[4] + lightAround[5] + lightAround[6]) / 4,
        ];
        return light;
    }

    public int GetWidth()
    {
        return Width;
    }
    public bool IsInWorld(int y)
    {
        return y >= 0 && y < Height;
    }
    public void WriteTileData(out int[] tileData, out byte[] skyLight, out byte[] tileLight)
    {
        int size = Width * Height;
        tileData = new int[2 * size];
        skyLight = new byte[size];
        tileLight = new byte[size];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                tileData[y * Width + x] = Tile.TileStateId.Get(GetTileState(TileLayer.Main, x, y));
                tileData[y * Width + x + size] = Tile.TileStateId.Get(GetTileState(TileLayer.Background, x, y));
                skyLight[y * Width + x] = GetSkyLight(x, y);
                tileLight[y * Width + x] = GetTileLight(x, y);
            }
        }
    }
    public void ReadTileData(int[] tileData, byte[] skyLight, byte[] tileLight)
    {
        int size = Height * Width;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                
                SetTileState(TileLayer.Main, x, y, Tile.TileStateId.Get(tileData[y * Width + x]));
                SetTileState(TileLayer.Background, x, y, Tile.TileStateId.Get(tileData[size + y * Width + x]));
                SetSkyLight(x, y, skyLight[y * Width + x]);
                SetTileLight(x, y, tileLight[y * Width + x]);
                
            }
        }
        isGenerated = false;
        //isLoaded = true;
    }
    public void SetCurrentTime(float currentTime)
    {
        currnetTime = currentTime;
    }

    public virtual void SaveData()
    {
        
    }

    public void DestoryTile(int x, int y)
    {
        for (int i = 0; i < Utils.Random.Next(5) + 5; i++)
        {
            float motionX = (float)Utils.Rand(0, 0.1f);
            float motionY = (float)Utils.Rand(0, 0.1f);
            float maxLife = Utils.Random.NextFloat(1, 3);
            AddParticle(new TileParticle(GetTileState(TileLayer.Main, x, y),this, x + 0.5f, y + 0.5f, motionX, motionY, maxLife));
        }
        SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
    }
    public void AddParticle(Particle particle)
    {
        Main.GetInstance().GetParticleManager().AddParticle(particle);
    }
    public abstract AbstractPlayerEntity CreatePlayer(NetPeer peer);
}
