using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace PokerGameKelci
{
    class Player
    {
        //name of the player
        private String player_name;
        //cards that the player holds
        private List<Card> cardList = new List<Card>();

        /**
         * construct a player with a giving name
         * @param player_name
         */
        public Player(String player_name)
        {
            this.player_name = player_name;
        }

        /**
         * receive card while handing out cards
         * @param card
         * @param cardAmount
         */
        public void receiveCard(Card card)
        {
            cardList.Add(card);
        }

        public int getCardCount()
        {
            return cardList.Count;
        }

        public void printMyCards()
        {
            Console.WriteLine(player_name);
            foreach (Card card in cardList)
            {
                Console.WriteLine(card.getCard_type_desc() + ":" + card.getNumber() + "   ");
            }
        }
    }
}
