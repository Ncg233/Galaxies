using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;

namespace Galaxies.Client.Render;
internal class SmoothStateInfo : IStateInfo
{
    public static byte Full = 0;
    public static byte SideIII = 1;
    public static byte SideII = 2;
    public static byte SideI = 3;
    public static byte Corner = 4;
    public static byte Single = 5;

    public static byte None = 0;
    public static byte Right = 1;
    public static byte Down = 2;
    public static byte Left = 3;
    //public static byte Any = 4;


    private List<Rectangle> sourceRect;
    public SmoothStateInfo(List<Rectangle> sourceRect)
    {
        this.sourceRect = sourceRect;
    }
    public Rectangle GetRenderRect(byte id)
    {
        return sourceRect[id % 10];
    }

    public byte UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        // the all sides are same
        if (IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y) && IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y))
        {
            return WithRotation(Full, (byte)((x + y) % 4));
            //return Full;
        }
        // the three sides are same
        else if (IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideIII, None);
        }
        else if (!IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideIII, Right);
        }
        else if (IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideIII, Down);
        }
        else if (IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideIII, Left);
        }
        // the two sides are same （side）
        else if (!IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideII, None);
        }
        else if (IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideII, Left);
        }
        //(corner)
        else if (IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(Corner, None);
        }
        else if (!IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(Corner, Right);
        }
        else if (!IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(Corner, Down);
        }
        else if (IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(Corner, Left);
        }
        // the one side is same 
        else if (!IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideI, None);
        }
        else if (!IsSameRight(world, layer, x, y) && IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideI, Right);
        }
        else if (!IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideI, Down);
        }
        else if (IsSameRight(world, layer, x, y) && !IsSameLeft(world, layer, x, y) && !IsSameDown(world, layer, x, y) && !IsSameUp(world, layer, x, y))
        {
            return WithRotation(SideI, Left);
        }
        // the single tile
        else
        {
            return WithRotation(Single, (byte)((x + y) % 4));
        }
    }
    private byte WithRotation(byte id, byte rotation)
    {
        return (byte)(id + rotation * 10);
    }
    private bool IsSameRight(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return world.GetTileState(layer, x + 1, y).IsFullTile();
    }
    private bool IsSameLeft(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return world.GetTileState(layer, x - 1, y).IsFullTile();
    }
    private bool IsSameUp(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return world.GetTileState(layer, x, y + 1).IsFullTile();
    }
    private bool IsSameDown(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return world.GetTileState(layer, x, y - 1).IsFullTile();
    }
}
