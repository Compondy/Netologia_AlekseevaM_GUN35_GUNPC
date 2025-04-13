namespace Casino.Dice
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException() : base() { }
        public WrongDiceNumberException(string Message) : base(Message) { }
        public WrongDiceNumberException(string Message, Exception InnerException) : base(Message, InnerException) { }
        public WrongDiceNumberException(int Min, int Max) : base(ProcessMinMax(Min, Max)) { }

        private static string ProcessMinMax(int Min, int Max)
        {
            string message = "Exception: Wrong dice number.\n";
            if (Min < 1 || Min > int.MaxValue) message += $"Min is {Min}, but must be in range of 1 to {int.MaxValue}.\n";
            if (Max < 1 || Max > int.MaxValue) message += $"Max is {Max}, but must be in range of 1 to {int.MaxValue}.\n";
            if (Max < Min) message += $"Min is {Min} and Max is {Max}. Max is lower than Min.\n";
            return message;
        }

    }
}
