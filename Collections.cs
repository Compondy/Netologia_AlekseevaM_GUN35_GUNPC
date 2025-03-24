using System.Runtime.CompilerServices;

namespace HomeWork
{
    internal class Program
    {
        private class Task1
        {
            private List<string> _data = new List<string>();
            public void TaskLoop()
            {
                _data.Clear();

                Console.WriteLine("For exit write \"-exit\".");

                for (int i = 1; i <= 5; i++) _data.Add("String" + i);

                while (true)
                {
                    while (true)
                    {
                        Console.Write("Input a new line: ");
                        string? line = Console.ReadLine();
                        if (line == "-exit") return;
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            _data.Add(line);
                            break;
                        }
                    }

                    Console.WriteLine("List: \"" + string.Join("\" , \"", _data) + "\"");
                    
                    while (true)
                    {
                        Console.Write("Input middle line: ");
                        string? line = Console.ReadLine();
                        if (line == "-exit") return;

                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            _data.Insert(_data.Count / 2, line);
                            break;
                        }
                    }

                    Console.WriteLine("List: \"" + string.Join("\" , \"", _data) + "\"");
                }
            }
        }

        private class Task2
        {
            private Dictionary<string, int> students = new Dictionary<string, int>();

            public void TaskLoop()
            {
                students.Clear();
                Console.WriteLine("For exit write \"-exit\".");

                while (true)
                {
                    string? student, studentmark;
                    int mark = 0;
                    while (true)
                    {
                        Console.Write("Input student name: ");
                        student = Console.ReadLine();
                        if (student == "-exit") return;
                        if (!string.IsNullOrWhiteSpace(student)) break;
                    }
                    while (true)
                    {
                        Console.Write("Input student mark (2-5): ");
                        studentmark = Console.ReadLine();
                        if (student == "-exit") return;
                        if (!string.IsNullOrWhiteSpace(student) && int.TryParse(studentmark, out mark) && mark > 1 && mark < 6) break;
                    }
                    students.Add(student, mark);

                    while (true)
                    {
                        Console.Write("Input student name for query: ");
                        student = Console.ReadLine();
                        if (student == "-exit") return;
                        if (!string.IsNullOrWhiteSpace(student)) break;
                    }

                    if (students.TryGetValue(student, out mark)) Console.WriteLine($"Student mark: {mark}"); else Console.WriteLine("No such student.");
                }
            }
        }

        private class Task3
        {
            private Dictionary<(string, string), int> students = new Dictionary<(string, string), int>();

            public void TaskLoop()
            {
                while (true)
                {
                    students.Clear();
                    Console.WriteLine("For exit write \"-exit\".");
                    int records = 0;
                    string recordsstring;
                    string student = "", studentfamily = "", studentmark = "";
                    int mark = 0;

                    while (true)
                    {
                        Console.Write("Input a number of new records (3-6): ");
                        recordsstring = Console.ReadLine()!;
                        if (recordsstring == "-exit") return;
                        if (!string.IsNullOrWhiteSpace(recordsstring) && int.TryParse(recordsstring, out records) && records > 2 && records < 7) break;
                    }

                    for (int i = 0; i != records; i++)
                    {

                        while (true)
                        {
                            Console.Write("Input student name: ");
                            student = Console.ReadLine()!;
                            if (student == "-exit") return;
                            if (!string.IsNullOrWhiteSpace(student)) break;
                        }

                        while (true)
                        {
                            Console.Write("Input student family: ");
                            studentfamily = Console.ReadLine()!;
                            if (studentfamily == "-exit") return;
                            if (!string.IsNullOrWhiteSpace(studentfamily)) break;
                        }

                        while (true)
                        {
                            Console.Write("Input student mark (2-5): ");
                            studentmark = Console.ReadLine()!;
                            if (student == "-exit") return;
                            if (!string.IsNullOrWhiteSpace(student) && int.TryParse(studentmark, out mark) && mark > 1 && mark < 6) break;
                        }
                        students.Add((student, studentfamily), mark);
                    }

                    foreach (var item in students)
                        Console.WriteLine($"Student: {item.Key.Item2} {item.Key.Item1}. Mark: {item.Value}.");

                    Console.WriteLine();

                    foreach (var item in students.Reverse())
                        Console.WriteLine($"Student: {item.Key.Item2} {item.Key.Item1}. Mark: {item.Value}.");
                }
            }
        }

        static void Main(string[] args)
        {
            int task = 0;
            Console.Write("Enter 1, 2 or 3 to check task 1, 2 or 3: ");

            while (task == 0)
            {
                int.TryParse(Console.ReadLine(), out task);
                switch (task)
                {
                    case 1:
                        CheckTask1();
                        break;
                    case 2:
                        CheckTask2();
                        break;
                    case 3:
                        CheckTask3();
                        break;
                    default:
                        Console.Write("Only 1,2 or 3 allowed: ");
                        task = 0;
                        break;
                }
            }
        }

        private static void CheckTask1()
        {
            new Task1().TaskLoop();
        }
        private static void CheckTask2()
        {
            new Task2().TaskLoop();
        }
        private static void CheckTask3()
        {
            new Task3().TaskLoop();
        }
    }
}