using Galaxias.Core.World.Chunks;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Gen;
using Galaxias.Core.World.Particle;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using System;
using System.Collections.Generic;

namespace Galaxias.Core.World;
public class World
{
    private readonly int width = 192;
    private readonly int height;
    private readonly List<Entity> entities = [];
    private readonly List<ParticleType> particles = [];
    private readonly Dictionary<int, Chunk> chunksLookup = [];
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

        generators = [new TileGen(seed, rand), new TreeGen(seed, rand)];
    }
    public Chunk GetChunk(int chunkX)
    {
        var chunk = chunksLookup.GetValueOrDefault(chunkX);
        
        if (chunk == null)
        {
            Console.WriteLine("generate new chunk x:" + chunkX);
            chunk = LoadChunk(chunkX);
            chunk.GenerateChunk(generators);
        }
        return chunk;

    }
    public Chunk LoadChunk(int chunkX)
    {
        Chunk chunk = new (this, chunkX);
        chunksLookup[chunkX] = chunk;

        return chunk;
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

    public TileState GetTileState(TileLayer layer, int x, int y)
    {
        return GetChunk(GetChunkX(x)).GetTileState(layer, x, y);
    }
    public void SetTileState(TileLayer layer, int x, int y, TileState id)
    {
        GetChunk(GetChunkX(x)).SetTileState(layer, x, y, id);
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
    public void CauseLightUpdate(int x, int y)
    {
        foreach (Direction direction in Direction.SurroundingIncludNone)
        {
            int dirX = x + direction.X;
            int dirY = y + direction.Y;

            if (IsChunkLoaded(dirX) && IsInWorld(dirY)){
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

            if (IsChunkLoaded(dirX) && IsInWorld(dirY))
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
                float mod = tile.GetTranslucentModifier(this, x, y, TileLayer.Main, isSky);
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
    public bool IsChunkLoaded(int dirX)
    {
        return chunksLookup.GetValueOrDefault(GetChunkX(dirX)) != null;
    }
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
        GetChunk(GetChunkX(x)).SetSkyLight(x, y, light);
    }
    public byte GetSkyLight(int x, int y)
    {
        return GetChunk(GetChunkX(x)).GetSkyLight(x, y);
    }
    public void SetTileLight(int x, int y, byte light)
    {
        GetChunk(GetChunkX(x)).SetTileLight(x, y, light);
    }
    public byte GetTileLight(int x, int y)
    {
        return GetChunk(GetChunkX(x)).GetTileLight(x, y);
    }
    public byte GetCombinedLight(int x, int y){
        return GetChunk(GetChunkX(x)).GetCombinedLight(x, y);
    }
    public int[] GetInterpolateLight(int x, int y)
    {
        Direction[] dirs = Direction.SurroundingIncludNone;
        byte[] lightAround = new byte[dirs.Length];
        for (int i = 0; i < dirs.Length; i++)
        {
            Direction dir = dirs[i];
            if (IsChunkLoaded(x + dir.X))
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
    public void addParticle(ParticleType particle){
        particles.Add(particle);
    }

    public int GetWidth()
    {
        return width;
    }
    public bool IsInWorld(int y)
    {
        return y >= 0 && y < GameConstants.ChunkHeight;
    }
}
