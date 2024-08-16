using Galaxias.Core.World.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.World.Gen;
public abstract class AbstractChunkGen : IChunkGenerator
{
    protected readonly int seed;
    protected readonly Random random;
    public AbstractChunkGen(int seed, Random random)
    {
        this.seed = seed;
        this.random = random;
    }
    public abstract void Generate(World world, Chunk applyChunk);
}
