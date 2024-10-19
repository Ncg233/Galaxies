using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;

namespace Galaxies.Client.Render;
internal class SmoothStateInfo : IStateInfo
{
    public static byte Full = 0;
    public static byte SideIII = 1;
    public static byte SideII = 2;
    public static byte SideI = 3;
    public static byte Corner = 4;
    public static byte Single = 5;

    public static short None = 0;
    public static short Right = 90;
    public static short Down = 180;
    public static short Left = 270;
    //public static byte Any = 4;


    private List<Rectangle> sourceRect;
    public SmoothStateInfo(List<Rectangle> sourceRect)
    {
        this.sourceRect = sourceRect;
    }
    public Rectangle GetRenderRect(byte id)
    {
        return sourceRect[id];
    }

    public TileRenderInfo UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        var renderInfo = new TileRenderInfo();
        bool isSameDown = IsSameDown(world, layer, x, y);
        bool isSameUp = IsSameUp(world, layer, x, y);
        bool isSameRight = IsSameRight(world, layer, x, y);
        bool isSameLeft = IsSameLeft(world, layer, x, y);
        // the all sides are same
        if (isSameDown && isSameUp && isSameRight && isSameLeft)
        {
            return renderInfo.WithRotation(Full, (short)(Utils.Random.Next(0, 3) * 90));
            //return Full;
        }
        // the three sides are same
        else if (isSameRight && isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideIII, None);
        }
        else if (!isSameRight && isSameLeft && isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideIII, Right);
        }
        else if (isSameRight && isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideIII, Down);
        }
        else if (isSameRight && !isSameLeft && isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideIII, Left);
        }
        // the two sides are same （side）
        else if (!isSameRight && !isSameLeft && isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideII, None);
        }
        else if (isSameRight && isSameLeft && !isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideII, Left);
        }
        //(corner)
        else if (isSameRight && !isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(Corner, None);
        }
        else if (!isSameRight && isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(Corner, Right);
        }
        else if (!isSameRight && isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(Corner, Down);
        }
        else if (isSameRight && !isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(Corner, Left);
        }
        // the one side is same 
        else if (!isSameRight && !isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideI, None);
        }
        else if (!isSameRight && isSameLeft && !isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideI, Right);
        }
        else if (!isSameRight && !isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideI, Down);
        }
        else if (isSameRight && !isSameLeft && !isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideI, Left);
        }
        // the single tile
        else
        {
            return renderInfo.WithRotation(Single, (short)(Utils.Random.Next(0, 3) * 10));
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
