using Galaxias.Core.World.Entities;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Client.Code.Render;
public class EntityRenderer
{
    public void Render(IntegrationRenderer renderer, float renderX, float renderY, float width, float height, Entity entity, int scale, Color colors)
    {
        renderer.Draw("Assets/Textures/Entities/player", renderX - width * scale / 2, renderY - height * scale, colors,
            effects: entity.direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

    }
}
