using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxies.Client.Render;
public class TileRenderer
{
    public static void LoadContent()
    {
        TileSpriteManager.LoadContent();
    }
    public static void Render(IntegrationRenderer renderer, TileState state, TileLayer layer, float x, float y, TileRenderInfo apperaance, Color colors)
    {
        TileSpriteMap tileTexture = TileSpriteManager.GetSpriteMap(state);
        int width = tileTexture.Width;
        int height = tileTexture.Height;
        int hw = width / 2;
        int hh = height / 2;
        Color color = Utils.MultiplyNoA(colors, layer == TileLayer.Main ? 1 : 0.45f);
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

            DrawTileSpriteMap(renderer, tileTexture, state, (x + 0.5f) * GameConstants.TileSize, -(y + 0.5f) * GameConstants.TileSize,
                width / 2f, height / 2f,
                width, height, apperaance, color);
        }
        else if(state.GetTile().GetRenderType() == TileRenderType.BottomCenter)
        {
            DrawTileSpriteMap(renderer, tileTexture, state, (x + 0.5f) * GameConstants.TileSize, -y * GameConstants.TileSize, width / 2f, height, width, height,
                apperaance, color);
        }
        else if (state.GetTile().GetRenderType() == TileRenderType.BottomCorner)
        {
            DrawTileSpriteMap(renderer, tileTexture, state, x * GameConstants.TileSize, -y * GameConstants.TileSize, 0, height, width, height,
                apperaance, color);
        }

    }
    private static void DrawTileSpriteMap(IntegrationRenderer renderer, TileSpriteMap map, TileState state, float x, float y, float originX, float originY, float width, float height,
         TileRenderInfo appearance, Color color)
    {
        int rotation = appearance.RotationD;
        Texture2D tex = map.SourceTexture;
        var info = map.GetStateInfo(state);
        Facing facing = state.Facing;
        renderer.Draw(tex, new Vector2(x, y), info.GetRenderRect(appearance.TextureId), color, (float)Math.PI / 180 * rotation, new Vector2(originX, originY), new Vector2(width / map.Width, height / map.Height), facing.Effect, 0);
    }
}
