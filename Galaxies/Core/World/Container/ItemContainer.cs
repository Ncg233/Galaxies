using Galaxies.Core.World.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Container;
public class ItemContainer : IEnumerable<Slot>
{
    public readonly AbstractPlayerEntity Player;
    private readonly List<Slot> slots = [];
    
    public ItemContainer(AbstractPlayerEntity player)
    {
        Player = player;
    }
    public void AddPlayerInventory(AbstractPlayerEntity player, int startX, int startY)
    {
        for (int y = 0; y < 4; y++) {
            for (int x = 0; x < 9; x++) {
                slots.Add(new Slot(player.GetInventory(), y * 9 + x, x * 20, y * 20));
            }
        }
    }
    public Slot GetSlot(int index)
    {
        return slots[index];
    }

    public IEnumerator<Slot> GetEnumerator()
    {
        return slots.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
