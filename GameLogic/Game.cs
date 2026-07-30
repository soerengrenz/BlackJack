namespace GameLogic
{
    public class Game
    {
        public enum GameStatus
        {
            NotStarted,
            Started,
            WaitingForPlayer,
            Hold,
            Won,
            Lost
        }

        private static Game? instance;
        private Game(Player player)
        {
            Player = player;
        }

        public Player Player { get; }

        public Deck Deck { get; private set; }

        public GameStatus Status { get; private set; }

        public static Game Create(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (instance == null)
            { 
                instance = new Game(player);
            }

            return instance;
        }

        public void Start()
        {
            instance.Deck = Deck.BuildDeck();
            instance.Status = GameStatus.Started;
        }

        public void DrawLastCard()
        {
            throw new NotImplementedException();
        }

        public void DrawCard()
        {
            var card = instance.Deck.Cards.First();
            instance.Deck.Cards.Remove(card);
            instance.Player.Cards.Add(card);

            instance.Status = DetermineStatusValue();
        }

        private void CalculatePLayerCardsValue()
        {
            instance.Player.CardsValue = CardsOnHandValue(instance.Player.Cards);

        }

        private int CardsOnHandValue(List<Card> cards)
        {
            return cards.Sum(x => x.CardValue.Last()) > 21 ? cards.Sum(x => x.CardValue.Last()) : cards.Sum(x => x.CardValue.First());
        }

        private GameStatus DetermineStatusValue()
        {
            CalculatePLayerCardsValue();
            if (instance.Player.CardsValue > 21)
            {
                return GameStatus.Lost;
            }

            if (instance.Player.CardsValue == 21)
            {
                return GameStatus.Hold;
            }

            return GameStatus.WaitingForPlayer;
        }

        public void Hold()
        {
            instance.Status = GameStatus.Hold;
        }

        public (List<Card> Cards, int CardValues) DealerDraw()
        {
            var dealerCards = new List<Card>();
            var dealerCardValue = 0;
            while (dealerCardValue < 22)
            {
                var card = instance.Deck.Cards.First();
                instance.Deck.Cards.Remove(card);
                dealerCards.Add(card);
                dealerCardValue = CardsOnHandValue(dealerCards);
                if(dealerCardValue >= 17)
                {
                    break;
                }
            }

            DetermineWinner(dealerCardValue);

            return (dealerCards, dealerCardValue);
        }

        public void DetermineWinner(int cardValues)
        {
            if(cardValues >= instance.Player.CardsValue)
            {
                instance.Status = GameStatus.Lost;
                return;
            }

            instance.Status = GameStatus.Won;
        }
    }
}