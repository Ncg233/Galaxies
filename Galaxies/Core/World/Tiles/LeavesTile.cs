namespace Galaxies.Core.World.Tiles;
public class LeavesTile : Tile
{
    public LeavesTile(TileSettings settings) : base(settings)
    {
    }

    public override TileRenderType GetRenderType()
    {
        return TileRenderType.BottomCenter;
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
