namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("������� 1");

            int[] work1 = { 0, 1 };
            for (int i = 0; i < 8; i++) work1 = work1.Append(work1[i] + work1[i + 1]).ToArray();
            Console.WriteLine(String.Join(", ", work1));

            Console.WriteLine("������� 2");
            int[] even = new int[0];
            for (int i = 2; i < 21; i++)
                if (i % 2 == 0) even = even.Append(i).ToArray();
            Console.WriteLine(String.Join(", ", even));

            Console.WriteLine("������� 3");
            for (int i = 1; i < 6; i++)
                for (int m = 1; m < 11; m++)
                    Console.WriteLine($"{i} * {m} = {i * m}");

            Console.WriteLine("������� 4");
            string? password;
            do
            {
                Console.Write("������� ������: ");
                password = Console.ReadLine();
                if (password == "qwerty")
                    Console.WriteLine("���������� ������");
                else
                    Console.WriteLine("������������ ������");
            }
            while (password != "qwerty");
        }
    }
}