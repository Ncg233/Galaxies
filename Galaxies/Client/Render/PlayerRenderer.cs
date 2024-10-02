using Galaxies.Core.World.Entities;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Galaxies.Client.Render;
public class PlayerRenderer : EntityRenderer<AbstractPlayerEntity>
{
    public override void Render(IntegrationRenderer renderer, AbstractPlayerEntity player, int scale, Color colors)
    {
        var x = player.GetRenderX();
        var y = player.GetRenderY();
        var width = 16;
        var height = 32;
        bool isTurn = player.direction == Direction.Left;
        //BODY
        renderer.Draw(GetSpriteName(), x, y,
            width / 2f, height, width, height, colors,
            effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        //HELD ITEM
        var item = player.GetItemOnHand();
        if (isTurn)
        {
            ItemRenderer.RenderInWorld(renderer, item, x - 0.75f, y - 4, colors);
        }
        else
        {
            ItemRenderer.RenderInWorld(renderer, item, x, y - 4, colors);

        }
    }
    protected override string GetSpriteName()
    {
        return "Textures/Entities/player";
    }

}
