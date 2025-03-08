namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Здесь массивы заданий 1-4
            int[] work1 = { 0, 1 }; //массив с первыми двумя
            for (int i = 0; i < 6; i++) work1 = work1.Append(work1[i] + work1[i + 1]).ToArray(); //добавить 6, складывая последние два

            string[] work2 = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            int[,] work3 = { { 2, 3, 4 }, { (int)Math.Pow(2, 2), (int)Math.Pow(3, 2), (int)Math.Pow(4, 2) }, { (int)Math.Pow(2, 3), (int)Math.Pow(3, 3), (int)Math.Pow(4, 3) } };

            double[][] work4 = new double[3][];
            work4[0] = new double[] { 1, 2, 3, 4, 5 };
            work4[1] = new double[] { Math.E, Math.PI };
            work4[2] = new double[] { Math.Log10(1), Math.Log10(10), Math.Log10(100), Math.Log10(100) };

            // массивы для заданий 5 и 6.
            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };
            Array.Copy(array, array2, 3);

            // Выведите результат
            Console.WriteLine(string.Join(", ", array2));

            string[] sample = { "", "" };
            Array.Resize(ref array, array.Length * 2);
            // Что же будет выведено?
            Console.WriteLine(string.Join(", ", array));
        }
    }
}