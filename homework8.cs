namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var unit = new Unit();
            if (unit.SetDamage(0)) Console.WriteLine("Юним умер и без урона, т.к. жизнь у него изначально 0");
        }
    }

    public class Unit
    {
        public string Name { get; }
        private float Health => _health; //только для чтения, не задается - будет всегда 0
        private float _health { get; set; }
        int Damage; //не используется
        float Armor;
        public Unit() : this(name: "Unknown Unit")
        { }

        public Unit(string name)
        {
            Name = name;
            Damage = 5;
            Armor = 0.6f;
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
}