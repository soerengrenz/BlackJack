using GameLogic.Helpers;

namespace GameLogic
{

    public enum CardValues
    {
        [CardValuesAttribute(2)]
        Two,
        [CardValuesAttribute(3)]
        Three,
        [CardValuesAttribute(4)]
        Four,
        [CardValuesAttribute(5)]
        Five,
        [CardValuesAttribute(6)]
        Six,
        [CardValuesAttribute(7)]
        Seven,
        [CardValuesAttribute(8)]
        Eight,
        [CardValuesAttribute(9)]
        Nine,
        [CardValuesAttribute(10)]
        Teen,
        [CardValuesAttribute(10)]
        Knight,
        [CardValuesAttribute(10)]
        Queen,
        [CardValuesAttribute(10)]
        King,
        [CardValuesAttribute(1,11)]
        Ace
    }

    public enum CardSuits
    {
        Heart,
        Club,
        Spade,
        Diamond
    }

    public class Card
    {
        public Card(CardValues value, CardSuits suit)
        {
            CardFace = (value, suit);
        }


        public (CardValues, CardSuits) CardFace { get; set; }

        public int[] CardValue { 
            get
            {
                return CardFace.Item1.GetIntegerValues();
            }
        }

        public override string ToString()
        {
            return this.CardFace.Item1 +" of "+ this.CardFace.Item2;
        }
    }
}
