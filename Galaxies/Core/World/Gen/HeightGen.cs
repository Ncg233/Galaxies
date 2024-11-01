using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using System;

namespace Galaxies.Core.World.Gen;
public class HeightGen : AbstractWorldGen
{
    #region GENERATED

    private float noiseFreq = 0.04f;
    private float caveFreq = 0.05f;
    private float heightMult = 4f;
    private float heightAddition = 120;
    #endregion
    public HeightGen(int seed, Random random) : base(seed, random)
    {
    }

    
    public override void Generate(AbstractWorld world)
    {

    }

    public double GetHeight(TileLayer layer, int x)
    {
        NoiseGen.SetSeed(seed);
        return NoiseGen.Make2dNoise(x * noiseFreq, noiseFreq) * heightMult + heightAddition;
    }
}
