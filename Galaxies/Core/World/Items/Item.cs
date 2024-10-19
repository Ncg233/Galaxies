using System.Numerics;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;

namespace Galaxies.Core.World.Items;
public class Item
{
    public readonly Properties properties;
    public static readonly int DefaultPileMaxCount = 64;
    public readonly PileMaxCount = properties.PileMaxCount;
    public readonly MaxDamage = properties.MaxDamage;
    public readonly CantBreak = properties.CantBreak;
    public Item(){
        this.properties = new Properties();
    }
    public Item(Properties properties){
        this.properties = properties;
    }
    public virtual bool Use(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return false;
    }
    public virtual bool HurtEntity(AbstractWorld world, LivingEntity player, LivingEntity entity)
    {
        return true;
    }
    public virtual bool OnUsingTime(AbstractWorld world, AbstractPlayerEntity player, ItemPile itemPile, float second)
    {
        return true;
    }
    public virtual bool ReleaseUse(AbstractWorld world, AbstractPlayerEntity player, ItemPile itemPile, float second)
    {
        return true;
    }
    public virtual bool OnHandTime(AbstractWorld world, AbstractPlayerEntity player, ItemPile itemPile)
    {
        return true;
    }
    public static class Properties{
        public int PileMaxCount = DefaultPileMaxCount;
        public int MaxDamage = 1;
        public CantBreak = false;
        public Properties(){
        }
        public Properties MaxCount(int i){
            if(MaxDamage != 1)return this;
            this.PileMaxCount = i;
            return this;
        }
        public Properties MaxDamage(int i){
            this.MaxDamage = i;
            this.PileMaxCount = 1;
            return this;
        }
        public Properties CantBreak(){
            this.CantBreak = true;
        }
    }
}