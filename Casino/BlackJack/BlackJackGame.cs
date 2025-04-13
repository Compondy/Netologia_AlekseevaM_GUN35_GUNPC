using Casino.Game;
using Casino.SaveLoadProfile;

namespace Casino.BlackJack
{
    public class BlackJackGame : CasinoGameBase
    {
        private List<Card> Cards = new List<Card>();

        private Queue<Card> Deck = new Queue<Card>();

        public BlackJackGame(int NumberOfCards = 36)
        {
            FactoryMethod();
            if (NumberOfCards <= 0 || NumberOfCards > 36) NumberOfCards = 36;
            Shuffle(NumberOfCards);
        }

        private Profile? user;

        private int bet = 0;
        private void Bet()
        {
        betAgain:
            Console.Write($"{user!.userName} please make your bet (1-{user.bank}): ");
            if (int.TryParse(Console.ReadLine(), out bet) == false) goto betAgain;
        }

        private int Sum(List<Card> cards)
        {
            var baseSum = cards.Where(x => x.ToValue() != 11).Sum(x => x.ToValue());
            var aces = cards.Where(x => x.ToValue() == 11).ToList();
            if (baseSum <= 21 - 11 - (aces.Count - 1)) return baseSum + 11 + (aces.Count - 1); // One Ace as 11 and else as 1
            else return baseSum + aces.Count; // All aces as 1
        }

        private bool Result(int userSum, int casinoSum)
        {
            if (userSum == casinoSum && userSum < 21) return true;
            else if (userSum <= 21 && (casinoSum > 21 || casinoSum < userSum))
            {
                Win(new GameEventArguments() { bet = bet, message = $"{user?.userName} ({userSum}) won Casino ({casinoSum})." });
                // player won
                return false;
            }
            else if (casinoSum <= 21 && (userSum > 21 || userSum < casinoSum))
            {
                Loose(new GameEventArguments() { bet = bet, message = $"{user?.userName} ({userSum}) looses to Casino ({casinoSum})." });
                // casino won
                return false;
            }
            else if (userSum == 21 && casinoSum == 21 || userSum > 21 && casinoSum > 21)
            {
                Draw(new GameEventArguments() { bet = bet, message = $"{user?.userName} ({userSum}) and Casino ({casinoSum}) draw." });
                // noone won
                return false;
            }
            else
                return true; // never executed
        }

        private bool Step()
        {
            if (Deck.Count >= 2)
            {
                if (userCards.Count == 0) // first step
                {
                    if (Deck.Count < 4)
                    {
                        Console.WriteLine("Error: Deck cards count is not sufficient for game start (<4).");
                        return false;
                    }
                    userCards.Add(Deck.Dequeue());
                    userCards.Add(Deck.Dequeue());
                    casinoCards.Add(Deck.Dequeue());
                    casinoCards.Add(Deck.Dequeue());
                    Console.WriteLine($"{user!.userName} has got:");
                    foreach (var card in userCards) Console.WriteLine($"[{card.ToName()} ({card.ToValueString()})]");
                    Console.WriteLine($"Casino has got:");
                    foreach (var card in casinoCards) Console.WriteLine($"[{card.ToName()} ({card.ToValueString()})]");
                    var userSum = Sum(userCards);
                    var casinoSum = Sum(casinoCards);
                    return Result(userSum, casinoSum);
                }
                else // next steps
                {
                    userCards.Add(Deck.Dequeue());
                    casinoCards.Add(Deck.Dequeue());
                    Console.WriteLine($"{user!.userName} has got:");
                    foreach (var card in userCards) Console.WriteLine($"[{card.ToName()} ({card.ToValueString()})]");
                    Console.WriteLine($"Casino has got:");
                    foreach (var card in casinoCards) Console.WriteLine($"[{card.ToName()} ({card.ToValueString()})]");
                    var userSum = Sum(userCards);
                    var casinoSum = Sum(casinoCards);
                    return Result(userSum, casinoSum);
                }
            }
            else
            {
                Console.WriteLine("No more cards on Deck. Game is finished.");
                return false;
            }
        }


        private List<Card> userCards = new List<Card>();
        private List<Card> casinoCards = new List<Card>();
        public override void PlayGame(Profile User)
        {
            user = User;
            userCards.Clear();
            casinoCards.Clear();
            Console.WriteLine("BlackJack game is stared.");
            Bet();
            while (Deck.Count >= 2 && Step())
            {
            }
        }

        protected override void FactoryMethod()
        {
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardNumber number in Enum.GetValues(typeof(CardNumber)))
                    Cards.Add(new Card((int)suit, (int)number));
        }

        private void Shuffle(int numberOfCards)
        {
            if (numberOfCards < 1 || numberOfCards > Cards.Count)
            {
                Console.WriteLine($"Error: Requested cards number ({numberOfCards}) must be in range from 1 to ({Cards.Count}).");
                return;
            }
            Deck.Clear();
            var rnd = new Random();
            var cardsCopy = new List<Card>(Cards);
            for (int i = 0; i < numberOfCards; i++)
            {
                var randomIndex = rnd.Next(0, cardsCopy.Count - 1);
                Deck.Enqueue(cardsCopy[randomIndex]);
                cardsCopy.RemoveAt(randomIndex);
            }
            cardsCopy.Clear();
        }

    }
}
