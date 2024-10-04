using Galaxies.Core.World.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class DirtTile : Tile
{
    public DirtTile(TileSettings settings) : base(settings)
    {
        //AddProp(TileProperties.Smooth);
    }
    public override ItemPile GetDropItem()
    {
        return new ItemPile(AllItems.Dirt, 1);
    }
}
