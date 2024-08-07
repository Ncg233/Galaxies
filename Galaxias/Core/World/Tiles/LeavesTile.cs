namespace Galaxias.Core.World.Tiles;
public class LeavesTile : Tile
{
    public override TileRenderType GetRenderType()
    {
        return TileRenderType.Bottom;
    }
    public override bool CanCollide()
    {
        return false;
    }
    protected override bool IsFullTile()
    {
        return false;
    }
}
