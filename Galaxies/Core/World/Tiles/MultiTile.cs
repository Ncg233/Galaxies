using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles.State;

namespace Galaxies.Core.World.Tiles;
public abstract class MultiTile : Tile
{
    private int width;
    private int height;
    public MultiTile(int width, int height, TileSettings settings) : base(settings)
    {
        this.width = width;
        this.height = height;
    }
    public override void OnDestoryed(TileState state, AbstractWorld world, int x, int y)
    {
        if (!state.IsMulti())
        {
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    if (i != x || j != y)
                    {
                        world.DestoryTile(i, j);
                    }
                }
            }
        }
    }
    public override void OnTilePlaced(TileState tileState, AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        if(!tileState.IsMulti())
        {
            var multiState = new MultiState(tileState, x, y);
            bool isTurn = tileState.GetFacing().Effect == Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
            if (isTurn) {
                for (int i = x; i > x - width; i--)
                {
                    for (int j = y; j < y + height; j++)
                    {
                        if (i != x || j != y)
                        {
                            world.SetTileState(TileLayer.Main, i, j, multiState);
                        }
                    }
                }

            }
            else
            {
                for (int i = x; i < x + width; i++)
                {
                    for (int j = y; j < y + height; j++)
                    {
                        if (i != x || j != y)
                        {
                            world.SetTileState(TileLayer.Main, i, j, multiState);
                        }
                    }
                }
            }
        }
        
    }
    public override int GetRenderWidth(TileState tileState)
    {
        return width * 8;
    }
    public override int GetRenderHeight(TileState tileState)
    {
        return height * 8;
    }
}
