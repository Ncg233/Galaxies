using Galaxies.Client.Render;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Menu;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.Entity;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Entities;
// server: PlayerEntity ConnectPlayer
// client: ClientPlayer
public abstract class AbstractPlayerEntity : LivingEntity
{
    private static readonly PlayerRenderer s_playerRender = new();
    public InteractionManager InteractionManager;
    public PlayerInventory Inventory { get; private set; } = new();
    public PlayerInventoryMenu container;
    protected InventoryMenu currentInvMenu;
    public bool IsWalking { get; protected set; }

    public int HitX = 0;
    public int HitY = 0;
    protected float homeX = 0;
    protected float homeY = 80;
    private float invincibleTicks = 3;
    protected bool isJumping;
    public int jumpTicks;
    public int jumpTimeout;
    public AbstractPlayerEntity(AbstractWorld world, Guid id) : base(AllEntityTypes.PlayerEntity, world)
    {
        SetPos(0, 140);
        Id = id;
        speed = 10f;
        maxHealth = 100;
        health = 100;
        container = new PlayerInventoryMenu(Inventory);
        
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
        if (collidedHor)
        {
            var ontoBox = hitbox.Add(direction.X / 100f, 1);
            bool canOnto = true;
            for (int x = Utils.Floor(ontoBox.minX); x < Utils.Ceil(ontoBox.maxX); x++)
            {
                for (int y = Utils.Floor(ontoBox.minY); y < Utils.Ceil(ontoBox.maxY); y++)
                {
                    TileState id = world.GetTileState(TileLayer.Main, x, y);
                    if (id.GetTile().CanCollide())
                    {
                        canOnto = false;
                        break;
                    }
                }
            }
            if (canOnto && onGround)
            {
                vy += 0.3f;
            }
            else
            {
                vx = 0;
            }
        }
        if (Math.Abs(vx) > 0.001f)
        {
            IsWalking = true;
        }
        else IsWalking = false;
        InteractionManager?.Update(dTime);
    }
    public override void Render(IntegrationRenderer renderer, Color color)
    {
        s_playerRender.Render(renderer, this, 1, color);
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
        return 6 / 8f;
    }
    public void Jump(float value)
    {
        if (onGround && !isJumping)
        {
            vy += value;
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
        return Inventory.GetItemOnHand();
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
            Jump(GetJumpHeight());
        }

    }

    public abstract bool OpenInventoryMenu(IMenuProvider provider);

    public class PlayerRenderer : EntityRenderer<AbstractPlayerEntity>
    {
        private static Color ClothesColor = Color.Blue;
        private static Color FaceColor = new Color(245, 203, 148);
        private static Color EyeColor = new Color(180, 41, 37);
        private static Color HairColor = new Color(250, 209, 81);
        public override void LoadContent()
        {

        }

        public override void Render(IntegrationRenderer renderer, AbstractPlayerEntity player, int scale, Color colors)
        {
            var x = player.GetRenderX();
            var y = player.GetRenderY();
            var width = 16;
            var height = 32;
            bool isTurn = player.direction == Direction.Left;
            //BODY
            renderer.Draw("Textures/Entities/Player/player_leg", x, y,
                width / 2f, height, colors,
                source: GetSource(player),
                effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            
            renderer.Draw("Textures/Entities/Player/player_body", x, y,
                width / 2f, height, colors,
                effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            renderer.Draw("Textures/Entities/Player/player_head", x, y,
                width / 2f, height, Utils.MultiplyNoA(colors, FaceColor),
                effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            renderer.Draw("Textures/Entities/Player/player_eye", x, y,
                width / 2f, height, Utils.MultiplyNoA(colors, EyeColor),
                effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            renderer.Draw("Textures/Entities/Player/player_hair", x, y,
                width / 2f, height, Utils.MultiplyNoA(colors, Color.Orange),
                effects: isTurn ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            //HELD ITEM
            var item = player.GetItemOnHand();
            if (isTurn)
            {
                ItemRenderer.RenderInWorld(renderer, item, x - 0.75f, y - 4, colors);
            }
            else
            {
                ItemRenderer.RenderInWorld(renderer, item, x, y - 4, colors);

            }
        }

        private Rectangle? GetSource(AbstractPlayerEntity player)
        {
            if (player.IsWalking)
            {
                int count = 5;
                long runningTime = DateTime.UtcNow.Ticks / 1000 % (count * 1200);

                long accum = 0;
                for (int i = 0; i < count; i++)
                {
                    accum += 1200;
                    if (accum >= runningTime)
                    {
                        return new Rectangle(16 + 16 * i, 0, 16, 32);
                    }
                }
            }
            else
            {
                return new Rectangle(0, 0, 16, 32);
            }
            return null;

        }
    }
}
