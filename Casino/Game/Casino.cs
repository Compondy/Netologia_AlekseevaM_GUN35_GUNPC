using Casino.Dice;
using Casino.BlackJack;
using Casino.SaveLoadProfile;

namespace Casino.Game
{

    public class Casino : IGame
    {
        static int casinoInitialBank;
        int casinoBank;
        int maxUserBank;
        Profile? profile;
        BlackJackGame blackJackGame;
        DiceGame diceGame;
        FileSystemSaveLoadService profileService;
        public Casino(int Bank, int MaxUserBank)
        {
            if (Bank == 0) Bank = 1000;
            if (MaxUserBank == 0) MaxUserBank = 2000;

            maxUserBank = MaxUserBank;
            casinoBank = Bank;
            casinoInitialBank = Bank;
            blackJackGame = new BlackJackGame();
            blackJackGame.OnWin += Game_OnWin;
            blackJackGame.OnLoose += Game_OnLoose;
            blackJackGame.OnDraw += Game_OnDraw;

            diceGame = new DiceGame(2, 1, 6);
            diceGame.OnWin += Game_OnWin;
            diceGame.OnLoose += Game_OnLoose;
            diceGame.OnDraw += Game_OnDraw;

            profileService = new FileSystemSaveLoadService(Directory.GetCurrentDirectory());
        }

        public void StartGame()
        {

            Console.Write("Hello player!\nPlease enter your name: ");
            var name = Console.ReadLine();

            profile = profileService.LoadProfile(name!);
            if (profile == null)
            {
            bankAgain:
                Console.Write($"{name}, how much money will you place in bank (1...{int.MaxValue}): ");
                if (!(int.TryParse(Console.ReadLine(), out int bank) && bank >= 1 && bank <= int.MaxValue)) goto bankAgain;
                profile = new Profile() { userName = name!, bank = bank };
                profileService.SaveProfile(profile);
            }
            int gameVariant = 0;
        chooseAgain:
            if (profile!.bank > maxUserBank)
            {
                profile!.bank /= 2;
                Console.WriteLine("You wasted half of your bank money in casino’s bar");
            }
            Console.Write("Choose a game (1 = Blackjack, 2 = Dice): ");
            string gameVariantString = Console.ReadLine()!;
            if (gameVariantString == "1") gameVariant = 1; else if (gameVariantString == "2") gameVariant = 2; else goto chooseAgain;

            if (profile!.bank == 0)
            {
                Console.WriteLine("No money? Kicked!");
                goto endGame;
            }

            if (gameVariant == 1) // BlackJack
            {
                blackJackGame.PlayGame(profile);
                goto chooseAgain;
            }
            else if (gameVariant == 2) // Dice
            {
                diceGame.PlayGame(profile);
                goto chooseAgain;
            }
        endGame:;
        }

        private void LastMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Thank you for playing. Goodbye {profile!.userName}!");
            profileService.SaveProfile(profile);
        }

        private void Game_OnDraw(object? sender, GameEventArguments e)
        {
            LastMessage(e.message!);
        }

        private void Game_OnLoose(object? sender, GameEventArguments e)
        {
            if (e.bet <= profile!.bank)
            {
                profile!.bank -= e.bet;
                casinoBank += e.bet;
                Console.WriteLine($"Your balance is {profile.bank} now. Casino balance is {casinoBank} now.");
            }
            else
                Console.WriteLine($"Error: User bet is somehow greater than his bank.");
            LastMessage(e.message!);
        }

        private void Game_OnWin(object? sender, GameEventArguments e)
        {
            if (e.bet >= casinoBank)
            {
                profile!.bank += casinoBank;
                casinoBank = casinoInitialBank;
                Console.WriteLine($"Your balance is {profile.bank} now. Casino is ruined and a new one will be built on it's place.");
            }
            else
            {
                profile!.bank += e.bet;
                casinoBank -= e.bet;
                Console.WriteLine($"Your balance is {profile.bank} now. Casino balance is {casinoBank} now.");
            }
            LastMessage(e.message!);
        }

    }
}
