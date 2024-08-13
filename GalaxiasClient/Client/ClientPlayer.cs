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
            Jump(GetJumpHeight(), dTime);
        }
        if (EnableJetpack() && KeyBind.Down.IsKeyDown() && !onGround)
        {
            vy = 0;
        }
        if (KeyBind.Home.IsKeyPressed())
        {
            x = homeX;
            y = homeY;
            vx = 0;
            vy = 0;
        }
        if (KeyBind.SetHome.IsKeyPressed())
        {
            homeX = x;
            homeY = y;
        }
        if (KeyBind.JetPack.IsKeyPressed())
        {
            isJetpackEnable = !isJetpackEnable;
        }
        if (KeyBind.D1.IsKeyDown()) GetInventory().onHand = 0;
        if (KeyBind.D2.IsKeyDown()) GetInventory().onHand = 1;
        if (KeyBind.D3.IsKeyDown()) GetInventory().onHand = 2;
        if (KeyBind.D4.IsKeyDown()) GetInventory().onHand = 3;
        if (KeyBind.D5.IsKeyDown()) GetInventory().onHand = 4;
        if (KeyBind.D6.IsKeyDown()) GetInventory().onHand = 5;
        if (KeyBind.D7.IsKeyDown()) GetInventory().onHand = 6;
        if (KeyBind.D8.IsKeyDown()) GetInventory().onHand = 7;
        if (KeyBind.D9.IsKeyDown()) GetInventory().onHand = 8;
    }
}
