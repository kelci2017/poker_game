using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace PokerGameKelci
{
    public class Player
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
            arrangeCardsByKind(cardList);
            foreach (Card card in cardList)
            {
                Console.WriteLine(card.getCard_type_desc() + ":" + card.getNumber() + "   ");
            }
        }

        public List<Card> getPlayerCards()
        {
            return this.cardList;
        }

        public void setPlayerCards(List<Card> cards)
        {
            this.cardList = cards;
  
        }
        public List<Card> arrangeCardsByKind(List<Card> cards)
        {
            cards.GroupBy(x => x.getCard_type())
                .Select(x => new
                {
                    Cards = x.OrderByDescending(c => c.getNumber()),
                     Count = x.Count(),
                })
                .OrderByDescending(x => x.Count)
                .SelectMany(x => x.Cards);
            return cards;
        }

        public List<Card> arrangeCardsByNum(List<Card> cards)
        {
            //cards.Sort();
            return cards;
        }
    }
}
