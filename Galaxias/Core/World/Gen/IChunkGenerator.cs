using Galaxias.Core.World.Chunks;

namespace Galaxias.Core.World.Gen;
public interface IChunkGenerator
{

    public void Generate(World world, Chunk applyChunk);
}
