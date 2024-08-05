using ClientGalaxias.Client.Resource;
using Galasias.Core.World.Items;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ClientGalaxias.Client.Render;
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
                itemToTexture.Add(itemId.Value.GetItem(), manager.LoadTexture2D("Textures/Items/" + itemId.Key));
            }
        }
    }
    
    public void Render(IntegrationRenderer renderer, Item item, float x, float y, Color color)
    {
        Texture2D itemTexture = itemToTexture.GetValueOrDefault(item);
        int width = item is TileItem ? 8 : 16;
        int height = item is TileItem ? 8 : 16;
        //float vw = width / (float)GameConstants.TileSize;
        //float vh = height / (float)GameConstants.TileSize;
        //for (float i = 0; i < 2; i += 1)
        //{
        //    for (float j = 0; j < 2; j += 1) {
        //        renderer.Draw(tileTexture, (x - 1 / 2) * GameConstants.TileSize - width + width * i / 2, -(y - j / 2) * GameConstants.TileSize - height, 1, 1, colors[(int)(i * 2 + j)],
        //            new Rectangle((int)(i / 2 * width), (int)(j / 2 * height), width / 2, height / 2));
        //    }
        //}
        renderer.Draw(itemTexture ,new Rectangle((int)x - width / 2, (int)y - height / 2, width, height), color);

    }
    
}
