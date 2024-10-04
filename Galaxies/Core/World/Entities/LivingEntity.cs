using Galaxies.Core.World;

namespace Galaxies.Core.World.Entities;
public class LivingEntity : Entity
{
    public float health { get; protected set; }
    public float maxHealth { get; protected set; }
    protected bool isFalling;
    protected float fallDistance = 0;
    private bool lastOnGround = true;
    public LivingEntity(EntityType type, AbstractWorld world) : base(type, world)
    {

    }
    public override void Update(float dTime)
    {
        HandleFalling();
        HandleHealth();
        base.Update(dTime);
    }
    public virtual void Hurt(float amout)
    {
        health -= amout;
    }
    protected float EvalFallDamage()
    {
        if (fallDistance > 20)
        {
            return fallDistance / 1.5f;
        }
        return 0;
    }
    protected virtual void HandleHealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void HandleFalling()
    {
        if (!lastOnGround && onGround)
        {
            Hurt(EvalFallDamage());
        }
        lastOnGround = onGround;
        if (lastY > y && !onGround)
        {
            isFalling = true;
            fallDistance += lastY - y;
        }
        else
        {
            isFalling = false;
            fallDistance = 0;
        }
    }
}