﻿using Galaxies.Client;
using Galaxies.Client.Resource;
using Galaxies.Core.World;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Particles;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using SharpDX;
using System;
using System.Collections.Generic;

namespace Galaxies.Client.Render;
public class WorldRenderer : IWorldListener
{
    private readonly Microsoft.Xna.Framework.Color[] ShadowColor = new Microsoft.Xna.Framework.Color[GameConstants.MaxLight + 1];
    private readonly Microsoft.Xna.Framework.Color[] LightColor = new Microsoft.Xna.Framework.Color[GameConstants.MaxLight + 1];
    private readonly Microsoft.Xna.Framework.Color startColor = new Microsoft.Xna.Framework.Color(90, 150, 255);
    private readonly Microsoft.Xna.Framework.Color endColor = new Microsoft.Xna.Framework.Color(15, 15, 16);
    private readonly Microsoft.Xna.Framework.Color hitColor = new Microsoft.Xna.Framework.Color(0, 1, 0, 0.2f);
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
        float step = 1.2F / GameConstants.MaxLight;
        for (int i = 0; i < ShadowColor.Length; i++)
        {
            float modifier = i * step;
            ShadowColor[i] = Microsoft.Xna.Framework.Color.Black * (1 - modifier);
            LightColor[i] = new Microsoft.Xna.Framework.Color(modifier, modifier, modifier, 1f);
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
                    if (tileState.ShouldRender())
                    {
                        int[] lights = _world.GetInterpolateLight(x, y);
                        Microsoft.Xna.Framework.Color[] colors = InterpolateWorldColor(lights);

                        var appearance = GetRenderApperance(layer, x, y, tileState);
                        TileRenderer.Render(renderer, tileState, layer, x, y, appearance, colors[0]);
                    }
                    
                }
            }
        }
            
        particleManager._particles.ForEach(p =>
        {
            p.Render(renderer, LightColor[_world.GetCombinedLight((int)p.X, (int)p.Y)]);
        });
        
        //render entity
        _world.GetAllEntities().ForEach(e =>
        {
            //render hitbox will move to another place
            //HitBox box = e.hitbox;
            //renderer.Draw("Assets/Textures/Misc/blank", (float)box.minX * scale, (float)-(box.minY + box.GetHeight()) * scale, (float)box.GetWidth(), (float)box.GetHeight(), hitColor);
            e.Render(renderer, LightColor[_world.GetCombinedLight((int)e.X, (int)e.Y)]);

        });
        var player = Main.GetInstance().GetPlayer();
        if(player != null && player.GetItemOnHand().GetItem() is TileItem tileItem)
        {
            Main.GetMosueTilePos(out int x, out int y);
            int[] lights = _world.GetInterpolateLight(x, y);
            Microsoft.Xna.Framework.Color[] colors = InterpolateWorldColor(lights);
            var state = tileItem.GetTile().GetPlaceState(_world, player, x, y);
            TileRenderer.Render(renderer, state, tileItem.GetLayer(), x, y, TileSpriteManager.GetStateInfo(state).DefaultInfo(), Utils.Multiply(colors[0], Microsoft.Xna.Framework.Color.White) * 0.75f);
        }
        

        
    }
    public void OnNotifyNeighbor(TileLayer layer, int x, int y, TileState state, TileState changeTile)
    {
        if (state.ShouldRender())
        {
            var apperaance = TileSpriteManager.GetStateInfo(state).UpdateAdjacencies(_world, layer, x, y);
            appearanceState[layer][_world.GetTileIndex(x, y)] = apperaance;
        }
    }
    private TileRenderInfo GetRenderApperance(TileLayer layer, int x, int y, TileState tileState)
    {
        
         var apperaance = appearanceState[layer][_world.GetTileIndex(x, y)];
         if (apperaance == null)
         {
             apperaance = TileSpriteManager.GetStateInfo(tileState).UpdateAdjacencies(_world, layer, x, y);
             appearanceState[layer][_world.GetTileIndex(x, y)] = apperaance;
         }
         return apperaance;

    }
    private void RenderSky(IntegrationRenderer renderer)
    {

        float skylightMod = _world.GetSkyLightModify(false);
        backgroundRenderer.Render(renderer, -camera.GetX(), -camera.GetY(), Utils.Ceil(camera.worldWidth), Utils.Ceil(camera.worldHeight), Microsoft.Xna.Framework.Color.White * skylightMod);

        float w = camera.worldWidth / 2;
        float h = camera.worldHeight / 2;
        float sunRadius = (w * w + h * h) / (2 * h);
        float x = -camera.GetX() - (float)(sunRadius * Math.Cos(_world.GetSunRotation()) + 24);
        float y = -camera.GetY() + (sunRadius - h) - (float)(sunRadius * Math.Sin(_world.GetSunRotation()));
        renderer.Draw("Textures/Skys/sun", x, y, Microsoft.Xna.Framework.Color.White);
    }
    private Microsoft.Xna.Framework.Color GetBackgroundColor(float skyLightMod)
    {
        return ShadowColor[(int)(GameConstants.MaxLight * (1 - skyLightMod))];
    }

    internal void OnResize(int width, int height)
    {
    }
    public Microsoft.Xna.Framework.Color[] InterpolateWorldColor(int[] interpolatedLight)
    {
        Microsoft.Xna.Framework.Color[] colors = new Microsoft.Xna.Framework.Color[interpolatedLight.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = LightColor[interpolatedLight[i]];
        }
        return colors;
    }

    public void AddParticle(TileState tileState, TileLayer layer, int x, int y)
    {
        if (tileState.ShouldRender())
        {
            float width = tileState.GetRenderWidth() / 8;
            float height = tileState.GetRenderWidth() / 8;
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

            for (int i = 0; i < Utils.Random.Next((int)(width * height)) + 3; i++)
            {
                float motionX = (float)Utils.Rand(0, 0.1f);
                float motionY = (float)Utils.Rand(0, 0.1f);
                float maxLife = Utils.Random.NextSingle() + 0.5f;
                AddParticle(new TileParticle(tileState, _world, x + Utils.Random.NextFloat(startX, endX), y + Utils.Random.NextFloat(startY, endY), motionX, motionY, maxLife));
            }
        }
    }
    public void AddParticle(Particle particle)
    {
        Main.GetInstance().GetParticleManager().AddParticle(particle);
    }

}