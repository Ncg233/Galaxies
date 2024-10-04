using Galaxies.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class GrassPlantTile : Tile
{
    public GrassPlantTile(TileSettings settings) : base(settings)
    {
    }
    public override bool CanStay(AbstractWorld world, int x, int y)
    {
        var downTile = world.GetTileState(TileLayer.Main, x, y - 1).GetTile();
        return downTile == AllTiles.GrassTile || downTile == AllTiles.Dirt;
    }
}
