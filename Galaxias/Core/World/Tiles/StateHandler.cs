namespace Galaxias.Core.World.Tiles;
internal class StateHandler
{
    private readonly Tile tile;

    private TileState defaultState;
    public StateHandler(Tile tile)
    {
        this.tile = tile;

        defaultState = new TileState(this.tile);
    }

    public TileState getDefaultState()
    {
        return defaultState;
    }
}
