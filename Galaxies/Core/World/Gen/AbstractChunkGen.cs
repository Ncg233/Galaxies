using System;

namespace Galaxies.Core.World.Gen;
public abstract class AbstractChunkGen : IChunkGenerator
{
    protected readonly int seed;
    protected readonly Random random;
    public AbstractChunkGen(int seed, Random random)
    {
        this.seed = seed;
        this.random = random;
    }
    public abstract void Generate(AbstractWorld world);
}
