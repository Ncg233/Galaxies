using Galaxies.Client;
using Galaxies.Core.Networking;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Gen;
using Galaxies.Core.World.Particles;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using LiteNetLib;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World;
public abstract class AbstractWorld
{
    public int Width { get; private set; } = 400;
    public int Height { get; private set; } = 300;
    private readonly List<Entity> entities = [];
    private readonly List<AbstractPlayerEntity> players = [];
    private readonly LightManager lightManager;
    private readonly Dictionary<TileLayer, TileState[]> blockStateGrid = [];
    private readonly IWorldListener worldListener;
    //private readonly Dictionary<LightType, byte[]> lightGrid = [];
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
    public AbstractWorld(bool isClient, IWorldListener listener)
    {
        IsClient = isClient;
        worldListener = listener;
        seed = new Random().Next(-10000, 10000);
        rand = new Random(seed);
        lightManager = new LightManager(this, Width, Height);
        //lightGrid.Add(LightType.Sky, new byte[Width * Height]);
        //lightGrid.Add(LightType.Tile, new byte[Width * Height]);
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
        lightManager.InitLight();
        //InitLight();
    }
    public void Update(float dTime)
    {
        currnetTime = (currnetTime + dTime) % 1440;
        //currnetTime = 720;
        sunRotation = (float)(currnetTime / tutolTime * 2 * Math.PI - Math.PI / 2);
        skyLight = (float)(Math.Sin(sunRotation) + 1) / 2;

        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(dTime);
            if (entities[i].IsDead) {
                entities.RemoveAt(i); 
            }
        }
        //entities.ForEach(e => e.Update(dTime));

    }
    public void AddEntity(Entity entity)
    {
        if(entity is AbstractPlayerEntity player)
        	players.Add(player);
        entities.Add(entity);
    }
    public List<Entity> GetAllEntities()
    {
        return entities;
    }
    public List<T> GetEntitiesInArea<T>(HitBox area, Predicate<T> test) where T : Entity
    {
        List<T> entities = [];
        
        foreach (Entity entity in this.entities)
        {
            if (!entity.IsDead && entity is T castEntity)
            {
                if (test == null || test.Invoke(castEntity))
                {
                    if (castEntity.hitbox.Intersects(area))
                    {
                        entities.Add(castEntity);
                    }
                }
            }
        }
        return entities;
    }
    public List<AbstractPlayerEntity> GetAllPlayers(){
        return players;
    }
    public int GetTileIndex(int x, int y)
    {
        while (x > Width - 1)
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
            if (tilestate != null)
            {
                return tilestate;
            }
        }
        return AllTiles.Air.GetDefaultState();
    }
    public void SetTileState(TileLayer layer, int x, int y, TileState id)
    {
        if (IsInWorld(y) && id != null)
        {
            var grid = blockStateGrid.GetValueOrDefault(layer);
            grid[GetTileIndex(x, y)] = id;
            id.OnTilePlaced(this, null, x, y);
            if (!isGenerated)
            {
                lightManager.CauseLightUpdate(x, y);
                NotifyNeighbors(layer, x, y, id);
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
    public void NotifyNeighbors(TileLayer layer, int x, int y, TileState changeTile)
    {
        foreach (var dir in Direction.AdjacentIncludeNone)
        {
            NotifyNeighbor(layer, x + dir.X, y + dir.Y, changeTile);
        }

    }
    public void NotifyNeighbor(TileLayer layer, int x, int y, TileState changedTile)
    {
        var state = GetTileState(layer, x, y);
        worldListener.OnNotifyNeighbor(layer, x, y, state, changedTile);
        if (!IsClient)
        {
            
            state.OnNeighborChanged(this,layer, x, y, changedTile);
        }
    }

    public double GetGenSuerfaceHeight(TileLayer layer, int x)
    {
        return heightGen.GetHeight(layer, x);
    }
    public float GetSunRotation()
    {
        return sunRotation;
    }
    public float GetSkyLightModify(bool doMin)
    {
        if (doMin)
        {
            return Math.Min(1F, skyLight + 0.1F);
        }
        return skyLight;

    }
    public void SetSkyLight(int x, int y, byte light)
    {
        lightManager.SetSkyLight(x, y, light);
    }
    public byte GetSkyLight(int x, int y)
    {
        return lightManager.GetSkyLight(x, y);
    }
    public void SetTileLight(int x, int y, byte light)
    {
        lightManager.SetTileLight(x, y, light);
    }
    public byte GetTileLight(int x, int y)
    {
        return lightManager.GetTileLight(x, y);
    }
    public byte GetCombinedLight(int x, int y)
    {
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
    public bool IsInWorld(int y)
    {
        return y >= 0 && y < Height;
    }

    public void SetCurrentTime(float currentTime)
    {
        currnetTime = currentTime;
    }
    public void DestoryTile(int x, int y)
    {
        var tileState = GetTileState(TileLayer.Main, x, y);
        SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());

        tileState.OnDestroyed(this, x, y);
        if (tileState.ShouldRender())
        {
            for (int i = 0; i < Utils.Random.Next(2) + 3; i++)
            {
                float motionX = (float)Utils.Rand(0, 0.1f);
                float motionY = (float)Utils.Rand(0, 0.1f);
                float maxLife = Utils.Random.NextSingle() + 0.5f;
                AddParticle(new TileParticle(tileState, this, x + 0.5f, y + 0.5f, motionX, motionY, maxLife));
            }
        }
        
    }
    public void AddParticle(Particle particle)
    {
        Main.GetInstance().GetParticleManager().AddParticle(particle);
    }
    public abstract AbstractPlayerEntity CreatePlayer(NetPeer peer);

    public virtual void SaveData()
    {

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
}
