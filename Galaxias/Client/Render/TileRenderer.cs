using Galaxias.Client.Resource;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxias.Client.Render;
public class TileRenderer
{
    private readonly Dictionary<TileState, SpriteMap> stateToTexture = [];
    public TileRenderer()
    {
    }
    public void LoadContent(TextureManager manager)
    {
        foreach (var tileId in AllTiles.tileRegister)
        {
            if (tileId.Key != "air")
            {
                stateToTexture.Add(tileId.Value.GetDefaultState(), new SpriteMap(manager.LoadTexture2D("Textures/Blocks/" + tileId.Key), 1, 1));
            }
        }
    }
    public void Render(IntegrationRenderer renderer, TileState state, float x, float y, Color[] colors)
    {
        SpriteMap tileTexture = stateToTexture.GetValueOrDefault(state);
        int width = tileTexture.Width;
        int height = tileTexture.Height;

        if (state.GetTile().GetRenderType() == TileRenderType.Center)
        {
            renderer.DrawSpriteMap(tileTexture, (x + 0.5f) * GameConstants.TileSize, -(y + 0.5f) * GameConstants.TileSize, width / 2f, height / 2f, 0, colors[0]);
        }
        else
        {
            renderer.DrawSpriteMap(tileTexture, (x + 0.5f) * GameConstants.TileSize, -y * GameConstants.TileSize, width / 2f, height, 0, colors[0]);
        }
        //for (float i = 0; i < 2; i += 1)
        //{
        //    for (float j = 0; j < 2; j += 1) {
        //        renderer.Draw(tileTexture, (x - 1 / 2) * GameConstants.TileSize - width + width * i / 2, -(y - j / 2) * GameConstants.TileSize - height, 1, 1, colors[(int)(i * 2 + j)],
        //            new Rectangle((int)(i / 2 * width), (int)(j / 2 * height), width / 2, height / 2));
        //    }
        //}

    }
}
