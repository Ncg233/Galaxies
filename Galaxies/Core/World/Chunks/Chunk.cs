using System;
using System.Collections.Generic;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Gen;

namespace Galaxies.Core.World.Chunks;
public class Chunk
{
    private readonly AbstractWorld world;
    private readonly Random rand;
    private readonly Dictionary<TileLayer, TileState[]> blockStateGrid = new();
    private readonly Dictionary<LightType, byte[]> lightGrid = new();
    public int chunkX { get; private set; }
    private static bool isGenerated;

    public Chunk(AbstractWorld world, int chunkX)
    {
        rand = new Random();
        this.chunkX = chunkX;
        lightGrid.Add(LightType.Sky, new byte[GameConstants.ChunkWidth * GameConstants.ChunkHeight]);
        lightGrid.Add(LightType.Tile, new byte[GameConstants.ChunkWidth * GameConstants.ChunkHeight]);
        this.world = world;
    }
    public void GenerateChunk(List<IChunkGenerator> generators)
    {
        isGenerated = true;
        foreach (IChunkGenerator generator in generators)
        {
            //generator.Generate(world, this);
        }
        InitLight();
        isGenerated = false;
    }
    public void SetTileState(TileLayer layer, int worldX, int worldY, TileState id)
    {
        SetTileStateInner(layer, worldX & 31, worldY, id);
    }

    private void SetTileStateInner(TileLayer layer, int gridX, int gridY, TileState id)
    {
        if (IsInWorld(gridY))
        {
            var grid = blockStateGrid.GetValueOrDefault(layer);
            if (grid == null)
            {
                grid = new TileState[GameConstants.ChunkWidth * GameConstants.ChunkHeight];
                blockStateGrid.Add(layer, grid);
            }

            grid[GetIndex(gridX, gridY)] = id;
            if (!isGenerated)
            {
                world.CauseLightUpdate(chunkX * GameConstants.ChunkWidth + gridX, gridY);
            }

        }
    }
    public TileState GetTileState(TileLayer layer, int worldX, int worldY)
    {
        return GetTileStateInner(layer, worldX & 31, worldY);
    }

    private TileState GetTileStateInner(TileLayer layer, int gridX, int gridY)
    {
        if (IsInWorld(gridY))
        {
            var grid = blockStateGrid.GetValueOrDefault(layer);
            if (grid == null)
            {
                return AllTiles.Air.GetDefaultState();
            }
            return grid[GetIndex(gridX, gridY)];
        }
        return AllTiles.Air.GetDefaultState();

    }
    private bool IsInWorld(int gridY)
    {
        return gridY >= 0 && gridY < GameConstants.ChunkHeight;
    }

    private static int GetIndex(int gridX, int gridY)
    {
        return gridY * GameConstants.ChunkWidth + gridX;
    }
    public byte GetSkyLight(int x, int y)
    {
        if (IsInWorld(y))
        {
            return lightGrid.GetValueOrDefault(LightType.Sky)[GetIndex(x & 31, y)];
        }
        return GameConstants.MaxLight;
    }
    public void SetSkyLight(int x, int y, byte light)
    {
        if (IsInWorld(y))
        {
            lightGrid.GetValueOrDefault(LightType.Sky)[GetIndex(x & 31, y)] = light;
        }
    }
    public byte GetTileLight(int x, int y)
    {
        if (IsInWorld(y))
        {
            return lightGrid.GetValueOrDefault(LightType.Tile)[GetIndex(x & 31, y)];
        }
        return GameConstants.MaxLight;
    }
    public void SetTileLight(int x, int y, byte light)
    {
        if (IsInWorld(y))
        {
            lightGrid.GetValueOrDefault(LightType.Tile)[GetIndex(x & 31, y)] = light;
        }
    }
    public byte GetCombinedLight(int x, int y)
    {
        byte skyLight = (byte)(GetSkyLight(x, y) * world.GetSkyLightModify(true));
        return (byte)Math.Min(GameConstants.MaxLight, skyLight + GetTileLight(x, y));
    }
    public void InitLight()
    {
        for (int x = GameConstants.ChunkWidth - 1; x >= 0; x--)
        {
            for (int y = GameConstants.ChunkHeight - 1; y >= 0; y--)
            {
                byte light = world.CalcLight(chunkX * GameConstants.ChunkWidth + x, y, true);
                SetSkyLight(x, y, light);
            }
        }
        for (int x = 0; x < GameConstants.ChunkWidth; x++)
        {
            for (int y = 0; y < GameConstants.ChunkHeight; y++)
            {
                byte light = world.CalcLight(chunkX * GameConstants.ChunkWidth + x, y, true);
                SetSkyLight(x, y, light);
            }
        }
    }
}
