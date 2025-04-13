using Casino.Game;
using Casino.SaveLoadProfile;

namespace Casino.Dice
{
    public class DiceGame : CasinoGameBase
    {

        int dicesCount;
        int dicesMin;
        int dicesMax;
        private List<Dice> dices = new List<Dice>();
        public DiceGame(int DicesCount, int Min, int Max)
        {
            if (DicesCount < 1) dicesCount = 1; else dicesCount = DicesCount;
            if (Min < 1 || Min > Max || Max < 1) { dicesMin = 1; dicesMax = 6; } else { dicesMin = Min; dicesMax = Max; }
            FactoryMethod();
        }

        private Profile? user;

        private int bet = 0;
        private void Bet()
        {
        betAgain:
            Console.Write($"{user!.userName} please make your bet (1-{user.bank}): ");
            if (int.TryParse(Console.ReadLine(), out bet) == false) goto betAgain;
        }

        private bool Result(int userSum, int casinoSum)
        {
            if (userSum > casinoSum)
            {
                Win(new GameEventArguments() { bet = bet, message = $"{user?.userName} ({userSum}) won Casino ({casinoSum})." });
                //player won
                return false;
            }
            else if (userSum < casinoSum)
            {
                Loose(new GameEventArguments() { bet = bet, message = $"{user?.userName} ({userSum}) looses to Casino ({casinoSum})." });
                //casino won
                return false;
            }
            else if (userSum == casinoSum)
            {
                Draw(new GameEventArguments() { bet = bet, message = $"{user?.userName} ({userSum}) and Casino ({casinoSum}) draw." });
                //noone won
                return false;
            }
            else
                return true; //never executed
        }

        private bool Step()
        {
            List<int> userNumbers = new List<int>();
            List<int> casinoNumbers = new List<int>();
            foreach (var dice in dices) userNumbers.Add(dice.Number);
            foreach (var dice in dices) casinoNumbers.Add(dice.Number);
            var userSum = userNumbers.Sum();
            var casinoSum = casinoNumbers.Sum();
            Console.WriteLine($"{user!.userName} results: {string.Join(", ", userNumbers)} = {userSum}");
            Console.WriteLine($"Casino results: {string.Join(", ", casinoNumbers)} = {casinoSum}");

            return Result(userSum, casinoSum);
        }

        public override void PlayGame(Profile User)
        {
            user = User;
            Console.WriteLine("Dice game is stared.");
            Bet();
            while (Step()) { }
        }


        protected override void FactoryMethod()
        {
            for (int i = 0; i < dicesCount; i++)
            {
                dices.Add(new Dice(dicesMin, dicesMax));
            }
        }


    }
}
