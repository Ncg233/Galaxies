using Galaxias.Core.World.Entities;
using System;
using System.Runtime.InteropServices.Marshalling;

namespace Galaxias.Core.World.Tiles;
public class TileState
{
    private readonly Tile tile;

    public TileState(Tile tile)
    {
        this.tile = tile;
    }

    public Tile GetTile()
    {
        return tile;
    }
    public int GetLight()
    {
        return tile.GetLight(this);
    }

    public bool IsFullTile()
    {
        return tile.IsFullTile();
    }
    public bool IsAir()
    {
        return tile.IsAir();
    }

    public void OnNeighborChanged(AbstractWorld abstractWorld, int x, int y, Tile changedTile)
    {
        tile.OnNeighborChanged(this, abstractWorld, x, y, changedTile);
    }
}
