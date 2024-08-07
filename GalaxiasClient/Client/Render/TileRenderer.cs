using ClientGalaxias.Client.Resource;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ClientGalaxias.Client.Render;
public class TileRenderer
{
    private readonly Dictionary<TileState, Texture2D> stateToTexture = [];
    public TileRenderer()
    {
    }
    public void LoadContent(TextureManager manager)
    {
        foreach (var tileId in AllTiles.tileRegister)
        {
            if (tileId.Key != "air")
            {
                stateToTexture.Add(tileId.Value.GetDefaultState(), manager.LoadTexture2D("Textures/Blocks/" + tileId.Key));
            }
        }
    }
    public void Render(IntegrationRenderer renderer, TileState state, float x, float y, Color[] colors)
    {
        Texture2D tileTexture = stateToTexture.GetValueOrDefault(state);
        int width = tileTexture.Width;
        int height = tileTexture.Height;
        float vw = width / (float)GameConstants.TileSize;
        float vh = height / (float)GameConstants.TileSize;
        //for (float i = 0; i < 2; i += 1)
        //{
        //    for (float j = 0; j < 2; j += 1) {
        //        renderer.Draw(tileTexture, (x - 1 / 2) * GameConstants.TileSize - width + width * i / 2, -(y - j / 2) * GameConstants.TileSize - height, 1, 1, colors[(int)(i * 2 + j)],
        //            new Rectangle((int)(i / 2 * width), (int)(j / 2 * height), width / 2, height / 2));
        //    }
        //}
        if (state.GetTile().GetRenderType() == TileRenderType.Center)
        {
            renderer.Draw(tileTexture, x * GameConstants.TileSize - (width - GameConstants.TileSize) / 2, -(y + 0.5f) * GameConstants.TileSize - height / 2, colors[0],
                        source: new Rectangle(0, 0, width, height));
        }
        else {
            renderer.Draw(tileTexture, x * GameConstants.TileSize - (width - GameConstants.TileSize) / 2, -(y + vh) * GameConstants.TileSize, colors[0],
                        source: new Rectangle(0, 0, width, height));
        }
        

    }
}
