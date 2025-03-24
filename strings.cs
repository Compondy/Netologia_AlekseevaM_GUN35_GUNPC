using System.Text;

namespace HomeWork
{
    internal class Program
    {
        public static string WordsConcatenate(string StringA, string StringB)
        {
            return StringA + StringB;
        }

        public static string GreetUser(string Name, int Age)
        {
            return $"Hello, {Name}!\nYou are {Age} years old.";
        }

        public static string UpperLower(string s)
        {
            return $"{s.Length}\n{s.ToUpper()}\n{s.ToLower()}";
        }

        public static string FirstFive(string s)
        {
            return s.Substring(0, 5);
        }

        static StringBuilder Joiner(string[] s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i]);
                if (i != s.Length - 1) sb.Append(' ');
            }
            return sb;
        }

        public static string ReplaceWords(string inputString, string wordToReplace, string replacementWord)
        {
            return inputString.Replace(wordToReplace, replacementWord);
        }

        static void Main(string[] args)
        {

            Console.WriteLine(WordsConcatenate("a", "b"));
            Console.WriteLine();

            Console.WriteLine(GreetUser("Edwin", 13));
            Console.WriteLine();

            Console.WriteLine(UpperLower("Edwin"));
            Console.WriteLine();

            Console.WriteLine(FirstFive("Edwin Alekseev"));
            Console.WriteLine();

            string[] words = { "a", "b", "c", "d", "e", "f", "g", "h" };
            Console.WriteLine(Joiner(words).ToString());
            Console.WriteLine();

            Console.WriteLine(ReplaceWords("Hello world", "world", "universe"));
            Console.WriteLine();
        }
    }
}