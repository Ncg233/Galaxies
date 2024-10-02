using Galaxies.Core.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Render;
public interface IEnityRenderer<in T> where T : Entity
{
    void Render(T entity);
}
