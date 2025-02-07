using Galaxies.Client.Render;
using Galaxies.Core.World;
using Microsoft.Xna.Framework;

namespace Galaxies.Core.World.Entities.Monsters;
public class Zombie : Monster
{
    public Zombie(AbstractWorld world) : base(world)
    {

    }

    public override void Render(IntegrationRenderer renderer, Color color)
    {
        throw new System.NotImplementedException();
    }
}