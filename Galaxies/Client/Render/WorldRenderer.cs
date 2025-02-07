using Galaxies.Core.World;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Particles;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace Galaxies.Client.Render;
public class WorldRenderer : IWorldListener
{
    private readonly Color[] ShadowColor = new Color[GameConstants.MaxLight + 1];
    private readonly Color[] LightColor = new Color[GameConstants.MaxLight + 1];
    private readonly Color startColor = new(90, 150, 255);
    private readonly Color endColor = new(15, 15, 16);
    private readonly Color hitColor = new(0, 1, 0, 0.2f);
    private Main _galaxias;
    private AbstractWorld _world;
    private Camera camera;
    private ItemRenderer itemRenderer;
    private BackgroundRenderer backgroundRenderer = new();
    private ParticleManager particleManager;
    private readonly Dictionary<TileLayer, TileRenderInfo[]> appearanceState = [];
    public WorldRenderer(Main galaxias, Camera camera, ParticleManager particleManager)
    {
        this.camera = camera;
        this.particleManager = particleManager;
        float step = 1.4F / GameConstants.MaxLight;
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
        appearanceState.Clear();
        foreach (var layer in Utils.GetAllLayers())
        {
            TileRenderInfo[] states = new TileRenderInfo[world.Width * world.Height];
            appearanceState.Add(layer, states);
        }
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
                    if (tileState.ShouldRender() && ShouldRender(layer, x, y))
                    { 
                        var appearance = GetRenderApperance(layer, x, y, tileState);
                        TileRenderer.Render(renderer, tileState, layer, x, y, appearance, GetColors(x, y));
                    }
                    
                }
            }
        }
            
        particleManager._particles.ForEach(p =>
        {
            p.Render(renderer, LightColor[_world.GetCombinedLightSmooth((int)p.X, (int)p.Y)]);
        });
        
        //render entity
        _world.GetAllEntities().ForEach(e =>
        {
            //render hitbox will move to another place
            //HitBox box = e.hitbox;
            //renderer.Draw("Assets/Textures/Misc/blank", (float)box.minX * scale, (float)-(box.minY + box.GetHeight()) * scale, (float)box.GetWidth(), (float)box.GetHeight(), hitColor);
            e.Render(renderer, LightColor[_world.GetCombinedLightSmooth((int)e.X, (int)e.Y)]);

        });
        
    }

    private bool ShouldRender(TileLayer layer, int x, int y)
    {
        if(layer == TileLayer.Main)
        {
            return true; 
        }else if(layer == TileLayer.Background)
        {
            
            var state = _world.GetTileState(TileLayer.Main, x, y);
            
            if(state.IsFullTile() && state.ShouldRender())
            {
                var appearance = GetRenderApperance(TileLayer.Main, x, y, state);
                return !appearance.IsFull();
            }
            return true;

        }
        return false;
        
    }

    public void OnNotifyNeighbor(TileLayer layer, int x, int y, TileState state, TileState changeTile)
    {
        if (state.ShouldRender())
        {
            var apperaance = SpriteManager.GetStateInfo(state).UpdateAdjacencies(_world, layer, x, y);
            appearanceState[layer][_world.GetTileIndex(x, y)] = apperaance;
        }
    }
    private Color[] GetColors(int x, int y)
    {
        var colors = new Color[4];
        byte[] light = new byte[5];
        for (int i = 0; i < Direction.AdjacentIncludeNone.Length; i++)
        {
            var dir = Direction.AdjacentIncludeNone[i];
            light[i] = _world.GetCombinedLight(x + dir.X, y + dir.Y);
        }
        colors[0] = LightColor[(light[0] + light[3] + light[4]) / 3];
        colors[1] = LightColor[(light[2] + light[3] + light[4]) / 3];
        colors[2] = LightColor[(light[0] + light[1] + light[4]) / 3];
        colors[3] = LightColor[(light[1] + light[2] + light[4]) / 3];
        return colors;
    }
    private TileRenderInfo GetRenderApperance(TileLayer layer, int x, int y, TileState tileState)
    {
        
         var apperaance = appearanceState[layer][_world.GetTileIndex(x, y)];
         if (apperaance == null)
         {
             apperaance = SpriteManager.GetStateInfo(tileState).UpdateAdjacencies(_world, layer, x, y);
             appearanceState[layer][_world.GetTileIndex(x, y)] = apperaance;
         }
         return apperaance;

    }
    private void RenderSky(IntegrationRenderer renderer)
    {

        float skylightMod = _world.GetSkyLightModify(false);
        backgroundRenderer.Render(renderer, Utils.Ceil(camera.worldWidth), Utils.Ceil(camera.worldHeight), Color.White * skylightMod);

        float w = camera.worldWidth / 2;
        float h = camera.worldHeight / 2;
        float sunRadius = (w * w + h * h) / (2 * h);
        float x = -camera.GetX() - (float)(sunRadius * Math.Cos(_world.GetSunRotation()) + 24);
        float y = -camera.GetY() + (sunRadius - h) - (float)(sunRadius * Math.Sin(_world.GetSunRotation()));
        renderer.Draw("Textures/Skys/sun", x, y, Color.White);
    }

    internal void OnResize(int width, int height)
    {
    }

    public void AddParticle(TileState tileState, TileLayer layer, int x, int y)
    {
        if (tileState.ShouldRender())
        {
            float width = tileState.GetRenderWidth() / 8f;
            float height = tileState.GetRenderWidth() / 8f;
            float startX = 0, startY = 0, endX = 0, endY = 0;
            if (tileState.GetRenderType() == TileRenderType.Center)
            {
                startX = 0; startY = 0; endX = width; endY = height;
            }else if(tileState.GetRenderType() == TileRenderType.BottomCenter)
            {
                startX = -width / 2; startY = 0; endX = width / 2; endY = height;
            }else if(tileState.GetRenderType() == TileRenderType.BottomCorner)
            {
                startX = tileState.GetFacing().IsHorTurn() ? -width : 0;
                startY = 0;
                endX = tileState.GetFacing().IsHorTurn() ? 0 : width; 
                endY = height;
            }

            for (int i = 0; i < Utils.Random.Next((int)(width * height)) + 5; i++)
            {
                float motionX = (float)Utils.Rand(0, 0.1f);
                float motionY = (float)Utils.Rand(0, 0.1f);
                float maxLife = Utils.Random.NextSingle() + 0.5f; 
                AddParticle(new TileParticle(tileState, _world, x + Utils.NextFloat(startX, endX), y + Utils.NextFloat(startY, endY), motionX, motionY, maxLife));
            }
        }
    }
    public void AddParticle(Particle particle)
    {
        Main.GetInstance().GetParticleManager().AddParticle(particle);
    }

}