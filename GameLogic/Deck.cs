namespace GameLogic
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }
        private Deck()
        {
        }

        public static Deck BuildDeck()
        {

            List<Card> deck = new List<Card>();
            foreach(var cardValue in Enum.GetValues<CardValues>())
            {
                foreach(var cardSuite in Enum.GetValues<CardSuits>())
                {
                    deck.Add(new Card(cardValue, cardSuite));
                }
            }

            var random = new Random();
            return new Deck
            {
                Cards = deck.OrderBy(_ => random.Next()).ToList()
            };
        }
    }
}
