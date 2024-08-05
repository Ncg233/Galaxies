using Galaxias.Core.World.Entities;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ClientGalaxias.Client.Render;
public class EntityRenderer
{
    public void Render(IntegrationRenderer renderer, float renderX, float renderY, float width, float height, Entity entity, int scale, Color colors)
    {
        renderer.Draw("Textures/Entities/player", renderX - width * scale / 2, renderY - height * scale, colors,
            effects: entity.direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

    }
}
