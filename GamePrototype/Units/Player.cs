using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Collections.Generic;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {            
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon) 
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++) 
            {
                if (items[i] is EconomicItem economicItem) 
                {
                    if (UseEconomicItem(economicItem))
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem) 
            {
                if (_equipment.TryGetValue(equipItem.Slot, out var equipmentItem))
                {
                    if (equipmentItem is Weapon weapon)
                    {
                        if (weapon.Damage < (item as Weapon)!.Damage || (weapon.Damage == (item as Weapon)!.Damage && weapon.Durability < (item as Weapon)!.Durability))
                        {
                            _equipment.Remove(EquipSlot.Weapon);
                            Inventory.TryRemove(equipmentItem);
                            _equipment.TryAdd(equipItem.Slot, (item as Weapon)!);
                            base.AddItemToInventory(item);
                            Console.WriteLine($"Weapon {weapon.Name} [damage:{weapon.Damage},durability:{weapon.Durability}] was dropped and replaced by {(item as Weapon)!.Name} [damage:{(item as Weapon)!.Damage},durability:{(item as Weapon)!.Durability}]");
                        }
                    }
                    else 
                        if (equipmentItem is Armor armor)
                        {
                            if (armor.Defence < (item as Armor)!.Defence || (armor.Defence == (item as Armor)!.Defence && armor.Durability < (item as Armor)!.Durability))
                            {
                                _equipment.Remove(EquipSlot.Weapon);
                                Inventory.TryRemove(equipmentItem);
                                _equipment.TryAdd(equipItem.Slot, (item as Armor)!);
                                base.AddItemToInventory(item);
                                Console.WriteLine($"Armor {armor.Name} [defence:{armor.Defence},durability:{armor.Durability}] was dropped and replaced by {(item as Armor)!.Name} [defence:{(item as Armor)!.Defence},durability:{(item as Armor)!.Durability}]");
                            }
                        }
                }
                else
                {
                    _equipment.TryAdd(equipItem.Slot, equipItem);
                    base.AddItemToInventory(item);
                }
                return;
            }
            base.AddItemToInventory(item);
        }

        public void ReduceWeaponDurability(uint amount)
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                if (weapon.Durability <= amount) weapon.ReduceDurability(weapon.Durability); else weapon.ReduceDurability(amount);
                if (weapon.Durability == 0)
                {
                    Console.WriteLine("Player weapon was destroyed.");
                    _equipment.Remove(EquipSlot.Weapon);
                    Inventory.TryRemove(weapon);
                }
            }
        }

        private bool UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion) 
            {
                if (Health < MaxHealth)
                {
                    var was = Health;
                    Health += healthPotion.HealthRestore;
                    Console.WriteLine($"Health potion have been used. Health was {was}. Now = {Health} / {MaxHealth}.");
                    return true;
                }
                else return false;
            }
            if (economicItem is Grindstone grindStone)
            {
                if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
                {
                    if (weapon.Durability < weapon.MaxDurability)
                    {
                        var was = weapon.Durability;
                        weapon.Repair(4);
                        Console.WriteLine($"Grindstone have been used on weapon. Durability was {was}. Now = {weapon.Durability}.");
                        return true;
                    }
                    else return false;
                }
            }
            return false;
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armor, out var item) && item is Armor armor)
            {
                damage -= (uint)(damage * (armor.Defence / 100f));
                armor.ReduceDurability(1);
            }
            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++) 
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}
