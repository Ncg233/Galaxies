using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
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
        if(apperaance == null)
        {
            apperaance = new TileRenderInfo();
        }
        float renderX = 0, renderY = 0, originX = 0, originY = 0, renderWidth = 0, renderHeight = 0;
        if (state.GetTile().GetRenderType() == TileRenderType.Center)
        {
            renderX = (x + 0.5f) * GameConstants.TileSize;
            renderY = -(y + 0.5f) * GameConstants.TileSize;
            originX = width / 2f;
            originY = height / 2f;
            renderWidth = width;
            renderHeight = height;
        }
        else if(state.GetTile().GetRenderType() == TileRenderType.BottomCenter)
        {
            renderX = (x + 0.5f) * GameConstants.TileSize;
            renderY = -y * GameConstants.TileSize;
            originX = width / 2f;
            originY = height;
            renderWidth = width;
            renderHeight = height;
        }
        else if (state.GetTile().GetRenderType() == TileRenderType.BottomCorner)
        {
            bool normal = state.GetFacing().Effect == SpriteEffects.None;
            renderX = (normal ? x : x + 1) * GameConstants.TileSize;
            renderY = -y * GameConstants.TileSize;
            originX = normal ? 0 : width;
            originY = height;
            renderWidth = width;
            renderHeight = height;
        }

        DrawTileSpriteMap(renderer, tileTexture, state, renderX, renderY, originX, originY, renderWidth, renderHeight,
                apperaance, color);


    }
    private static void DrawTileSpriteMap(IntegrationRenderer renderer, TileSpriteMap map, TileState state, float x, float y, float originX, float originY, float width, float height,
         TileRenderInfo appearance, Color color)
    {
        int rotation = appearance.RotationD;
        Texture2D tex = map.SourceTexture;
        var info = map.GetStateInfo(state);
        Facing facing = state.GetFacing();
        renderer.Draw(tex, new Vector2(x, y), info.GetRenderRect(appearance.TextureId), color, 
            (float)Math.PI / 180 * rotation, 
            new Vector2(originX, originY), 
            new Vector2(width / map.Width, height / map.Height), facing.Effect, 0);
    }
}
