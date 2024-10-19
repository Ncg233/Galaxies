using Galaxies.Client;
using Galaxies.Client.Resource;
using Galaxies.Core.World;
using Galaxies.Core.World.Particles;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;

namespace Galaxies.Client.Render;
public class WorldRenderer
{
    private readonly Color[] ShadowColor = new Color[GameConstants.MaxLight + 1];
    private readonly Color[] LightColor = new Color[GameConstants.MaxLight + 1];
    private readonly Color startColor = new Color(90, 150, 255);
    private readonly Color endColor = new Color(15, 15, 16);
    private readonly Color hitColor = new Color(0, 1, 0, 0.2f);
    private Main _galaxias;
    private AbstractWorld _world;
    private Camera camera;
    private ItemRenderer itemRenderer;
    private BackgroundRenderer backgroundRenderer = new();
    private ParticleManager particleManager;
    //private readonly Dictionary<TileLayer, byte[]> appearanceState = [];
    public WorldRenderer(Main galaxias, Camera camera, ParticleManager particleManager)
    {
        this.camera = camera;
        this.particleManager = particleManager;
        float step = 1.2F / GameConstants.MaxLight;
        for (int i = 0; i < ShadowColor.Length; i++)
        {
            float modifier = i * step;
            ShadowColor[i] = Color.Black * (1 - modifier);
            LightColor[i] = new Color(modifier, modifier, modifier, 1f);
        }
        _galaxias = galaxias;

    }
    public void SetRenderWorld(AbstractWorld world)
    {
        _world = world;
        //appearanceState.Clear();
        //foreach (var layer in Utils.GetAllLayers())
        //{
        //    byte[] states = new byte[world.Width * world.Height];
        //    Array.Fill(states, Byte.MaxValue);
        //    appearanceState.Add(layer, states);
        //}
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
        foreach (var layer in Utils.GetAllLayers())
        {
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    var tileState = _world.GetTileState(layer, x, y);
                    if (!tileState.IsAir())
                    {
                        var appearance = GetRenderApperance(layer, x, y, tileState);
                        TileRenderer.Render(renderer, tileState, layer, x, y, appearance, colors: Color.White);
                    }
                    
                }
            }
        }
            
        particleManager._particles.ForEach(p =>
        {
            p.Render(renderer, Color.White);
        });
        //render light
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                int[] lights = _world.GetInterpolateLight(x, y);
                Color[] colors = InterpolateWorldColor(lights);
                renderer.Draw(TextureManager.BlankTexture, x * GameConstants.TileSize, -y * GameConstants.TileSize, colors[0]);
            }
        }
        
        //render entity
        _world.GetAllEntities().ForEach(e =>
        {
            //render hitbox will move to another place
            //HitBox box = e.hitbox;
            //renderer.Draw("Assets/Textures/Misc/blank", (float)box.minX * scale, (float)-(box.minY + box.GetHeight()) * scale, (float)box.GetWidth(), (float)box.GetHeight(), hitColor);
            //var eRenderer = EntityRendererHandler.GetRenderer(e.Type);
            //eRenderer.Render(renderer, e, scale, ShadowColor[Brightness]);
            e.Render(renderer, LightColor[_world.GetCombinedLight((int)e.x, (int)e.y)]);

        });
        
        
    }
    private byte GetRenderApperance(TileLayer layer, int x, int y, TileState tileState)
    {
        return TileSpriteManager.GetStateInfo(tileState).UpdateAdjacencies(_world, layer, x, y);
    }
    private void RenderSky(IntegrationRenderer renderer)
    {

        float skylightMod = _world.GetSkyLightModify(false);
        backgroundRenderer.Render(renderer, -camera.GetX(), -camera.GetY(), Utils.Ceil(camera.worldWidth), Utils.Ceil(camera.worldHeight), Color.White * skylightMod);

        float w = camera.worldWidth / 2;
        float h = camera.worldHeight / 2;
        float sunRadius = (w * w + h * h) / (2 * h);
        float x = -camera.GetX() - (float)(sunRadius * Math.Cos(_world.GetSunRotation()) + 24);
        float y = -camera.GetY() + (sunRadius - h) - (float)(sunRadius * Math.Sin(_world.GetSunRotation()));
        renderer.Draw("Textures/Skys/sun", x, y, Color.White);
    }
    private Color GetBackgroundColor(float skyLightMod)
    {
        return ShadowColor[(int)(GameConstants.MaxLight * (1 - skyLightMod))];
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