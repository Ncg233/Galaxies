using Galaxias.Core.Main;
using Galaxias.Core.World;
using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using System;

namespace Galaxias.Client.Render;
public class WorldRenderer
{
    private readonly Color[] ShadowColor = new Color[GameConstants.MaxLight + 1];
    private readonly Color startColor = new Color(90, 150, 255);
    private readonly Color endColor = new Color(15, 15, 16);
    private readonly Color hitColor = new Color(0, 1, 0, 0.2f);
    private GalaxiasClient _galaxias;
    private AbstractWorld _world;
    private Camera camera;
    private TileRenderer tileRenderer;
    private float sunRadius;
    private float scaleHeight;
    public WorldRenderer(GalaxiasClient galaxias, Camera camera, TileRenderer tileRenderer)
    {
        this.camera = camera;
        this.tileRenderer = tileRenderer;
        float step = 1F / GameConstants.MaxLight;
        for (int i = 0; i < ShadowColor.Length; i++)
        {
            float modifier = i * step;
            ShadowColor[i] = new Color(modifier, modifier, modifier, 1f);
        }
        _galaxias = galaxias;
    }
    public void SetRenderWorld(AbstractWorld world)
    {
        _world = world;
    }
    public void LoadContents()
    {
    }
    public void Render(GameTime gameTime, IntegrationRenderer renderer)
    {
        RenderSky(renderer);
        int scale = GameConstants.TileSize;

        int minX = (int)((-camera._pos.X - _galaxias.GetWindowWidth() / camera.GetScale() / 2 - 16) / 8);
        int minY = (int)((camera._pos.Y - _galaxias.GetWindowHeight() / camera.GetScale() / 2 - 16) / 8);
        int maxX = (int)((-camera._pos.X + _galaxias.GetWindowWidth() / camera.GetScale() / 2 + 16) / 8);
        int maxY = (int)((camera._pos.Y + _galaxias.GetWindowHeight() / camera.GetScale() / 2 + 16) / 8);
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                TileState state = _world.GetTileState(TileLayer.Main, x, y);
                if (state != AllTiles.Air.GetDefaultState())
                {
                    int[] lights = _world.GetInterpolateLight(x, y);
                    Color[] colors = InterpolateWorldColor(lights);

                    tileRenderer.Render(renderer, state, x, y, colors: colors);
                    //renderer.Draw("Assets/Textures/Blocks/dirt", x * 8, -(y + 1) * 8, 1, 1, );
                }
            }
        }
        _world.GetAllEntities().ForEach(e =>
        {
            //render hitbox will move to another place
            //HitBox box = e.hitbox;
            //renderer.Draw("Assets/Textures/Misc/blank", (float)box.minX * scale, (float)-(box.minY + box.GetHeight()) * scale, (float)box.GetWidth(), (float)box.GetHeight(), hitColor);

            byte Brightness = _world.GetCombinedLight((int)e.x, (int)e.y + 1);
            var erenderer = e.GetRenderer();
            erenderer.Render(renderer, (float)e.x * scale, (float)-e.y * scale, 2, 4, e, scale, ShadowColor[Brightness]);

        });

    }

    private void RenderSky(IntegrationRenderer renderer)
    {
        float skylightMod = _world.GetSkyLightModify(false);

        _galaxias.GraphicsDevice.Clear(GetBackgroundColor(skylightMod));


        float w = _galaxias.GetWindowWidth() / camera.GetScale() / 2;
        scaleHeight = _galaxias.GetWindowHeight() / camera.GetScale() / 2;
        sunRadius = (w * w + scaleHeight * scaleHeight) / (2 * scaleHeight);
        float x = -camera.GetX() - (float)(sunRadius * Math.Cos(_world.GetSunRotation()) + 24);
        float y = -camera.GetY() + (sunRadius - scaleHeight) - (float)(sunRadius * Math.Sin(_world.GetSunRotation()));
        renderer.Draw("Assets/Textures/Skys/sun", x, y, Color.White);
    }
    private Color GetBackgroundColor(float skyLightMod)
    {
        return Color.Lerp(startColor, endColor, 1 - skyLightMod);
    }

    internal void OnResize(int width, int height)
    {
    }
    public Color[] InterpolateWorldColor(int[] interpolatedLight)
    {
        Color[] colors = new Color[interpolatedLight.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = ShadowColor[interpolatedLight[i]];
        }
        return colors;
    }
}