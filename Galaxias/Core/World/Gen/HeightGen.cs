using Galaxias.Core.World.Chunks;
using Galaxias.Core.World.Tiles;
using System;

namespace Galaxias.Core.World.Gen;
public class HeightGen : AbstractChunkGen
{
    #region GENERATED

    private float noiseFreq = 0.04f;
    private float caveFreq = 0.05f;
    private float heightMult = 4f;
    private float heightAddition = 120;
    public HeightGen(int seed, Random random) : base(seed, random) {
    }

    #endregion
    public override void Generate(AbstractWorld world, Chunk applyChunk)
    {
        
    }

    public double GetHeight(TileLayer layer, int x)
    {
        return NoiseGen.Make2dNoise((x + seed) * noiseFreq, seed * noiseFreq) * heightMult + heightAddition;
    }
}
