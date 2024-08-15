using Galaxias.Client.Main;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Client.Render;
public class PlayerRenderer : EntityRenderer
{
    public override void Render(IntegrationRenderer renderer, Entity entity, int scale, Color colors)
    {
        Player player = (Player)entity;
        var x = (float)player.x;
        var y = (float)player.y;
        var width = 2;
        var height = player.GetHeight();
        bool isTurn = entity.direction == Direction.Left;
        //BODY
        renderer.Draw(GetSpriteName(), (x - width / 2) * GameConstants.TileSize, (-y - height) * GameConstants.TileSize,
            colors,
            effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        //HELD ITEM
        var item = player.GetItemOnHand();
        if (isTurn)
        {
            GalaxiasClient.GetInstance().GetItemRenderer().RenderInWorld(renderer, item, x - 0.75f, y + 2, colors);
        }else
        {
            GalaxiasClient.GetInstance().GetItemRenderer().RenderInWorld(renderer, item, x, y + 2, colors);

        }
        

    }

    protected override string GetSpriteName()
    {
        return "Textures/Entities/player";
    }

}
