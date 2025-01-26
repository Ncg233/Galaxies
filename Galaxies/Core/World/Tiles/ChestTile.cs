using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles.Entity;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Galaxies.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class ChestTile : MultiTile, ITileEntityProvider
{
    public ChestTile(TileSettings settings) : base(2, 2, settings)
    {

    }
    public TileEntity CreateTileEntity(AbstractWorld world, TileState tileState, TilePos pos)
    {
        return new ChestTileEntity(world, pos);
    }
    public override void OnUse(TileState tileState, AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        if (world.IsClient) { 
            return;
        }
        TileEntity tileEntity = world.GetTileEntity(new TilePos(x, y, TileLayer.Main));
        if (tileEntity is ChestTileEntity entity){
            Log.Info("open chest");
            player.OpenInventoryMenu(entity);
        }
    }
    public override TileRenderType GetRenderType()
    {
        return TileRenderType.BottomCorner;
    }
}

