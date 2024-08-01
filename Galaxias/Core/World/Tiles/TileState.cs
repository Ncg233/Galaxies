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
}
