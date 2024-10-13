﻿using Galaxies.Core.World.Entities;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class FurnitureTile : Tile
{
    public FurnitureTile() : base(new TileSettings().SetCanCollide(false).SetFullTile(false))
    {
        
    }
    public override void AddProp(StateHandler handler)
    {
        handler.AddFacing(Facing.Right);
        handler.AddFacing(Facing.Left);
    }
    public override TileRenderType GetRenderType()
    {
        return TileRenderType.BottomCorner;
    }
    public override TileState GetPlaceState(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return GetDefaultState().ChangeFacing(player.direction == Direction.Right ? Facing.Right : Facing.Left);
    }
}