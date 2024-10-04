using Galaxies.Core.World.Entities;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Galaxies.Client.Render;
public class PlayerRenderer : EntityRenderer<AbstractPlayerEntity>
{
    private static Color ClothesColor = Color.Blue;
    private static Color FaceColor = new Color(220, 179, 125);
    public override void LoadContent()
    {
        
    }

    public override void Render(IntegrationRenderer renderer, AbstractPlayerEntity player, int scale, Color colors)
    {
        var x = player.GetRenderX();
        var y = player.GetRenderY();
        var width = 16;
        var height = 32;
        bool isTurn = player.direction == Direction.Left;
        //BODY
        renderer.Draw("Textures/Entities/Player/player_leg", x, y,
            width / 2f, height, colors,
            source: GetSource(player),
            effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

        renderer.Draw("Textures/Entities/Player/player_body", x, y,
            width / 2f, height, colors,
            effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

        renderer.Draw("Textures/Entities/Player/player_head", x, y,
            width / 2f, height, colors,
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

    private Rectangle? GetSource(AbstractPlayerEntity player)
    {
        if(player.IsWalking)
        {
            int count = 5;
            long runningTime = DateTime.UtcNow.Ticks / 1000 % (count * 1200);

            long accum = 0;
            for (int i = 0; i < count; i++)
            {
                accum += 1200;
                if (accum >= runningTime)
                {
                    return new Rectangle(16 + 16 * i, 0, 16, 32);
                }
            }
        }else
        {
            return new Rectangle(0, 0, 16, 32);
        }
        return null;
       
    }
}
