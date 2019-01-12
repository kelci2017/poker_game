using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameKelci
{
    class Dealer
    {
        private Game game;
        /**
         * Card list
         * Why using LinkedList is for shuffling and cutting consideration.
         */
        private List<Card> cards = new List<Card>();

        public Dealer(Game game)
        {
            this.game = game;
           
            Deck deck = new Deck();
            deck.transferToDealer(cards);
        }

        

        /**
         * deal the cards to players.
         */
        public void deal()
        {
            int playerIndex = 0;
            Card card = popCard();
            while (card != null)
            {
                game.deal(playerIndex, card);

                playerIndex++;
                if (playerIndex == game.getPlayerCount())
                {  //Roll back to 0 if reaches the end of players
                    playerIndex = 0;
                }

                /**
                 * Cards can be all sent out or each player can have a given number of cards.
                 */
                if (game.getCardCount() > 0)
                {
                    if (game.getPlayer(game.getPlayerCount() - 1).getCardCount() == game.getCardCount())
                    {
                        break;
                    }
                }

                card = popCard();
            }
        }
        private Card popCard() {
            Card card = null;

            if (cards.Count > 0)
            {
                card = cards.ElementAt(cards.Count - 1);
                cards.RemoveAt(cards.Count - 1);
                return card;
            }
            return null;
            
        }
        public void shuffle()
        {
            Random r = new Random();
            
            
                for (int n = cards.Count - 1; n > 0; --n)
                {
                    int k = r.Next(n + 1);
                    Card temp = cards[n];
                    cards[n] = cards[k];
                    cards[k] = temp;
                }
            
        }
    }
}
