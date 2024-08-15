using ClientGalaxias.Client.Main;
using Galaxias.Core.World;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using System;

namespace ClientGalaxias.Client.Render;
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
    private ItemRenderer itemRenderer;
    private EntityRenderer entityRenderer = new();
    private BackgroundRenderer backgroundRenderer = new();
    private float sunRadius;
    private float scaleHeight;
    public WorldRenderer(GalaxiasClient galaxias, Camera camera, TileRenderer tileRenderer)
    {
        this.camera = camera;
        this.tileRenderer = tileRenderer;
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
                TileState tileState = _world.GetTileState(TileLayer.Main, x, y);
                TileState background = _world.GetTileState(TileLayer.Background, x, y);
                if (!tileState.IsFullTile() && background != AllTiles.Air.GetDefaultState())//todo
                {
                    int[] lights = _world.GetInterpolateLight(x, y);
                    Color[] colors = InterpolateWorldColor(lights, TileLayer.Background);

                    tileRenderer.Render(renderer, background, x, y, colors: colors);
                }
                if(!tileState.IsAir())
                {
                    int[] lights = _world.GetInterpolateLight(x, y);
                    Color[] colors = InterpolateWorldColor(lights, TileLayer.Main);

                    tileRenderer.Render(renderer, tileState, x, y, colors: colors);
                }
            }
        }
        _world.GetAllEntities().ForEach(e =>
        {
            //render hitbox will move to another place
            //HitBox box = e.hitbox;
            //renderer.Draw("Assets/Textures/Misc/blank", (float)box.minX * scale, (float)-(box.minY + box.GetHeight()) * scale, (float)box.GetWidth(), (float)box.GetHeight(), hitColor);

            byte Brightness = _world.GetCombinedLight(Utils.Floor(e.x), Utils.Floor(e.y));
            //var erenderer = e.GetRenderer();
            entityRenderer.Render(renderer, (float)e.x * scale, (float)-e.y * scale , 2, 4, e, scale, ShadowColor[Brightness], "player");

        });

    }

    private void RenderSky(IntegrationRenderer renderer)
    {
        
        float skylightMod = _world.GetSkyLightModify(false);
        backgroundRenderer.Render(renderer, -camera.GetX(), -camera.GetY(), Utils.Ceil(_galaxias.GetWindowWidth() / camera.GetScale()), Utils.Ceil(_galaxias.GetWindowHeight() / camera.GetScale()), GetBackgroundColor(skylightMod));
        //


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
        float factor = layer == TileLayer.Main ? 1 : 0.5f;
        Color[] colors = new Color[interpolatedLight.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Utils.MultiplyNoA(ShadowColor[interpolatedLight[i]], factor);
        }
        return colors;
    }
}