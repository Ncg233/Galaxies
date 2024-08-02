using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Galaxias.Client.Render;
public class EntityRenderer
{
    public void Render(IntegrationRenderer renderer, float renderX, float renderY, float width, float height, Entity entity, int scale, Color colors)
    {
        renderer.Draw("Assets/Textures/Entities/player", renderX - width * scale / 2, renderY - height * scale, 1, 1, colors,
            effects: entity.direction == Util.Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

    }
}
