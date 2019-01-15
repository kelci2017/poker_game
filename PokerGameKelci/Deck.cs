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

        public Deck()
        {
            //initial Heart
            for (int i = 2; i < 15; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_HEART, i);
                cards.Add(card);
            }
            //initial Spade
            for (int i = 2; i < 15; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_SPADE, i);
                cards.Add(card);
            }
            //initial Club
            for (int i = 2; i < 15; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_CLUB, i);
                cards.Add(card);
            }
            //initial Diamond
            for (int i = 2; i < 15; i++)
            {
                Card card = new Card(Constants.CARD_TYPE_DIAMOND, i);
                cards.Add(card);
            }
            //black kind
            Card blackKing = new Card(Constants.CARD_TYPE_BLACK_KING, 15);
            cards.Add(blackKing);
            //red king
            Card redKing = new Card(Constants.CARD_TYPE_RED_KING, 16);
            cards.Add(redKing);
        }

        public void transferToDealer(List<Card> cardDealer)
        {
            cardDealer.AddRange(cards);
        }
    }
}
