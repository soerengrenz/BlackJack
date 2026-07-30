namespace GameLogic
{ 
    public class Player
    {
        public Player()
        {
        }

        public Player(string? playerName)
        {
            PlayerName = playerName;
        }

        public void AlterWallet(decimal money)
        {
            if (Money == null)
            {
                Money = new Wallet(money);
            }
        }

        public string? PlayerName { get; }
        public Wallet Money { get; private set; }
        public List<Card> Cards { get; internal set; } = new List<Card>();
        public int CardsValue { get; internal set; }
    }
}