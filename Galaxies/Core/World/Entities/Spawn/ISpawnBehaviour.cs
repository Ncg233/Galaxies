using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Entities.Spawn;
public interface ISpawnBehaviour<out T> where T : Entity
{
    T CreateEntity(AbstractWorld world, float x, float y);
    float GetSpawnFrequency(AbstractWorld world);
}
