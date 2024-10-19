using Galaxies.Core.World;
using Galaxies.Core.World.Entities;

namespace Galaxies.Core.World.Entities.Monsters;
public class Zombie : Monster
{
    public Zombie(AbstractWorld world) : base(AllEntityTypes.Zombie, world)
    {

    }
}