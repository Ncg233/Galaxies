namespace Galaxias.Core.World.Tiles;
internal class AirTile : Tile
{
    public override bool IsFullTile()
    {
        return false;
    }
    public override bool CanCollide()
    {
        return false;
    }
}
