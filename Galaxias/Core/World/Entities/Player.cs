using System.CodeDom;
using Galasias.Core.World.Entities;
using Galasias.Core.World.Inventory;
using Galasias.Core.World.Items;
using Galaxias.Util;

namespace Galaxias.Core.World.Entities;
public class Player : LivingEntity
{
    private Inventory inventory;
    protected double homeX = 0;
    protected double homeY = 80;
    private int invincibleTicks = 50 * 3;
    protected bool isJumping;
    protected bool isJetpackEnable;
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
        //float factor = (KeyBind.Sprint.IsKeyDown() ? 1.5f : 1f);
        //if (KeyBind.Left.IsKeyDown())
        //{
        //    direction = Direction.Left;
        //    if (vx > -speed)
        //    {
        //        vx -= factor * speed * dTime;
        //    }
        //}
        //else if (KeyBind.Right.IsKeyDown())
        //{
        //    direction = Direction.Right;
        //    if (vx < speed)
        //    {
        //        vx += factor * speed * dTime;
        //    }
        //}
        //if (KeyBind.Jump.IsKeyDown())
        //{
        //    Jump(GetJumpHeight());
        //}
        //if (EnableJetpack() && KeyBind.Down.IsKeyDown() && !onGround)
        //{
        //    vy = 0;
        //}
        //if (KeyBind.Home.IsKeyDown())
        //{
        //    x = homeX;
        //    y = homeY;
        //    vx = 0;
        //    vy = 0;
        //}
        //if (KeyBind.SetHome.IsKeyDown())
        //{
        //    homeX = x;
        //    homeY = y;
        //}
        //if (KeyBind.JetPack.IsKeyDown())
        //{
        //    isJetpackEnable = !isJetpackEnable;
        //}
    }
    protected int GetJumpTimeout()
    {
        return 3;
    }
    protected float GetJumpHeight()
    {
        return EnableJetpack() ? 0.04f : 0.5f;
    }
    protected bool EnableJetpack()
    {
        return isJetpackEnable;
    }
    public void Jump(double value)
    {
        if (EnableJetpack() || (onGround && !isJumping))
        {
            vy += value;
            if (!EnableJetpack())
            {
                isJumping = true;
            }
        }
    }
    public override void Die()
    {
        //GalaxiasClient.GetInstance().Exit();
    }
    public override void Hurt(float amout)
    {
        if (invincibleTicks <= 0)
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
    public Item GetItemOnHand(){
        return inventory.QuickBar[inventory.onHand];
    }
    public Inventory GetInventory(){
        return inventory;
    }
}
