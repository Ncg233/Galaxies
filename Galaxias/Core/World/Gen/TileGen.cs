using Galaxias.Core.World.Chunks;
using Galaxias.Core.World.Tiles;
using System;

namespace Galaxias.Core.World.Gen;
public class TileGen : IChunkGenerator
{
    private readonly Random rand = new Random();
    #region GENERATED

    private float noiseFreq = 0.04f;
    private float caveFreq = 0.05f;
    private float seed = 10;
    private float heightMult = 10f;
    private float heightAddition = 120;

    #endregion
    public void Generate(AbstractWorld world ,Chunk applyChunk)
    {
        for (int x = 0; x < GameConstants.ChunkWidth; x++)
        {
            double height = world.GetGenSuerfaceHeight(TileLayer.Main, applyChunk.chunkX * GameConstants.ChunkWidth + x);
            for (int y = 0; y < GameConstants.ChunkHeight; y++)
            {
                double v = NoiseGen.Make2dNoise((applyChunk.chunkX * GameConstants.ChunkWidth + x + seed) * caveFreq, (y + seed) * caveFreq);
        
                if (y < height)
                {
                    //this will be used to generate cave and it will move to CaveGen
                    if(v < 0.2f && y < 80)
                    {
                        applyChunk.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
                    }
                    else
                    {
                        if (height - (rand.NextInt64() % 2 == 0 ? 10 : 9) >= y)
                        {
                            applyChunk.SetTileState(TileLayer.Main, x, y, AllTiles.Stone.GetDefaultState());
                        }
                        else
                        {
                            applyChunk.SetTileState(TileLayer.Main, x, y, AllTiles.Dirt.GetDefaultState());
                        }
                    }
                    
                }
                else if(y < height + 1)
                {
                    applyChunk.SetTileState(TileLayer.Main, x, y, AllTiles.GrassTile.GetDefaultState());
                }
                else {
                    applyChunk.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
                }
            }
        }
    }
}
