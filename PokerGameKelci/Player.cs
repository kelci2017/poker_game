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
            foreach (Card card in cardList)
            {
                Console.WriteLine(card.getCard_type_desc() + ":" + card.getNumber() + "   ");
            }
        }

        public String getPlayerName(Player player)
        {
            return player.player_name;
        }

        public List<Card> getPlayerCards()
        {
            return this.cardList;
        }

        public void setPlayerCards(List<Card> cards)
        {
            this.cardList = cards;
  
        }
        //arrange player's cards by type
        public List<Card> arrangeCardsByKind(List<Card> cards) 
        {
            cards.Sort(
                  delegate (Card p1, Card p2)
                  {
                    int compareType = p1.getCard_type().CompareTo(p2.getCard_type());
                    if (compareType == 0)
                    {
                        return p2.getNumber().CompareTo(p1.getNumber());
                    }
                    return compareType;
                  }
                );
            return cards;
        }
        //arrange player's cards by number
        public List<Card> arrangeCardsByNum(List<Card> cards)
        {
            cards.Sort(
                  delegate (Card p1, Card p2)
                  {
                      int compareNumber = p2.getNumber().CompareTo(p1.getNumber());
                      if (compareNumber == 0)
                      {
                          return p2.getCard_type().CompareTo(p1.getCard_type());
                      }
                      return compareNumber;
                  }
                );
            return cards;
        }
    }
}
