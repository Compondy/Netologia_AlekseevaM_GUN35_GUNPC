
class Program
{
    static void Main(string[] args)
    {
        if (!Int32.TryParse(Console.ReadLine(), out var a))
        {
            Console.WriteLine("Not a number!");
            return;
        }
        if (!Int32.TryParse(Console.ReadLine(), out var b))
        {
            Console.WriteLine("Not a number!");
            return;
        }
        var s = Console.ReadLine();

        if (s!.Length != 1 || (s[0] != '&' && s[0] != '|' && s[0] != '^'))
        {
            Console.WriteLine("Wrong sign!");
            return;
        }

        switch (s[0])
        {
            case '&':
                    Console.WriteLine("Decimal result of {0} & {1} = {2}", a, b, Convert.ToString(a & b, 10));
                    Console.WriteLine("Binary result of {0} & {1} = {2}", a, b, Convert.ToString(a & b, 2));
                    Console.WriteLine("Hexademical result of {0} & {1} = {2}", a, b, Convert.ToString(a & b, 16));
                    break;
            case '^':
                    Console.WriteLine("Decimal result of {0} & {1} = {2}", a, b, Convert.ToString(a ^ b, 10));
                    Console.WriteLine("Binary result of {0} & {1} = {2}", a, b, Convert.ToString(a ^ b, 2));
                    Console.WriteLine("Hexademical result of {0} & {1} = {2}", a, b, Convert.ToString(a ^ b, 16));
                    break;
            case '|':
                    Console.WriteLine("Decimal result of {0} & {1} = {2}", a, b, Convert.ToString(a | b, 10));
                    Console.WriteLine("Binary result of {0} & {1} = {2}", a, b, Convert.ToString(a | b, 2));
                    Console.WriteLine("Hexademical result of {0} & {1} = {2}", a, b, Convert.ToString(a | b, 16));
                    break;
            default:
                Console.WriteLine("Wrong sign!");
                break;
        }
    }
}


