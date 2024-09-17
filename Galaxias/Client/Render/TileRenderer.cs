using Galaxias.Client.Resource;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Galaxias.Client.Render;
public class TileRenderer
{
    public static readonly Dictionary<TileState, SpriteMap> stateToSprite = [];
    public static void LoadContent()
    {

        foreach (var tileId in AllTiles.tileRegister)
        {
            if (tileId.Key != "air")
            {
                stateToSprite.Add(tileId.Value.GetDefaultState(), new SpriteMap(TextureManager.LoadTexture2D("Textures/Blocks/" + tileId.Key), 1, 1));
            }
        }
    }
    public static void Render(IntegrationRenderer renderer, TileState state, float x, float y, Color[] colors)
    {
        SpriteMap tileTexture = stateToSprite.GetValueOrDefault(state);
        int width = tileTexture.Width;
        int height = tileTexture.Height;
        int hw = width / 2;
        int hh = height / 2;

        if (state.GetTile().GetRenderType() == TileRenderType.Center)
        {
            //for (int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < 2; j++)
            //    {
            //
            //        renderer.DrawSpriteMap(tileTexture, hw - 4 + x * 8 + hw * i, -2f -y * 8 - hh * (1 - j), hw / 2, hh / 2, width, height, 
            //            i * hw, j * hh, hw, hh, 0, colors[i * 2 + j]);
            //        //renderer.DrawSpriteMap(tileTexture, 2f + x * 8 + hw * i, -2f - y * 8 - hh * (1 - j), hw / 2, hh / 2, width, height,
            //        //    i * hw, j * hh, hw, hh, 0, colors[i * 2 + j]);
            //    }
            //}
            
            renderer.DrawSpriteMap(tileTexture, (x + 0.5f) * GameConstants.TileSize, -(y + 0.5f) * GameConstants.TileSize, 
                width / 2f, height / 2f, 
                width, height, 0, 0, width, height, 0, colors[0]);
        }
        else
        {
            renderer.DrawSpriteMap(tileTexture, (x + 0.5f) * GameConstants.TileSize, -y * GameConstants.TileSize, width / 2f, height, width, height, 0, 0, width, height, 0, colors[0]);
        }

    }
}
