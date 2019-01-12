using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameKelci
{
    class Deck
    {
        //all cards in the PokerDeck
        private List<Card> cards = new List<Card>();

        /**
         * <p>Default constructor
         * There should be a factory to prevent creating Deck every time.
         */
        public Deck()
        {
            //initial Heart
            for (int i = 1; i < 14; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_HEART, i);
                cards.Add(card);
            }
            //initial Spade
            for (int i = 1; i < 14; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_SPADE, i);
                cards.Add(card);
            }
            //initial Club
            for (int i = 1; i < 14; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_CLUB, i);
                cards.Add(card);
            }
            //initial Diamond
            for (int i = 1; i < 14; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_DIAMOND, i);
                cards.Add(card);
            }
            //black kind
            Card blackKing = new Card(Constants.CARD_TYPE_BLACK_KING, 14);
            cards.Add(blackKing);
            //red king
            Card redKing = new Card(Constants.CARD_TYPE_RED_KING, 15);
            cards.Add(redKing);
        }

        /**
         * get one Card then remove it from LinkedList
         * @return
         */
        public void transferToDealer(List<Card> cardDealer)
        {
            cardDealer.AddRange(cards);
        }
    }
}
