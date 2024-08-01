namespace Galaxias.Core.World.Tiles;
public class LeavesTile : Tile
{
    public override bool CanCollide()
    {
        return false;
    }
    protected override bool IsFullTile()
    {
        return false;
    }
}
