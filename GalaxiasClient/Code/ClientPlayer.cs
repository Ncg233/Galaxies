using Client.Code.Key;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Code;
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
    }
}
