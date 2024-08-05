using ClientGalaxias.Client.Key;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Util;
namespace ClientGalaxias.Client;
public class ClientPlayer : Player
{
    public ClientPlayer(AbstractWorld world) : base(world)
    {

    }

    protected override void HandleMovement(float dTime)
    {
        float factor = KeyBind.Sprint.IsKeyDown() ? 1.5f : 1f;
        if (KeyBind.Left.IsKeyDown())
        {
            direction = Direction.Left;
            if (vx > -speed)
            {
                vx -= factor * speed * dTime;
            }
        }
        else if (KeyBind.Right.IsKeyDown())
        {
            direction = Direction.Right;
            if (vx < speed)
            {
                vx += factor * speed * dTime;
            }
        }
        if (KeyBind.Jump.IsKeyDown())
        {
            Jump(GetJumpHeight());
        }
        if (EnableJetpack() && KeyBind.Down.IsKeyDown() && !onGround)
        {
            vy = 0;
        }
        if (KeyBind.Home.IsKeyDown())
        {
            x = homeX;
            y = homeY;
            vx = 0;
            vy = 0;
        }
        if (KeyBind.SetHome.IsKeyDown())
        {
            homeX = x;
            homeY = y;
        }
        if (KeyBind.JetPack.IsKeyDown())
        {
            isJetpackEnable = !isJetpackEnable;
        }
        if (KeyBind.D1.IsKeyDown())this.GetInventory().onHand = 1;
        if (KeyBind.D2.IsKeyDown())this.GetInventory().onHand = 2;
        if (KeyBind.D3.IsKeyDown())this.GetInventory().onHand = 3;
        if (KeyBind.D4.IsKeyDown())this.GetInventory().onHand = 4;
        if (KeyBind.D5.IsKeyDown())this.GetInventory().onHand = 5;
        if (KeyBind.D6.IsKeyDown())this.GetInventory().onHand = 6;
        if (KeyBind.D7.IsKeyDown())this.GetInventory().onHand = 7;
        if (KeyBind.D8.IsKeyDown())this.GetInventory().onHand = 8;
        if (KeyBind.D9.IsKeyDown())this.GetInventory().onHand = 9;
    }
}
