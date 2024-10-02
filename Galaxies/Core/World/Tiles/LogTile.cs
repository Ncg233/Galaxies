namespace Galaxies.Core.World.Tiles;
internal class LogTile : Tile
{
    public LogTile(TileSettings settings) : base(settings)
    {
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
