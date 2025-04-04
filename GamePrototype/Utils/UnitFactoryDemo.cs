using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
            player.AddItemToInventory(new Weapon(10, 50, "Sword"));
            player.AddItemToInventory(new Armor(10, 15, "Armor"));
            player.AddItemToInventory(new HealthPotion("Potion"));
            return player;
        }

        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);
    }
}
