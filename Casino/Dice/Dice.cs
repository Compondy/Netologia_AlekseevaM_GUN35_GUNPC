namespace Casino.Dice
{
    public struct Dice
    {
        private Random rnd;
        private int min;
        private int max;
        public Dice(int Min, int Max)
        {
            rnd = new Random();
            try
            {
                if (Min < 1 || Min > int.MaxValue || Max > int.MaxValue || Max < Min) throw new WrongDiceNumberException(Min, Max);
                max = Max;
                min = Min;
            }
            catch (WrongDiceNumberException e)
            {
                Console.WriteLine(e.Message);
                min = 1;
                max = 6;
                Console.WriteLine("Min is set to 1 and Max is set to 6.");
            }
        }
        public readonly int Number
        {
            get { return rnd.Next(min, max); }
        }
    }
}
