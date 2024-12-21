using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Tiles;
public abstract class MultiTile : Tile
{
    private readonly int Width;
    private readonly int Height;
    public MultiTile(int width, int height, TileSettings settings) : base(settings)
    {
        Width = width;
        Height = height;
    }
    public override bool CanPlaceThere(TileState state, AbstractWorld world, TileLayer placeLayer, int x, int y)
    {
        bool isTurn = state.GetFacing().IsHorTurn();
        if (isTurn)
        {
            for (int i = x; i > x - Width; i--)
            {
                for (int j = y; j < y + Height; j++)
                {
                    if(!world.GetTileState(placeLayer, i, j).IsAir())
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            for (int i = x; i < x + Width; i++)
            {
                for (int j = y; j < y + Height; j++)
                {
                    if (!world.GetTileState(placeLayer, i, j).IsAir())
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public override void OnDestoryed(TileState state, AbstractWorld world, int x, int y)
    {
        if (!state.IsMulti())
        {
            bool isTurn = state.GetFacing().IsHorTurn();
            if (isTurn)
            {
                for (int i = x; i > x - Width; i--)
                {
                    for (int j = y; j < y + Height; j++)
                    {
                        if (i != x || j != y)
                        {
                            world.DestoryTile(i, j);
                        }
                    }
                }

            }
            else
            {
                for (int i = x; i < x + Width; i++)
                {
                    for (int j = y; j < y + Height; j++)
                    {
                        if (i != x || j != y)
                        {
                            world.DestoryTile(i, j);
                        }
                    }
                }
            }
        }
    }
    public override void OnTilePlaced(TileState tileState, AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        if(!tileState.IsMulti())
        {
            bool isTurn = tileState.GetFacing().IsHorTurn();
            if (isTurn) {
                for (int i = x; i > x - Width; i--)
                {
                    for (int j = y; j < y + Height; j++)
                    {
                        if (i != x || j != y)
                        {
                            world.SetTileState(TileLayer.Main, i, j, new MultiState(tileState, x, y, i - x, j - y));
                        }
                    }
                }

            }
            else
            {
                for (int i = x; i < x + Width; i++)
                {
                    for (int j = y; j < y + Height; j++)
                    {
                        if (i != x || j != y)
                        {
                            world.SetTileState(TileLayer.Main, i, j, new MultiState(tileState, x, y, i - x, j - y));
                        }
                    }
                }
            }
        }
        
    }
    public override int GetRenderWidth(TileState tileState)
    {
        return Width * 8;
    }
    public override int GetRenderHeight(TileState tileState)
    {
        return Height * 8;
    }
    public override List<HitBox> GetHitBoxes(TileState tileState)
    {
        var isTurn = tileState.GetFacing().IsHorTurn();
        var hitBox = GetHitBox(tileState).GetCurrent(isTurn);
        if (tileState.IsMulti())
        {
            return [hitBox.GetSlicedBox(((MultiState)tileState).InnerX, ((MultiState)tileState).InnerY)];
        }else
        {
            return [hitBox.GetSlicedBox(0, 0)];
        }
    }
}
