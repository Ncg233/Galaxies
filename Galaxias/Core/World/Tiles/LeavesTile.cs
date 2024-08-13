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
    public override bool IsFullTile()
    {
        return false;
    }
}
