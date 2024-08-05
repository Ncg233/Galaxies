using Galaxias.Core.World.Chunks;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework.Input;
using SharpDX;
using System;

namespace Galaxias.Core.World.Gen;
public class TreeGen : IChunkGenerator
{
    Random random;
    public TreeGen(int seed) { 
        random = new Random(seed);
    }
    public void Generate(AbstractWorld world, Chunk applyChunk)
    {
        Random random = new Random();
        for (int x = applyChunk.chunkX * GameConstants.ChunkWidth; x < (applyChunk.chunkX + 1) * GameConstants.ChunkWidth; x++)
        {
            for (int y = 0; y < GameConstants.ChunkHeight; y++)
            {
                var state = applyChunk.GetTileState(TileLayer.Main, x, y - 1);
                if (random.NextFloat(0, 1) < 0.1f && state != null && state.GetTile() == AllTiles.GrassTile)
                {
                    var height = GetHeight(random);
                    for (int ly = y; ly < y + height; ly++)
                    {
                        applyChunk.SetTileState(TileLayer.Main, x, ly, AllTiles.Log.GetDefaultState());
                    }
                    world.SetTileState(TileLayer.Main, x, y + height, AllTiles.Leaves.GetDefaultState());
                    world.SetTileState(TileLayer.Main, x - 1, y + height, AllTiles.Leaves.GetDefaultState());
                    world.SetTileState(TileLayer.Main, x + 1, y + height, AllTiles.Leaves.GetDefaultState());
                    world.SetTileState(TileLayer.Main, x, y + height + 1, AllTiles.Leaves.GetDefaultState());
                }
            }
        }
    }
    private int GetHeight(Random random)
    {
        return random.Next(5, 8);
    }

}
