using Galaxias.Core.World;
using Galaxias.Core.World.Particles;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using System;

namespace Galaxias.Client.Render;
public class WorldRenderer
{
    private readonly Color[] ShadowColor = new Color[GameConstants.MaxLight + 1];
    private readonly Color startColor = new Color(90, 150, 255);
    private readonly Color endColor = new Color(15, 15, 16);
    private readonly Color hitColor = new Color(0, 1, 0, 0.2f);
    private Main _galaxias;
    private AbstractWorld _world;
    private Camera camera;
    private ItemRenderer itemRenderer;
    private BackgroundRenderer backgroundRenderer = new();
    private ParticleManager particleManager;
    private float sunRadius;
    private float scaleHeight;
    public WorldRenderer(Main galaxias, Camera camera, ParticleManager particleManager)
    {
        this.camera = camera;
        this.particleManager = particleManager;
        float step = 1.2F / GameConstants.MaxLight;
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
        backgroundRenderer.LoadContents();
    }
    public void Render(IntegrationRenderer renderer)
    {

        RenderSky(renderer);
        //render tiles
        int scale = GameConstants.TileSize;

        int minX = (int)((-camera._pos.X - _galaxias.GetWindowWidth() / camera.GetScale() / 2 - 32) / 8);
        int minY = (int)((camera._pos.Y - _galaxias.GetWindowHeight() / camera.GetScale() / 2 - 32) / 8);
        int maxX = (int)((-camera._pos.X + _galaxias.GetWindowWidth() / camera.GetScale() / 2 + 32) / 8);
        int maxY = (int)((camera._pos.Y + _galaxias.GetWindowHeight() / camera.GetScale() / 2 + 32) / 8);
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                TileState tileState = _world.GetTileState(TileLayer.Main, x, y);
                TileState background = _world.GetTileState(TileLayer.Background, x, y);
                if (!tileState.IsFullTile() && background != AllTiles.Air.GetDefaultState())//todo
                {
                    int[] lights = _world.GetInterpolateLight(x, y);
                    Color[] colors = InterpolateWorldColor(lights, TileLayer.Background);

                    TileRenderer.Render(renderer, background, x, y, colors: colors);
                }
                if (!tileState.IsAir())
                {
                    int[] lights = _world.GetInterpolateLight(x, y);
                    Color[] colors = InterpolateWorldColor(lights, TileLayer.Main);

                    TileRenderer.Render(renderer, tileState, x, y, colors: colors);
                }
            }
        }
        particleManager._particles.ForEach(p =>
        {
            p.Render(renderer, ShadowColor[_world.GetCombinedLight((int)p.x, (int)p.y)]);
        });
        //render entity
        _world.GetAllEntities().ForEach(e =>
        {
            //render hitbox will move to another place
            //HitBox box = e.hitbox;
            //renderer.Draw("Assets/Textures/Misc/blank", (float)box.minX * scale, (float)-(box.minY + box.GetHeight()) * scale, (float)box.GetWidth(), (float)box.GetHeight(), hitColor);

            byte Brightness = _world.GetCombinedLight(Utils.Floor(e.x), Utils.Floor(e.y));
            //var eRenderer = EntityRendererHandler.GetRenderer(e.Type);
            //eRenderer.Render(renderer, e, scale, ShadowColor[Brightness]);
            e.Render(renderer, ShadowColor[Brightness]);

        });

    }

    private void RenderSky(IntegrationRenderer renderer)
    {

        float skylightMod = _world.GetSkyLightModify(false);
        backgroundRenderer.Render(renderer, -camera.GetX(), -camera.GetY(), Utils.Ceil(_galaxias.GetWindowWidth() / camera.GetScale()), Utils.Ceil(_galaxias.GetWindowHeight() / camera.GetScale()), GetBackgroundColor(skylightMod));

        float w = _galaxias.GetWindowWidth() / camera.GetScale() / 2;
        scaleHeight = _galaxias.GetWindowHeight() / camera.GetScale() / 2;
        sunRadius = (w * w + scaleHeight * scaleHeight) / (2 * scaleHeight);
        float x = -camera.GetX() - (float)(sunRadius * Math.Cos(_world.GetSunRotation()) + 24);
        float y = -camera.GetY() + (sunRadius - scaleHeight) - (float)(sunRadius * Math.Sin(_world.GetSunRotation()));
        renderer.Draw("Textures/Skys/sun", x, y, Color.White);
    }
    private Color GetBackgroundColor(float skyLightMod)
    {
        return ShadowColor[(int)(GameConstants.MaxLight * skyLightMod)];
    }

    internal void OnResize(int width, int height)
    {
    }
    public Color[] InterpolateWorldColor(int[] interpolatedLight, TileLayer layer)
    {
        float factor = layer == TileLayer.Main ? 1 : 0.7f;
        Color[] colors = new Color[interpolatedLight.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Utils.MultiplyNoA(ShadowColor[interpolatedLight[i]], factor);
        }
        return colors;
    }
}