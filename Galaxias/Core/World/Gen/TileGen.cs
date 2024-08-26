using Galaxias.Core.World.Chunks;
using Galaxias.Core.World.Tiles;
using System;

namespace Galaxias.Core.World.Gen;
public class TileGen : AbstractChunkGen
{
    #region GENERATED

    private float noiseFreq = 0.04f;
    private float caveFreq = 0.05f;
    private float heightMult = 10f;
    private float heightAddition = 120;

    public TileGen(int seed, Random random) : base(seed, random)
    {
    }

    #endregion
    public override void Generate(AbstractWorld world)
    {
        for (int x = 0; x < world.GetWidth(); x++)
        {
            double height = world.GetGenSuerfaceHeight(TileLayer.Main, x);
            for (int y = 0; y < world.Height; y++)
            {
                double v = NoiseGen.Make2dNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
        
                if (y < height)
                {
                    //this will be used to generate cave and it will move to CaveGen
                    if(v < 0.2f && y < 80)
                    {
                        world.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
                    }
                    else
                    {
                        if (height - (random.NextInt64() % 2 == 0 ? 10 : 9) >= y)
                        {
                            world.SetTileState(TileLayer.Main, x, y, AllTiles.Stone.GetDefaultState());
                        }
                        else
                        {
                            world.SetTileState(TileLayer.Main, x, y, AllTiles.Dirt.GetDefaultState());
                        }
                    }
                    world.SetTileState(TileLayer.Background, x, y, AllTiles.Dirt.GetDefaultState());
                    
                }
                else if(y < height + 1)
                {
                    world.SetTileState(TileLayer.Main, x, y, AllTiles.GrassTile.GetDefaultState());
                    world.SetTileState(TileLayer.Background, x, y, AllTiles.Air.GetDefaultState());
                }
                else {
                    world.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
                    world.SetTileState(TileLayer.Background, x, y, AllTiles.Air.GetDefaultState());
                }
                
            }
        }
    }
}
