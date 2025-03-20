namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dungeon dungeon = new Dungeon();
            dungeon.ShowRooms();
        }
    }

    public class Unit
    {
        private Interval _damage;
        public string Name { get; }
        private float Health => _health; //только для чтения, не задается - будет всегда 0
        private float _health { get; set; }
        private Interval Damage => _damage;
        float Armor;
        public Unit() : this(name: "Unknown Unit")
        {
        }

        public Unit(string name)
        {
            Name = name;
            Armor = 0.6f;
        }
        public Unit(string name, int minDamage, int maxDamage) : this(name: name)
        {
            _damage.Min = minDamage;
            _damage.Max = maxDamage;
        }

        public float GetRealHealth()
        {
            return Health * (1f + Armor);
        }

        public bool SetDamage(float value)
        {
            _health -= value * Armor;
            return _health <= 0f ? true : false;
        }
    }

    public class Weapon
    {
        private Interval _damage;
        public string Name { get; }
        private Interval Damage => _damage;
        private float Durability { get; }

        public Weapon(string name)
        {
            Name = name;
            Durability = 1f;
        }
        public Weapon(string name, int minDamage, int maxDamage) : this(name)
        {
            SetDamageParams(minDamage, maxDamage);
        }

        public void SetDamageParams(int minDamage, int maxDamage)
        {
            _damage.Max = maxDamage;
            _damage.Min = minDamage;
            if (minDamage > maxDamage)
            {
                int temp = minDamage;
                _damage.Min = maxDamage;
                _damage.Max = temp;
                Console.WriteLine($"Ошибка: minDamage > maxDamage для оружия {Name}");
            }
            if (minDamage < 1)
            {
                _damage.Min = 1;
                Console.WriteLine($"Ошибка: минимальное значение урона для оружия {Name} меньше 1. Установлено 1.");
            }
            if (maxDamage <= 1)
            {
                _damage.Max = 10;
                Console.WriteLine($"Ошибка: максимальное значение урона для оружия {Name} меньше или равно 1. Установлено 10.");
            }
        }

        public int GetDamage()
        {
            return (Damage.Max + Damage.Min) / 2;
        }

    }

    struct Interval
    {
        public int Min;
        public int Max;
        private Random rnd;
        public Interval(int min, int max)
        {
            Min = min;
            Max = max;
            rnd = new Random();
            if (Min > Max)
            {
                int temp = Min;
                Min = Max;
                Max = temp;
                Console.WriteLine($"Ошибка: Min > Max для Interval.");
            }

            if (Min < 0)
            {
                Min = 0;
                Console.WriteLine($"Ошибка: Min < 0 для Interval. Изменен на 0.");
            }
            if (Max < 0)
            {
                Max = 0;
                Console.WriteLine($"Ошибка: Max < 0 для Interval. Изменен на 0.");
            }
            if (Min == Max)
            {
                Max += 10;
                Console.WriteLine($"Ошибка: Min = Max для Interval. Max увеличен на 10.");
            }
        }

        public int Get()
        {
            return rnd.Next(Min, Max);
        }
    }

    public struct Room
    {
        public Unit _unit { get; set; }
        public Weapon _weapon { get; set; }

        public Room(Unit unit, Weapon weapon)
        {
            _unit = unit;
            _weapon = weapon;
        }
    }

    public class Dungeon
    {
        Room[] rooms;
        public Dungeon()
        {
            rooms = new Room[]
            {
                new Room(new Unit("Unit1"),new Weapon("Bow",2,4)),
                new Room(new Unit("Unit2"), new Weapon("Bazooka", 7, 10)),
                new Room(new Unit("Unit3"), new Weapon("Pistol", 3, 5)),
                new Room(new Unit("Unit4"), new Weapon("Hands", 1, 2))
            };
        }

        public void ShowRooms()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                var room = rooms[i];
                Console.WriteLine("Unit of room: " + room._unit.Name);
                Console.WriteLine("Weapon of room: " + room._weapon.Name);
                Console.WriteLine("—");
            }
        }
    }
}