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
        private string _name { get; set; }
        public string Name => _name;
        public float Health => _health;
        private float _health { get; set; }
        public Interval Damage => _damage;
        private Interval _damage { get; set; }
        public float Armor => _armor;
        private float _armor { get; set; }

        public Unit() : this(name: "Unknown Unit")
        {
            
        }

        public Unit(string name)
        {
            _name = name;
            _armor = 0.6f;
        }
        public Unit(string name, int minDamage, int maxDamage) : this(name: name)
        {
            _damage = new Interval(minDamage,maxDamage);
        }

        public float GetRealHealth()
        {
            return _health * (1f + _armor);
        }

        public bool SetDamage(float value)
        {
            _health -= value * _armor;
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
            _damage = new Interval(minDamage, maxDamage);
        }

        public int GetDamage()
        {
            return (Damage.Max + Damage.Min) / 2;
        }

    }

    public struct Interval
    {
        private int _min;
        private int _max;
        public int Min => _min;
        public int Max => _max;
        private Random rnd;
        public Interval(int min, int max)
        {
            _min = min;
            _max = max;
            rnd = new Random();
            if (_min > _max)
            {
                int temp = Min;
                _min = Max;
                _max = temp;
                Console.WriteLine($"Ошибка: Min > Max для Interval.");
            }

            if (_min < 0)
            {
                _min = 0;
                Console.WriteLine($"Ошибка: Min < 0 для Interval. Изменен на 0.");
            }
            if (_max < 0)
            {
                _max = 0;
                Console.WriteLine($"Ошибка: Max < 0 для Interval. Изменен на 0.");
            }
            if (_min == _max)
            {
                _max += 10;
                Console.WriteLine($"Ошибка: Min = Max для Interval. Max увеличен на 10.");
            }
        }

        public int Get()
        {
            return rnd.Next(_min, _max);
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