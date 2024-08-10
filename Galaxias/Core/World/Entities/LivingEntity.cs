using Galaxias.Core.World;
using Galaxias.Core.World.Entities;

namespace Galasias.Core.World.Entities;
public class LivingEntity : Entity
{
    public float health { get; protected set; }
    public float maxHealth { get; protected set; }
    protected bool isFalling;
    protected double fallDistance = 0;
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
        this.health -= amout;
    }
    protected float EvalFallDamage()
    {
        if (fallDistance > 5)
        {
            return (float)((float)fallDistance / 1.5);
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
        if (lastOnGround == false && onGround == true)
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