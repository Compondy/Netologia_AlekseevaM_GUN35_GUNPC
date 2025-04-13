namespace Casino.BlackJack
{
    public struct Card
    {
        public Card(int Suit, int Number)
        {
            if (Enum.GetValues(typeof(CardSuit)).Cast<int>().Contains(Suit))
                cardSuit = Suit;
            if (Enum.GetValues(typeof(CardNumber)).Cast<int>().Contains(Number))
                cardNumber = Number;
        }
        public readonly int cardSuit;
        public readonly int cardNumber;

        public string ToName()
        {
            string name = "";

            switch (cardNumber)
            {
                case (int)CardNumber.Six:
                    {
                        name += "Six";
                        break;
                    }
                case (int)CardNumber.Seven:
                    {
                        name += "Seven";
                        break;
                    }
                case (int)CardNumber.Eight:
                    {
                        name += "Eight";
                        break;
                    }
                case (int)CardNumber.Nine:
                    {
                        name += "Nine";
                        break;
                    }
                case (int)CardNumber.Ten:
                    {
                        name += "Ten";
                        break;
                    }
                case (int)CardNumber.Jack:
                    {
                        name += "Jack";
                        break;
                    }
                case (int)CardNumber.Queen:
                    {
                        name += "Queen";
                        break;
                    }
                case (int)CardNumber.King:
                    {
                        name += "King";
                        break;
                    }
                case (int)CardNumber.Ace:
                    {
                        name += "Ace";
                        break;
                    }
            }

            switch (cardSuit)
            {
                case (int)CardSuit.Diamond:
                    {
                        name += " of Diamond";
                        break;
                    }
                case (int)CardSuit.Spade:
                    {
                        name += " of Spade";
                        break;
                    }
                case (int)CardSuit.Heart:
                    {
                        name += " of Heart";
                        break;
                    }
                case (int)CardSuit.Club:
                    {
                        name += " of Club";
                        break;
                    }
            }

            return name;
        }
        public int ToValue()
        {
            switch (cardNumber)
            {
                case (int)CardNumber.Six: return 6;
                case (int)CardNumber.Seven: return 7;
                case (int)CardNumber.Eight: return 8;
                case (int)CardNumber.Nine: return 9;
                case (int)CardNumber.Ten: return 10;
                case (int)CardNumber.Jack: return 10;
                case (int)CardNumber.Queen: return 10;
                case (int)CardNumber.King: return 10;
                case (int)CardNumber.Ace: return 11;
                default: return 0;
            }
        }

        public string ToValueString()
        {
            int value = ToValue();
            if (value >= 6 && value <= 10) return value.ToString();
            else if (value == 11) return "1 or 11"; else return "Unknown";
        }

    }
}
