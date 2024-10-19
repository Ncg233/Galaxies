using Galaxies.Client.Resource;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Tiles;
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
        foreach (var itemId in AllItems.itemRegister)
        {
            if (itemId.Key != "air")
            {
                //if (itemId.Value is TileItem)
                //{
                //    itemToTexture.Add(itemId.Value, TextureManager.LoadTexture2D("Textures/Tiles/" + itemId.Key));
                //}
                //else {
                itemToTexture.Add(itemId.Value, TextureManager.LoadTexture2D("Textures/Items/" + itemId.Key));
                //}
                
            }
        }
    }
    public static void RenderInWorld(IntegrationRenderer renderer, ItemPile itemPile, float worldX, float worldY, Color color)
    {
        if (itemPile != null && !itemPile.isEmpty())
        {
            Item item = itemPile.GetItem();
            Texture2D tex = itemToTexture.GetValueOrDefault(item);

            float width = 8;
            float height = 8;
            renderer.Draw(tex, worldX, worldY - height, color, width / tex.Width, height / tex.Height);
        }
    }

    public static void RenderInGui(IntegrationRenderer renderer, ItemPile itemPile, float x, float y, Color color)
    {
        if (itemPile != null && !itemPile.isEmpty())
        {
            Item item = itemPile.GetItem();
            Texture2D itemTexture = itemToTexture.GetValueOrDefault(item);

            int width = item is TileItem ? 8 : 16;
            int height = item is TileItem ? 8 : 16;
            renderer.Draw(itemTexture, new Rectangle((int)x - width / 2, (int)y - height / 2, width, height), color);
            renderer.DrawString(itemPile.GetCount().ToString(), x, y, 0.5f);
        }

    }
}
