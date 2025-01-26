using Galaxies.Core.Data;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Menu;
using Galaxies.Utill;

namespace Galaxies.Core.World.Tiles.Entity;
public class ChestTileEntity : TileEntity, IMenuProvider, IInventoryProvider
{
    private readonly TileInventory tileInv = new(9 * 4);
    public ChestTileEntity(AbstractWorld world, TilePos pos) : base(world, pos)
    {
    }
    public InventoryMenu CreateMenu(PlayerInventory inventory)
    {
        return new ChestInventoryMenu(inventory, this);
    }

    public IInventory GetInventory()
    {
        return tileInv;
    }

    public override void Read(DataSet data)
    {
        tileInv.LoadInventory(data);
    }

    public override void Save(DataSet data)
    {
        tileInv.SaveInventory(data);
    }
}
