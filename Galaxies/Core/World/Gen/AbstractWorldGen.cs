using System;

namespace Galaxies.Core.World.Gen;
public abstract class AbstractWorldGen : IWorldGenerator
{
    protected readonly int seed;
    protected readonly Random random;
    public AbstractWorldGen(int seed, Random random)
    {
        this.seed = seed;
        this.random = random;
    }
    public abstract void Generate(AbstractWorld world);
}
