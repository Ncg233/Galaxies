using Galaxies.Core.World.Tiles.State;

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
    public override int GetRenderWidth(TileState tileState)
    {
        return 5 * 8;
    }
    public override int GetRenderHeight(TileState tileState)
    {
        return 5 * 8;
    }
}
