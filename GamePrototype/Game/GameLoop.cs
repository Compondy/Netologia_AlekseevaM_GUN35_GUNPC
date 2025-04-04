using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit? _player;
        private DungeonRoom? _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        
        public void StartGame() 
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");
            _dungeon = DungeonBuilder.BuildDungeon();
            Console.WriteLine("Enter your name");
            var name = Console.ReadLine();
            Console.WriteLine($"Hello {name}");
            _player = UnitFactoryDemo.CreatePlayer(name!);
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;
            
            while (currentRoom!.IsFinal == false) 
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success) 
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true) 
                {
                    if (Enum.TryParse<Direction>(Console.ReadLine(), out var direction) && currentRoom!.Rooms.TryGetValue(direction, out var nextRoom)) //проверка на доступное направление исправлена
                    {
                        currentRoom = nextRoom;
                        Console.WriteLine($"You've entered room {currentRoom.Name}.");
                        if (currentRoom.Enemy == null && currentRoom.Loot == null)
                        Console.WriteLine($"Inside you'v found nothing.");
                        else
                        {
                            Console.WriteLine($"Inside you'v found:");
                            if (currentRoom.Enemy!= null) Console.WriteLine($"Enemy: {currentRoom.Enemy.Name}");
                            if (currentRoom.Loot != null) Console.WriteLine($"Loot: {currentRoom.Loot.Name}");
                        }
                            break;
                    }
                    else 
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player!.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player!.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Enemy != null) 
            {
                if (_combatManager.StartCombat(_player!, currentRoom.Enemy) == _player)
                {
                    _player!.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else 
                {
                    success = false;
                }
            }

            if (currentRoom.Loot != null)
            {
                _player!.AddItemToInventory(currentRoom.Loot);
            }


            void LootEnemy(Unit enemy)
            {
                _player!.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int) room.Key}\t");
            }
        }

        
        #endregion
    }
}
