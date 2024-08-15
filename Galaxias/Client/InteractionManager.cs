using System;
using Galaxias.Client.Render;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Items;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;

namespace Galaxias.Client;
public class InteractionManager
{
    private AbstractWorld world;
    private float dtime = 0;
    public InteractionManager(AbstractWorld world)
    {
        this.world = world;
    }

    public void Update(Main.GalaxiasClient galaxias, Camera camera, float dTime)
    {
        if (galaxias.IsActive)
        {
            var state = Mouse.GetState();
            Vector2 p = camera.ScreenToWorldSpace(state.Position);
            int x = Utils.Floor(p.X / 8);
            int y = Utils.Floor(-p.Y / 8);
            Player player = galaxias.GetPlayer();
            player.HitX = x;
            player.HitY = y;
            //player.GetItemOnHand().GetItem().OnHandTime(galaxias.GetWorld(), player, player.GetItemOnHand());
            if (state.LeftButton == ButtonState.Pressed)
            {
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                if (tileState.GetTile() != AllTiles.Air && tileState.GetTile().OnBreak(world, x, y, tileState))
                {
                    world.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
                }

            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                dtime += dTime;
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                var item = player.GetItemOnHand().GetItem();
                if (item is TileItem)
                {
                    item.UseOnTile(galaxias.GetWorld(), player, tileState, x, y);
                }
                else
                {
                    item.Use(galaxias.GetWorld(), player, player.GetItemOnHand());
                    item.OnUsingTime(galaxias.GetWorld(), player, player.GetItemOnHand(), dtime);
                    tileState.OnUse(world, x, y, tileState, player);
                }
            }
            else if (state.RightButton == ButtonState.Released)
            {
                player.GetItemOnHand().GetItem().ReleaseUse(galaxias.GetWorld(), player, player.GetItemOnHand(), dtime);
                dtime = 0;
            }

        }
    }
}
