using System;
using System.Numerics;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;

namespace Galaxies.Core.World.Items;
public class Item
{
    public readonly Properties properties;
    public static readonly int DefaultPileMaxCount = 64;
    public readonly int PileMaxCount;
    public readonly int MaxDamage;
    public readonly bool CantBreak;
    public readonly bool IsDiggingTool;
    public Item(){
        properties = new Properties();
        PileMaxCount = properties.PileMaxCount;
        MaxDamage = properties.MaxDamage;
        CantBreak = properties.CantBreak;
        IsDiggingTool = properties.IsDiggingTool;   
    }
    public Item(Properties properties){
        this.properties = properties;
        PileMaxCount = properties.PileMaxCount;
        MaxDamage = properties.MaxDamage;
        CantBreak = properties.CantBreak;
        IsDiggingTool = properties.IsDiggingTool;
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

    public class Properties{
        public int PileMaxCount = DefaultPileMaxCount;
        public int MaxDamage = 1;
        public bool CantBreak = false;
        public bool IsDiggingTool = false;
        public Properties(){
        }
        public Properties SetMaxCount(int i){
            if(MaxDamage != 1)return this;
            PileMaxCount = i;
            return this;
        }
        public Properties SetMaxDamage(int i){
            MaxDamage = i;
            PileMaxCount = 1;
            return this;
        }
        public Properties SetCantBreak(){
            CantBreak = true;
            return this;
        }
        public Properties SetDiggingTool()
        {
            IsDiggingTool = true;
            return this;
        }
    }
}