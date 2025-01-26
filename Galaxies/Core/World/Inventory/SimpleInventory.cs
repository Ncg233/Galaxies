using Galaxies.Core.Data;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Inventory;
public class SimpleInventory : IInventory
{
    private readonly DefaultedList<ItemPile> items;
    public readonly int Size;
    public SimpleInventory(int size)
    {
        Size = size;
        items = new DefaultedList<ItemPile>(Size, ItemPile.Empty);
    }
    public ItemPile GetItemPile(int index)
    {
        return items.GetValue(index);
    }

    public void Set(int slotId, ItemPile pile)
    {
        items.SetValue(slotId, pile);
    }

    public void SaveInventory(DataSet set)
    {
        List<DataSet> list = [];
        for (int i = 0; i < items.Count(); i++)
        {
            ItemPile pile = items.GetValue(i);
            if (!pile.IsEmpty())
            {
                DataSet dataSet = new DataSet();
                dataSet.PutByte("slot", (byte)i);
                pile.Save(dataSet);
                list.Add(dataSet);
            }
        }
        set.PutList("itemList", list);
    }
    public void LoadInventory(DataSet set)
    {
        var list = set.GetData<List<DataSet>>("itemList");
        foreach (DataSet dataSet in list) {
            var slot = dataSet.GetData<byte>("slot");
            ItemPile pile = new(null, 0);
            pile.Load(dataSet);
            items.SetValue(slot, pile);
        }
    }
}
