namespace Galaxies.Core.World.Tiles;
internal class AirTile : Tile
{
    public AirTile() : base(new TileSettings().SetAir())
    {

    }

    public override bool IsFullTile()
    {
        return false;
    }
    public override bool CanCollide()
    {
        return false;
    }
    public override TileRenderType GetRenderType()
    {
        return TileRenderType.Invisible;
    }
}
