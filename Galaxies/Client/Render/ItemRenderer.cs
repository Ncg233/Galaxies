using Galaxies.Client.Resource;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Galaxies.Client.Render;
public class ItemRenderer
{

    private static readonly Dictionary<Item, Texture2D> itemToTexture = new Dictionary<Item, Texture2D>();
    public static void LoadContent()
    {
    }
    public static void RenderInWorld(IntegrationRenderer renderer, ItemPile itemPile, float worldX, float worldY,float scale,Color color, SpriteEffects effects = SpriteEffects.None)
    {
        if (itemPile != null && !itemPile.IsEmpty())
        {
            Item item = itemPile.GetItem();
            var map = SpriteManager.GetSpriteMap(item);
            Texture2D tex = map.Texture;

            float width = map.RenderWidth;
            float height = map.Renderheight;
            renderer.Draw(tex, worldX - width / 2f, worldY - height, Utils.MultiplyNoA(color, map.ColorMod), width / tex.Width * scale, height / tex.Height * scale, null, effects);
        }
    }

    public static void RenderInGui(IntegrationRenderer renderer, ItemPile itemPile, float x, float y, Color color)
    {
        if (itemPile != null && !itemPile.IsEmpty())
        {
            Item item = itemPile.GetItem();
            var map = SpriteManager.GetSpriteMap(item);
            Texture2D tex = map.Texture;

            int width = map.RenderWidth;
            int height = map.Renderheight;
            renderer.Draw(tex, new Rectangle((int)x - width / 2, (int)y - height / 2, width, height), Utils.MultiplyNoA(color, map.ColorMod));
            renderer.DrawString(itemPile.GetCount().ToString(), x, y, 0.5f);
        }

    }
}
