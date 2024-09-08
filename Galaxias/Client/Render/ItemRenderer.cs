using Galaxias.Client.Resource;
using Galaxias.Core.World.Items;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Galaxias.Client.Render;
public class ItemRenderer
{
    private readonly Dictionary<Item, Texture2D> itemToTexture = new Dictionary<Item, Texture2D>();
    public ItemRenderer()
    {
    }
    public void LoadContent(TextureManager manager)
    {
        foreach (var itemId in AllItems.itemRegister)
        {
            if (itemId.Key != "air")
            {
                itemToTexture.Add(itemId.Value, manager.LoadTexture2D("Textures/Items/" + itemId.Key));
            }
        }
    }
    public void RenderInWorld(IntegrationRenderer renderer, ItemPile itemPile, float worldX, float worldY, Color color)
    {
        if (itemPile != null)
        {
            Item item = itemPile.GetItem();
            Texture2D tex = itemToTexture.GetValueOrDefault(item);

            float width = 8;
            float height = 8;
            renderer.Draw(tex, worldX * GameConstants.TileSize, -worldY * GameConstants.TileSize, color, width / tex.Width, height / tex.Height);
        }
    }

    public void RenderInGui(IntegrationRenderer renderer, ItemPile itemPile, float x, float y, Color color)
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
