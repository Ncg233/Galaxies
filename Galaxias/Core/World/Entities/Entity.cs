using Galaxias.Client;
using Galaxias.Client.Render;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Galaxias.Core.World.Entities;
public abstract class Entity
{
    public float existedTime { get; private set; }
    public float nextSyncTime;
    public readonly EntityType Type;
    private float friction = 0.5f;
    public float x { get; protected set; }
    public float y { get; protected set; }
    protected float lastY;
    protected float lastX;
    public float lastSyncX { get; protected set; }
    public float lastSyncY { get; protected set; }
    private float renderX;
    public float vx;
    public float vy;
    public Direction direction { get; set; }
    public HitBox hitbox { get; protected set; } = HitBox.Empty();
    public AbstractWorld world { get; protected set; }
    public float speed;
    //private EntityRenderer renderer;
    public bool onGround;
    public bool collidedHor;
    public bool collidedVert;
    
    public bool IsDead;
    public Entity(EntityType entity, AbstractWorld world)
    {
        Type = entity;
        this.world = world;
        //renderer = new EntityRenderer();
    }
    public virtual void Update(float dTime)
    {
        lastY = y;
        existedTime += dTime;
        if (y < -20)
        {
            Die();
        }
        PreMovement(dTime);
        HandleMovement(dTime);
        HandleCollision(dTime);
        TpToOtherSide();
        if (onGround)
        {
            vy = 0;
        }
        if (!world.IsClient)
        {

        }
    }
    public virtual void PreMovement(float dTime)
    {
        vx *= friction;
        if (vy > -3) vy -= GameConstants.Gravity * dTime;
    }
    public virtual void Die()
    {

    }
    public void SetDead()
    {
        IsDead = true;
    }
    protected virtual void HandleMovement(float dTime)
    {

    }
    private void HandleCollision(float dTime)
    {
        float motionY = vy;
        float motionX = vx;
        HitBox ownBoxMotion = hitbox.Copy().Add(motionX, motionY);
        HitBox ownBox = hitbox;
        if (true)
        {
            List<HitBox> blockBoxes = new();
            for (int x = Utils.Floor(ownBoxMotion.minX); x < Utils.Ceil(ownBoxMotion.maxX); x++)
            {
                for (int y = Utils.Floor(ownBoxMotion.minY); y < Utils.Ceil(ownBoxMotion.maxY); y++)
                {

                    TileState id = world.GetTileState(TileLayer.Main, x, y);
                    if (id.GetTile().CanCollide())
                    {
                        blockBoxes.Add(new HitBox(x, y, x + 1, y + 1));
                    }
                }
            }

            if (motionY != 0 && blockBoxes.Count != 0)
            {
                foreach (HitBox box in blockBoxes)
                {
                    if (!box.IsEmpty() && ownBox.intersectsX(box))
                    {
                        if (motionY > 0 && ownBox.maxY <= box.minY)
                        {
                            float diff = box.minY - ownBox.maxY;
                            if (diff < motionY)
                            {
                                motionY = diff;
                            }
                        }
                        else if (motionY < 0 && ownBox.minY >= box.maxY)
                        {
                            float diff = box.maxY - ownBox.minY;
                            if (diff > motionY)
                            {
                                motionY = diff;
                            }
                        }
                    }
                }
            }
            if (motionX != 0 && blockBoxes.Count != 0)
            {
                foreach (HitBox box in blockBoxes)
                {
                    if (!box.IsEmpty() && ownBox.intersectsY(box))
                    {
                        if (motionX > 0 && ownBox.maxX <= box.minX)
                        {
                            float diff = box.minX - ownBox.maxX;
                            if (diff < motionX)
                            {
                                motionX = diff;
                            }
                        }
                        else if (motionX < 0 && ownBox.minX >= box.maxX)
                        {
                            float diff = box.maxX - ownBox.minX;
                            if (diff > motionX)
                            {
                                motionX = diff;
                            }
                        }
                    }
                }
            }
        }

        collidedVert = motionY != vy;
        collidedHor = motionX != vx;
        onGround = collidedVert && vy < 0;
        SetPos(x + motionX, y + motionY);
    }
    public virtual void Render(IntegrationRenderer renderer, Color light)
    {

    }
    public void SetPos(float x, float y)
    {
        this.x = x;
        this.y = y;
        SetHitBox(MakeHitBox());
    }

    private void SetHitBox(HitBox hit)
    {
        hitbox = hit;
    }
    private HitBox MakeHitBox()
    {
        float u = GetWidth() / 2;
        float h = GetHeight();
        return new HitBox(x - u, y, x + u, y + h);
    }

    public virtual float GetWidth()//1 -> 8Pixel
    {
        return 1;
    }
    public virtual float GetHeight()//1 -> 8Pixel
    {
        return 1;
    }
    public AbstractWorld GetWorld() {
        return world;
    }
    public void TpToOtherSide()
    {
        if (x >= world.Width)
        {
            SetPos(x - world.GetWidth(), y);
        }
        else if (x < 0)
        {
            SetPos(x + world.GetWidth(), y);
        }         
    }
    //client use
    public float GetRenderX()
    {
        if (Main.GetInstance().GetPlayer() != null)
        {
            var playerX = Main.GetInstance().GetPlayer().x;
            if (Math.Abs(playerX - x) > world.Width / 2)//the distance between player and entity
            {
                if (playerX < world.Width / 2)
                {
                    return renderX = (x - world.Width) * GameConstants.TileSize;
                }
                else
                {
                    return renderX = (x + world.Width) * GameConstants.TileSize;
                }
            }
            else
            {
                return renderX = x * GameConstants.TileSize;
            }
        }
        return renderX;
    }
    public float GetRenderY()
    {
        return -y * GameConstants.TileSize;
    }
}

