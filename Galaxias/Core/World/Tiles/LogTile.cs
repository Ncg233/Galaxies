namespace Galaxias.Core.World.Tiles;
internal class LogTile : Tile
{
    public override bool CanCollide()
    {
        return false;
    }
    protected override bool IsFullTile()
    {
        return false;
    }
    public override bool OnBreak(AbstractWorld world, int x, int y, TileState state)
    {
        int yo = 0;
        for (; world.GetTileState(TileLayer.Main, x, y+yo).GetTile() == this; yo++)
        {
            world.SetTileState(TileLayer.Main, x, y + yo, AllTiles.Air.GetDefaultState());
        }
        for (int xo = -1;xo <= 1;xo++)
        {
            if (world.GetTileState(TileLayer.Main, x + xo, y + yo).GetTile() == AllTiles.Leaves)
            {
                world.SetTileState(TileLayer.Main, x + xo, y + yo, AllTiles.Air.GetDefaultState());
            }
        }
        if (world.GetTileState(TileLayer.Main, x, y + yo+1).GetTile() == AllTiles.Leaves)
        {
            world.SetTileState(TileLayer.Main, x, y + yo+1, AllTiles.Air.GetDefaultState());
        }
        return base.OnBreak(world, x, y, state);
    }
}
