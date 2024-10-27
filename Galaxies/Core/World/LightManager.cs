using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World;
public class LightManager
{

    private readonly Dictionary<LightType, byte[]> lightGrid = [];
    private int Width, Height;
    private AbstractWorld World;
    public LightManager(AbstractWorld world,int width, int height)
    {
        World = world;
        Width = width;
        Height = height;
        lightGrid.Add(LightType.Sky, new byte[width * height]);
        lightGrid.Add(LightType.Tile, new byte[width * height]);
    }
    public void InitLight()
    {
        for (int x = Width - 1; x >= 0; x--)
        {
            for (int y = Height - 1; y >= 0; y--)
            {
                byte light = CalcLight(x, y, true);
                SetSkyLight(x, y, light);
            }
        }
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                byte light = CalcLight(x, y, true);
                SetSkyLight(x, y, light);
            }
        }
    }
    public void CauseLightUpdate(int x, int y)
    {
        foreach (Direction direction in Direction.SurroundingIncludNone)
        {
            int dirX = x + direction.X;
            int dirY = y + direction.Y;

            if (World.IsInWorld(dirY))
            {
                bool change = false;

                byte skylightThere = GetSkyLight(dirX, dirY);
                byte calcedSkylight = CalcLight(dirX, dirY, true);
                if (calcedSkylight != skylightThere)
                {
                    SetSkyLight(dirX, dirY, calcedSkylight);
                    change = true;
                }

                byte tilelightThere = GetTileLight(dirX, dirY);
                byte calcedTilelight = CalcLight(dirX, dirY, false);
                if (calcedTilelight != tilelightThere)
                {
                    SetTileLight(dirX, dirY, calcedTilelight);
                    change = true;
                }
                if (change)
                {
                    CauseLightUpdate(dirX, dirY);
                }
            }
        }

    }
    public byte CalcLight(int x, int y, bool isSky)
    {
        byte maxLight = 0;

        foreach (Direction direction in Direction.SurroundingIncludNone)
        {
            int dirX = x + direction.X;
            int dirY = y + direction.Y;

            if (World.IsInWorld(dirY))
            {
                byte light = isSky ? GetSkyLight(dirX, dirY) : GetTileLight(dirX, dirY);
                if (light > maxLight)
                {
                    maxLight = light;
                }
            }


        }

        maxLight = (byte)(maxLight * GetTileModifier(x, y, isSky));

        byte emitted = GetTileLight(x, y, isSky);
        if (emitted > maxLight)
        {
            maxLight = emitted;
        }

        return maxLight;
    }
    public byte GetTileLight(int x, int y, bool isSky)
    {
        int highestLight = 0;
        bool nonAir = false;

        foreach (TileLayer layer in Utils.GetAllLayers())
        {
            TileState tilestate = World.GetTileState(layer, x, y);
            if (!tilestate.IsAir())
            {
                int light = tilestate.GetLight();
                if (light > highestLight)
                {
                    highestLight = light;
                }

                nonAir = true;
            }
        }


        if (nonAir)
        {
            if (!isSky)
            {
                return (byte)highestLight;
            }
        }
        else if (isSky)
        {
            return GameConstants.MaxLight;
        }
        return 0;
    }

    public float GetTileModifier(int x, int y, bool isSky)
    {
        float smallestMod = 1F;
        bool nonAir = false;


        foreach (TileLayer layer in Utils.GetAllLayers())
        {

            Tile tile = World.GetTileState(layer, x, y).GetTile();
            if (!tile.IsAir())
            {
                float mod = tile.GetTranslucentModifier(World, x, y, layer, isSky);
                if (mod < smallestMod)
                {
                    smallestMod = mod;
                }

                nonAir = true;
            }
        }
        if (nonAir)
        {
            return smallestMod;
        }
        else
        {
            return isSky ? 1F : 0.8F;
        }
    }
    public void SetSkyLight(int x, int y, byte light)
    {
        if (World.IsInWorld(y))
        {
            var grid = lightGrid[LightType.Sky];
            grid[World.GetTileIndex(x, y)] = light;
        }
    }
    public byte GetSkyLight(int x, int y)
    {
        if (World.IsInWorld(y))
        {
            var grid = lightGrid[LightType.Sky];
            return grid[World.GetTileIndex(x, y)];
        }
        return GameConstants.MaxLight;
    }
    public void SetTileLight(int x, int y, byte light)
    {
        if (World.IsInWorld(y))
        {
            var grid = lightGrid[LightType.Tile];
            grid[World.GetTileIndex(x, y)] = light;
        }
    }
    public byte GetTileLight(int x, int y)
    {
        if (World.IsInWorld(y))
        {
            var grid = lightGrid[LightType.Tile];
            return grid[World.GetTileIndex(x, y)];
        }
        return GameConstants.MaxLight;
    }
}
