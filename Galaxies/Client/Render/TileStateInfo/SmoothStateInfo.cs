using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Newtonsoft.Json.Linq;

namespace Galaxies.Client.Render.TileStateInfo;
internal class SmoothStateInfo : IStateInfo
{
    public static byte Full = 0;
    public static byte SideIII = 1;
    public static byte SideII = 2;
    public static byte SideI = 3;
    public static byte Corner = 4;
    public static byte Single = 5;

    public static short None = 0;
    public static short r_90d = 90;
    public static short r_180d = 180;
    public static short r_270d = 270;
    private bool shouldRandom;
    //public static byte Any = 4;
    private List<Rectangle> rectList;
    public SmoothStateInfo()
    {
    }
    public Rectangle GetRenderRect(byte id)
    {
        return rectList[id];
    }
    public void Deserialize(JObject prop, Rectangle[,] sourceRect)
    {
        JsonUtils.TryGetValue(prop, "random", out shouldRandom, true);

        rectList = [];
        var rect = JsonUtils.GetValue<int[]>(prop, "full");
        rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

        rect = JsonUtils.GetValue<int[]>(prop, "sideIII");
        rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

        rect = JsonUtils.GetValue<int[]>(prop, "sideII");
        rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

        rect = JsonUtils.GetValue<int[]>(prop, "sideI");
        rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

        rect = JsonUtils.GetValue<int[]>(prop, "corner");
        rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

        rect = JsonUtils.GetValue<int[]>(prop, "single");
        rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);
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
            return renderInfo.WithRotation(Full, shouldRandom ? (short)(Utils.Random.Next(0, 3) * 90) : (short)0);
            //return Full;
        }
        // the three sides are same
        else if (isSameRight && isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideIII, None);
        }
        else if (!isSameRight && isSameLeft && isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideIII, r_90d);
        }
        else if (isSameRight && isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideIII, r_180d);
        }
        else if (isSameRight && !isSameLeft && isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideIII, r_270d);
        }
        // the two sides are same （side）
        else if (!isSameRight && !isSameLeft && isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideII, None);
        }
        else if (isSameRight && isSameLeft && !isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideII, r_270d);
        }
        //(corner)
        else if (isSameRight && !isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(Corner, None);
        }
        else if (!isSameRight && isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(Corner, r_90d);
        }
        else if (!isSameRight && isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(Corner, r_180d);
        }
        else if (isSameRight && !isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(Corner, r_270d);
        }
        // the one side is same 
        else if (!isSameRight && !isSameLeft && isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideI, None);
        }
        else if (!isSameRight && isSameLeft && !isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideI, r_90d);
        }
        else if (!isSameRight && !isSameLeft && !isSameDown && isSameUp)
        {
            return renderInfo.WithRotation(SideI, r_180d);
        }
        else if (isSameRight && !isSameLeft && !isSameDown && !isSameUp)
        {
            return renderInfo.WithRotation(SideI, r_270d);
        }
        // the single tile
        else
        {
            return renderInfo.WithRotation(Single, shouldRandom ? (short)(Utils.Random.Next(0, 3) * 90) : (short)0);
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

    public TileRenderInfo DefaultInfo()
    {
        var renderInfo = new TileRenderInfo();
        return renderInfo.WithRotation(Single, None);
    }
}
