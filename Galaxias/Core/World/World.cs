using Galaxias.Core.World.Chunks;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Gen;
using Galaxias.Core.World.Particle;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Galaxias.Core.World;
public class World
{
    public int width { get; private set; } = 192;
    public int height { get; private set; } = 256;
    private readonly List<Entity> entities = [];
    private readonly List<ParticleType> particles = [];
    private readonly Dictionary<TileLayer, TileState[]> blockStateGrid = [];
    private readonly Dictionary<LightType, byte[]> lightGrid = [];
    private readonly List<IChunkGenerator> generators;
    private int tutolTime = 1440;
    public float currnetTime { get; private set; } = 8 * 60;//8:00
    private float sunRotation, skyLight;
    private HeightGen heightGen;
    private int seed;
    private Random rand;
    public bool IsClient { get; private set; }
    public World(bool isClient)
    {
        IsClient = isClient; 
        seed = new Random().Next(-10000, 10000);
        rand = new Random(seed);
        
        heightGen = new HeightGen(seed, rand); 

        lightGrid.Add(LightType.Sky, new byte[width * height]);
        lightGrid.Add(LightType.Tile, new byte[width * height]);
        blockStateGrid.Add(TileLayer.Background, new TileState[width * height]);
        blockStateGrid.Add(TileLayer.Main, new TileState[width * height]);
        if (!isClient)
        {
            generators = [new TileGen(seed, rand), new TreeGen(seed, rand)];
            foreach (var generator in generators)
            {
                generator.Generate(this);
            }
            InitLight();
        }

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
        while(x > width -1)
        {
            x -= width;
        }
        while (x < 0)
        {
            x += width;
        }
        return y * width + x;
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
        }
    }
    private int GetChunkX(int worldX)
    {
        while (worldX > width / 2 - 1)
        {
            worldX -= width;
        }
        while (worldX < -width / 2)
        {
        
            worldX += width;
        
        }
        return Utils.ToGridPos(worldX);
    }
    public double GetGenSuerfaceHeight(TileLayer layer ,int x)
    {
        return heightGen.GetHeight(layer, x);
    }
    public void InitLight()
    {
        for (int x = width - 1; x >= 0; x--)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                byte light = CalcLight(x, y, true);
                SetSkyLight(x, y, light);
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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
            (lightAround[0] + lightAround[4] + lightAround[5] + lightAround[6]) / 4
        ];
        return light;
    }
    public void AddParticle(ParticleType particle){
        particles.Add(particle);
    }

    public int GetWidth()
    {
        return width;
    }
    public bool IsInWorld(int y)
    {
        return y >= 0 && y < height;
    }
}
