using Galaxies.Client.Resource;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace Galaxies.Client.Render;
public class TileRenderer
{
    public static void LoadContent()
    {
        SpriteManager.LoadContent();
    }
    public static void Render(IntegrationRenderer renderer, TileState state, TileLayer layer, float x, float y, TileRenderInfo apperaance, Color[] colors)
    {
        TileSpriteMap tileTexture = SpriteManager.GetSpriteMap(state);
        int width = tileTexture.Width;
        int height = tileTexture.Height;
        
        float renderX = 0, renderY = 0, originX = width / 2f, originY = height / 2f, renderWidth = width, renderHeight = height;
        if (state.GetTile().GetRenderType() == TileRenderType.Center)
        {
            renderX = (x + 0.5f) * GameConstants.TileSize;
            renderY = -(y + 0.5f) * GameConstants.TileSize;
        }
        else if(state.GetTile().GetRenderType() == TileRenderType.BottomCenter)
        {
            renderX = (x + 0.5f) * GameConstants.TileSize;
            renderY = -y * GameConstants.TileSize - height / 2;
        }
        else if (state.GetTile().GetRenderType() == TileRenderType.BottomCorner)
        {
            bool normal = state.GetFacing().Effect == SpriteEffects.None;
            renderX = normal ? (x * GameConstants.TileSize + width / 2f) : ((x + 1) * GameConstants.TileSize - width / 2f);
            renderY = -y * GameConstants.TileSize - height / 2;
        }
        DrawTileSpriteMap(renderer, tileTexture, state,layer, renderX, renderY, originX, originY, renderWidth, renderHeight,
               apperaance, colors);
    }
    private static void DrawTileSpriteMap(IntegrationRenderer renderer, TileSpriteMap map, TileState state, TileLayer layer, float x, float y, float originX, float originY, float width, float height,
         TileRenderInfo appearance, Color[] colors)
    {
        appearance ??= new TileRenderInfo();

        int angle = appearance.RotationD;
        Texture2D tex = map.SourceTexture;
        var info = map.GetStateInfo(state);
        Facing facing = appearance.Facing;
        var rect = info.GetRenderRect(appearance.TextureId);
        if(state.GetRenderType() == TileRenderType.Center)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Transform(i, j, out int i2, out int j2, angle, facing);
                    renderer.Draw(tex, new Vector2(x - width / 4f + i * width / 2, y - height / 4f + j * height / 2), new Rectangle(rect.X + i2 * rect.Width / 2, rect.Y + j2 * rect.Height / 2, rect.Width / 2, rect.Height / 2), Utils.MultiplyNoA(colors[i * 2 + j], layer == TileLayer.Main ? 1 : 0.45f),
                        (float)Math.PI / 180 * angle,
                        new Vector2(originX / 2, originY / 2),
                        new Vector2(width / map.Width, height / map.Height), facing.Effect, 0);
                }
            }
        }else
        {
            renderer.Draw(tex, new Vector2(x, y), info.GetRenderRect(appearance.TextureId), Utils.MultiplyNoA(colors[0], layer == TileLayer.Main ? 1 : 0.45f),
            (float)Math.PI / 180 * angle,
            new Vector2(originX, originY),
            new Vector2(width / map.Width, height / map.Height), facing.Effect, 0);
        }
    }
    private static void Transform(int i, int j, out int i2, out int j2, int angle, Facing facing)
    {
        j2 = j;
        i2 = i;
        if (angle != 0)
        {
            if (angle == 180)
            {
                i2 = (i + 1) % 2;
                j2 = (j + 1) % 2;
                //oy = -height / 2;
            }
            else if (angle == 90)
            {
                if (i == 0)
                {
                    if (j == 0) j2 = 1;
                    else i2 = 1;
                }
                else
                {
                    if (j == 1) j2 = 0;
                    else i2 = 0;
                }
            }
            else if (angle == 270)
            {
                if (i == 0)
                {
                    if (j == 0) i2 = 1;
                    else j2 = 0;
                }
                else
                {
                    if (j == 1) i2 = 0;
                    else j2 = 1;
                }
            }
        }
        if (facing.IsHorTurn())
        {
            i2 = (i2 + 1) % 2;
        }
    }
}
