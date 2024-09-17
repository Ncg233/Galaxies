using Galaxias.Core.World.Entities;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Galaxias.Client.Render;
public abstract class EntityRenderer<T> where T : Entity
{
    public abstract void Render(IntegrationRenderer renderer, T entity, int scale, Color colors);
    
    protected abstract string GetSpriteName();
}
