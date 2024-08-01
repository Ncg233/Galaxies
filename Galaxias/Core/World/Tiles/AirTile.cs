namespace Galaxias.Core.World.Tiles;
internal class AirTile : Tile
{
    protected override bool IsFullTile()
    {
        return false;
    }
    public override bool CanCollide()
    {
        return false;
    }
}
