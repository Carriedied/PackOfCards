namespace PackOfCards_v._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player user = new Player();

            user.Work();
        }

        class Deck
        {
            public Deck()
            {
                string path = "AllCards.txt";
                string[] cardValue = File.ReadAllLines(path);

                foreach (string card in cardValue)
                {
                    int position = card.IndexOf(',');

                    if (position < 0)
                    {
                        continue;
                    }

                    _allCards.Add(new Card(card.Substring(0, position), card.Substring(position + 1)));
                }
            }

            private List<Card> _allCards = new List<Card>();
            private List<Card> _playerDeck = new List<Card>();

            private void Shuffle()
            {
                Card swapCard;
                Random random = new Random();

                int minValueRandom = 1;
                int maxValueRandom = _allCards.Count;

                int countRepeat = random.Next(minValueRandom, maxValueRandom);
                int mixCards;

                for(int i = 0; i < countRepeat; i++)
                {
                    mixCards = random.Next(minValueRandom, maxValueRandom);

                    swapCard = _allCards[i];
                    _allCards[i] = _allCards[mixCards];
                    _allCards[mixCards] = swapCard;
                }
            }
            
            public void PickUpCard(int actionNumber)
            {
                Random random = new Random();
                int minCountInDeck = 1;
                int maxCountInDeck = _allCards.Count + 1;
                int randomCard = random.Next(minCountInDeck, maxCountInDeck);
                string countCard;
                int pickUpCard = 1;
                int pickUpSomeCards = 2;
                bool isNumber;

                Shuffle();

                if (actionNumber == pickUpCard)
                {
                    _playerDeck.Add(_allCards[randomCard]);

                    Console.WriteLine("Вы взяли карту: ");
                }
                else if(actionNumber == pickUpSomeCards)
                {
                    Console.WriteLine("Сколько карт вы хотите взять?");
                    countCard = Console.ReadLine();

                    isNumber = GetNumber(countCard, out int convertedCountCard);

                    if(isNumber)
                    {
                        if (convertedCountCard > minCountInDeck && convertedCountCard < maxCountInDeck)
                        {
                            for (int i = 0; i < convertedCountCard; i++)
                            {
                                randomCard = random.Next(minCountInDeck, maxCountInDeck);

                                _playerDeck.Add(_allCards[randomCard - 1]);
                            }

                            Console.WriteLine("Вы взяли карты: ");
                        }
                        else
                        {
                            Console.WriteLine("Столько карт нет в колоде");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверную команду");
                    }
                }

                foreach (var item in _playerDeck)
                {
                    Console.WriteLine($"{item.SuitName} - {item.Merit}");
                }
            }

            public bool GetNumber(string number, out int convertedNumber)
            {
                bool isNumber;

                isNumber = int.TryParse(number, out convertedNumber);

                return isNumber;
            }
        }

        class Card
        {
            public Card(string suitName, string merit)
            {
                SuitName = suitName;
                Merit = merit;
            }

            public string SuitName { get; private set; }
            public string Merit { get; private set; }
        }

        class Player
        {
            private const int PickUpCardCommand = 1;
            private const int PickUpSomeCardsCommand = 2;

            public void Work()
            {
                Deck deck = new Deck();

                string actionChoice;
                bool isNumber;

                Console.WriteLine("Выберите действие, которое хотите сделать:" +
                                 $"\n{PickUpCardCommand} Взять карту из колоды" +
                                 $"\n{PickUpSomeCardsCommand} Взять несколько карт из колоды");
                actionChoice = Console.ReadLine();

                isNumber = deck.GetNumber(actionChoice, out int convertedActionChoice);

                if (isNumber)
                {
                    if (convertedActionChoice == PickUpCardCommand || convertedActionChoice == PickUpSomeCardsCommand)
                    {
                        deck.PickUpCard(convertedActionChoice);
                    }
                    else
                    {
                        Console.WriteLine("Вы выбрали неверную команду");
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели неверную команду");
                }
            }
        }
    }
}