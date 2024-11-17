using Galaxies.Client;
using Galaxies.Client.Render;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Entities;
public abstract class Entity
{
    public Guid Id = Guid.NewGuid();
    public float existedTime { get; private set; }
    public float nextSyncTime;
    public readonly EntityType Type;
    private float friction = 0.5f;
    public float X { get; private set; }
    public float Y { get; private set; }
    protected float lastY;
    protected float lastX;
    public float lastSyncX { get; protected set; }
    public float lastSyncY { get; protected set; }
    private float renderX;
    public float vx;
    public float vy;
    public Direction direction { get; set; }
    public HitBox hitbox { get; protected set; }
    public AbstractWorld world { get; protected set; }
    public float speed;
    protected float width;
    protected float height;
    //private EntityRenderer renderer;
    public bool onGround;
    public bool collidedHor;
    public bool collidedVert;

    public bool IsDead = false;
    public Entity(EntityType entity, AbstractWorld world)
    {
        Type = entity;
        this.world = world;
        hitbox = HitBox.Empty();
        //renderer = new EntityRenderer();
    }
    public virtual void Update(float dTime)
    {
        lastY = Y;
        existedTime += dTime;
        if (Y < -20)
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
    protected virtual void HandleCollision(float dTime)
    {
        float motionY = vy;
        float motionX = vx;
        HitBox ownBoxMotion = hitbox.Add(motionX, motionY);
        HitBox ownBox = hitbox;
        if (true)
        {
            List<HitBox> blockBoxes = [];
            for (int x = Utils.Floor(ownBoxMotion.minX); x < Utils.Ceil(ownBoxMotion.maxX); x++)
            {
                for (int y = Utils.Floor(ownBoxMotion.minY); y < Utils.Ceil(ownBoxMotion.maxY); y++)
                {

                    TileState id = world.GetTileState(TileLayer.Main, x, y);
                    if (id.GetTile().CanCollide())
                    {
                        var boxes = id.GetHitBoxes();
                        boxes.ForEach(b => {
                            blockBoxes.Add(b.Add(x, y));
                        });
                        //blockBoxes.AddRange(id.GetHitBoxes());
                    }
                }
            }
            List<Entity> entities = world.GetEntitiesInArea<Entity>(ownBoxMotion.Epxand(1, 2), e => e != this);
            foreach (Entity entity in entities)
            {
                OnCollideWithEntity(entity);
            }
            if (motionY != 0 && blockBoxes.Count != 0)
            {
                foreach (HitBox box in blockBoxes)
                {
                    if (!box.IsEmpty() && ownBox.IntersectsX(box))
                    {
                        if (motionY > 0 && ownBox.maxY <= box.minY)
                        {
                            float diff = box.minY - ownBox.maxY;
                            motionY = Math.Min(diff, motionY);
                        }
                        else if (motionY < 0 && ownBox.minY >= box.maxY)
                        {
                            float diff = box.maxY - ownBox.minY;
                            motionY = Math.Max(diff, motionY);
                        }
                    }
                    if (motionY == 0)
                    {
                        break;
                    }          
                }
            }
            if (motionX != 0 && blockBoxes.Count != 0)
            {
                foreach (HitBox box in blockBoxes)
                {
                    if (!box.IsEmpty() && ownBox.IntersectsY(box))
                    {
                        if (motionX > 0 && ownBox.maxX <= box.minX)
                        {
                            float diff = box.minX - ownBox.maxX;
                            motionX = Math.Min(diff, motionX);
                        }
                        else if (motionX < 0 && ownBox.minX >= box.maxX)
                        {
                            float diff = box.maxX - ownBox.minX;
                            motionX = Math.Max(diff, motionX);
                        }
                    }
                    if(motionX == 0)
                    {
                        break;
                    }
                    
                    
                }
            }
        }

        collidedVert = motionY != vy;
        collidedHor = motionX != vx;
        onGround = collidedVert && vy < 0;
        SetPos(X + motionX, Y + motionY);

        
    }

    public virtual void OnCollideWithEntity(Entity otherEntity)
    {
        
    }

    public virtual void Render(IntegrationRenderer renderer, Color color)
    {

    }
    public void SetPos(float x, float y)
    {
        this.X = x;
        this.Y = y;
        float u = GetWidth() / 2;
        float h = GetHeight();

        hitbox.SetPos(x - u, y, x + u, y + h);
        //SetHitBox(MakeHitBox());
    }

    private void SetHitBox(HitBox hit)
    {
        hitbox = hit;
    }

    public virtual float GetWidth()//1 -> 8Pixel
    {
        return 1;
    }
    public virtual float GetHeight()//1 -> 8Pixel
    {
        return 1;
    }
    public AbstractWorld GetWorld()
    {
        return world;
    }
    public void TpToOtherSide()
    {
        if (X >= world.Width)
        {
            SetPos(X - world.Width, Y);
        }
        else if (X < 0)
        {
            SetPos(X + world.Width, Y);
        }
    }
    //client use
    public float GetRenderX()
    {
        if (Main.GetInstance().GetPlayer() != null)
        {
            var playerX = Main.GetInstance().GetPlayer().X;
            if (Math.Abs(playerX - X) > world.Width / 2)//the distance between player and entity
            {
                if (playerX < world.Width / 2)
                {
                    return renderX = (X - world.Width) * GameConstants.TileSize;
                }
                else
                {
                    return renderX = (X + world.Width) * GameConstants.TileSize;
                }
            }
            else
            {
                return renderX = X * GameConstants.TileSize;
            }
        }
        return renderX;
    }
    public float GetRenderY()
    {
        return -Y * GameConstants.TileSize;
    }
}

