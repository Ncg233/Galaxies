using Galasias.Core.World.Entities;
using Galaxias.Client.Key;
using Galaxias.Core.Main;
using Galaxias.Util;

namespace Galaxias.Core.World.Entities;
public class Player : LivingEntity
{
    private double homeX = 0;
    private double homeY = 80;
    private int invincibleTicks = 50 * 3;
    protected bool isJumping;
    public int jumpTicks;
    public int jumpTimeout;
    public float factor;
    public Player(AbstractWorld world) : base(world)
    {
        y = 140;
        speed = 5f;
        maxHealth = 100;
        health = 100;
    }
    public override void Update(float dTime)
    {
        base.Update(dTime);
        if (invincibleTicks > 0)
        {
            invincibleTicks--;
        }
        if (isJumping)
        {
            if (jumpTicks > 0 && collidedVert)
            {
                vy = 0;
                isJumping = false;
                jumpTicks = 0;
                jumpTimeout = GetJumpTimeout();
            }
            else
            {
                jumpTicks++;
            }
        }
        
    }
    protected override void HandleMovement(float dTime)
    {
        float factor = (KeyBind.Sprint.IsKeyDown() ? 1.5f : 1f);
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
    }
    protected int GetJumpTimeout()
    {
        return 3;
    }
    private float GetJumpHeight()
    {
        return this.EnableJetpack() ? 0.04f : 0.5f;
    }
    private bool EnableJetpack()
    {
        return true;
    }
    public void Jump(double value)
    {
        if (this.EnableJetpack() || (onGround && !isJumping))
        {
            vy += value;
            if (!this.EnableJetpack())
            {
                isJumping = true;
            }
        }
    }
    public override void Die()
    {
        GalaxiasClient.GetInstance().Exit();
    }
    public override void Hurt(float amout)
    {
        if (this.invincibleTicks <= 0)
        {
            base.Hurt(amout);
        }
    }
    public override float GetWidth()
    {
        return 1.6f;
    }
    public override float GetHeight()
    {
        return 4;
    }
}
