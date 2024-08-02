using Galaxias.Client.Render;
using Galaxias.Core.Main;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Galaxias.Core.World.Entities;
public class InteractionManager
{
    private AbstractWorld world;
    public InteractionManager(AbstractWorld world)
    {
        this.world = world;
    }

    public void Update(GalaxiasClient galaxias, Camera camera)
    {
        if (galaxias.IsActive)
        {
            var state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed || state.RightButton == ButtonState.Pressed)
            {
                Vector2 p = camera.ScreenToWorldSpace(Mouse.GetState().Position);
                int x = Utils.Floor(p.X / 8);
                int y = Utils.Floor(-p.Y / 8);
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                if (state.LeftButton == ButtonState.Pressed && tileState.GetTile() != AllTiles.Air)
                {
                    if (tileState.GetTile().OnBreak(world, x, y, tileState))
                    {
                        world.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
                    }
                }
                else if (state.RightButton == ButtonState.Pressed && tileState.GetTile() == AllTiles.Air)
                {
                    if (AllTiles.Dirt.OnPlace(world, x, y, tileState))
                    {
                        world.SetTileState(TileLayer.Main, x, y, AllTiles.Dirt.GetDefaultState());
                    }
                }
            }
        }
    }
}
