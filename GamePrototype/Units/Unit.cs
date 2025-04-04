using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;

namespace GamePrototype.Units
{
    public abstract class Unit
    {
        private const int INVENTORY_SIZE = 4;
        private uint _health;
        private uint _maxHealth;
        protected uint BaseDamage;
        protected Inventory Inventory;
        
        public string Name { get; private set; }
        public uint Health
        {
            get => _health;
            protected set => _health = value <= _maxHealth ? value : _maxHealth; //не записывать health больше максимума
        }

        public uint MaxHealth => _maxHealth;

        protected Unit(string name, uint health, uint maxHealth, uint baseDamage) 
        {
            Name = name;
            _health = health;
            _maxHealth = maxHealth;
            BaseDamage = baseDamage;
            Inventory = new Inventory(INVENTORY_SIZE);
        }

        public void ApplyDamage(uint damage)
        {
            var damageApplied = CalculateAppliedDamage(damage);
            if (_health <= damageApplied)// || (_health - damageApplied) <= 0) результат никогда не будет меньше 0 т.к. он uint
                _health = 0;
            else 
                _health -= damageApplied;

            DamageReceiveHandler();
        }

        protected abstract uint CalculateAppliedDamage(uint damage);
        
        protected virtual void DamageReceiveHandler() { }
        
        public abstract uint GetUnitDamage();

        public abstract void HandleCombatComplete();

        public virtual void AddItemToInventory(Item item) 
        {
            if (!Inventory.TryAdd(item)) 
            {
                Console.WriteLine($"Inventory of {Name} is full. Item: {item.Name} was discarded.");
            } else Console.WriteLine($"You'v collected item: {item.Name}.");
        }

        public void AddItemsFromUnitToInventory(Unit unit)
        {
            for (int i = 0; i < unit.Inventory.Items.Count; i++) 
            {
                if (!Inventory.TryAdd(unit.Inventory.Items[i]))
                {
                    Console.WriteLine($"Loot discarded: {unit.Inventory.Items[i].Name} because inventory is full.");
                    return;
                }
                else Console.WriteLine($"Loot added to inventory: {unit.Inventory.Items[i].Name}");
            }
        }
    }
}
