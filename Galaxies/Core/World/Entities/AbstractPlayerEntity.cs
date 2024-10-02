using Galaxies.Client;
using Galaxies.Client.Render;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using Microsoft.Xna.Framework;

namespace Galaxies.Core.World.Entities;
// server: PlayerEntity ConnectPlayer
// client: ClientPlayer
public abstract class AbstractPlayerEntity : LivingEntity
{
    private static readonly PlayerRenderer s_playerRender = new PlayerRenderer();
    private InteractionManager InteractionManager;
    public PlayerInventory Inventory { get; private set; } = new();
    public int HitX = 0;
    public int HitY = 0;
    protected float homeX = 0;
    protected float homeY = 80;
    private float invincibleTicks = 3;
    protected bool isJumping;
    public int jumpTicks;
    public int jumpTimeout;
    public AbstractPlayerEntity(AbstractWorld world) : base(AllEntityTypes.PlayerEntity, world)
    {
        y = 140;
        speed = 10f;
        maxHealth = 100;
        health = 100;
        InteractionManager = new InteractionManager(world, this);
    }
    public override void Update(float dTime)
    {
        base.Update(dTime);
        if (invincibleTicks > 0)
        {
            invincibleTicks -= dTime;
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
        InteractionManager.Update(dTime);
    }
    public override void Render(IntegrationRenderer renderer, Color light)
    {
        s_playerRender.Render(renderer, this, 1, light);
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
        return 45f;
    }
    public void Jump(float value, float deltTime)
    {
        if (onGround && !isJumping)
        {
            vy += value * deltTime;
            isJumping = true;
        }
    }
    public override void Die()
    {
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
    public ItemPile GetItemOnHand()
    {
        return Inventory.Hotbar[Inventory.onHand];
    }
    public PlayerInventory GetInventory()
    {
        return Inventory;
    }
    public abstract void SendToClient(S2CPacket packet);

    public void Move(Direction moveDir, float deltaTime)
    {
        if (moveDir == Direction.Left)
        {
            direction = Direction.Left;
            if (vx > -speed)
            {
                vx -= speed * deltaTime;
            }
        }
        if (moveDir == Direction.Right)
        {
            direction = Direction.Right;
            if (vx < speed)
            {
                vx += speed * deltaTime;
            }
        }
        if (moveDir == Direction.Up)
        {
            Jump(GetJumpHeight(), deltaTime);
        }

    }
}
