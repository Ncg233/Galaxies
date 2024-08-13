using System.CodeDom;
using Galasias.Core.World.Entities;
using Galasias.Core.World.Inventory;
using Galasias.Core.World.Items;
using Galaxias.Util;

namespace Galaxias.Core.World.Entities;
public class Player : LivingEntity
{
    private PlayerInventory inventory = new();
    public int HitX = 0;
    public int HitY = 0;
    protected double homeX = 0;
    protected double homeY = 80;
    private int invincibleTicks = 50 * 3;
    protected bool isJumping;
    protected bool isJetpackEnable;
    public int jumpTicks;
    public int jumpTimeout;
    public float factor;
    public Player(AbstractWorld world) : base(AllEntityTypes.PlayerEntity ,world)
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
        
    }
    protected int GetJumpTimeout()
    {
        return 5;
    }
    protected float GetJumpHeight()
    {
        return EnableJetpack() ? 0.04f : 30f;
    }
    protected bool EnableJetpack()
    {
        return isJetpackEnable;
    }
    public void Jump(double value, float deltTime)
    {
        if (EnableJetpack() || (onGround && !isJumping))
        {
            vy += value * deltTime;
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
    public ItemPile GetItemOnHand(){
        return inventory.Hotbar[inventory.onHand];
    }
    public PlayerInventory GetInventory(){
        return inventory;
    }
}
