using System.CodeDom;
using Galaxias.Core.World.Inventory;
using Galaxias.Core.World.Items;
using Galaxias.Util;

namespace Galaxias.Core.World.Entities;
public class Player : LivingEntity
{
    public PlayerInventory Inventory { get; private set; } = new();
    public int HitX = 0;
    public int HitY = 0;
    protected float homeX = 0;
    protected float homeY = 80;
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
        return EnableJetpack() ? 0.04f : 0.6f;
    }
    protected bool EnableJetpack()
    {
        return isJetpackEnable;
    }
    public void Jump(float value, float deltTime)
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
    public ItemPile GetItemOnHand(){
        return Inventory.Hotbar[Inventory.onHand];
    }
    public PlayerInventory GetInventory(){
        return Inventory;
    }
}
