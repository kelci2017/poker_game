using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGameKelci
{
    public class Game
    {
        //card amount per player for this round
        private int cardCount = -1;

        private List<Player> players = new List<Player>();

        public Game() { }

        /**
         * constructor with a card number parameter
         * @param cardAmount
         */
        public Game(int cardCount)
        {
            this.cardCount = cardCount;
            //negative is not allowed
            if (this.cardCount <= 0)
            {
                this.cardCount = 5;
            }
        }

        /**
         * add player
         * @param player
         */
        public void addPlayer(Player player)
        {
            //add player to list
            players.Add(player);
        }

        /**
         * get amount of player of this round
         * @return
         */
        public int getPlayerCount()
        {
            return players.Count;
        }

        public Player getPlayer(int playerIndex)
        {
            return players.ElementAt(playerIndex);
        }

        public List<Player> getAllPlayer()
        {
            return players;
        }

        /**
         * get card amount this round
         * @return
         */
        public int getCardCount()
        {
            return this.cardCount;
        }

        /**
         * deal card to player
         * @param playerIndex
         * @param card
         */
        public void deal(int playerIndex, Card card)
        {
            players.ElementAt(playerIndex).receiveCard(card);
        }
        /**
         * output winner list
         * @param players
         */
        public Dictionary<Player, int> outputWinnerList(List<Player> players)
        {
            Arbitrator arbitrator = new Arbitrator(players);
            return arbitrator.outPutWinners(players);            
        }
    }
}