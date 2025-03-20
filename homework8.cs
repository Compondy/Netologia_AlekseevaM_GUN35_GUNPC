namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var unit = new Unit();
            if (unit.SetDamage(0)) Console.WriteLine("���� ���� � ��� �����, �.�. ����� � ���� ���������� 0");
        }
    }

    public class Unit
    {
        public string Name { get; }
        private float Health => _health; //������ ��� ������, �� �������� - ����� ������ 0
        private float _health { get; set; }
        int Damage; //�� ������������
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