using Galaxies.Core.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Galaxies.Client.Render;
public abstract class EntityRenderer<T> where T : Entity
{
    public abstract void Render(IntegrationRenderer renderer, T entity, int scale, Color colors);

    protected abstract string GetSpriteName();
}
