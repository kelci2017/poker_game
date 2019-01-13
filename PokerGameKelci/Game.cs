﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameKelci
{
    public class Game
    {
        //card amount per player for this round
        private int cardCount = -1;

        private int ARRANGE_BY_KIND = 0;
        private int ARRANGE_BY_NUM = 1;

        //players ArrayList,they are disorder
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

        public Player outputWinner(List<Player> players)
        {
            Player winner = null;

            arrangePlayersCards(players, ARRANGE_BY_KIND);
            winner = comparePlayersCardsByKind(players);

            if (winner == null)
            {
                arrangePlayersCards(players, ARRANGE_BY_NUM);
                winner = comparePlayersCardsByNum(players);
            }
            
            return winner;
        }

        public void arrangePlayersCards(List<Player> players, int arrange_way)
        {
            if (arrange_way == ARRANGE_BY_KIND)
            {
                foreach (Player player in players)
                {
                    player.setPlayerCards(player.arrangeCardsByKind(player.getPlayerCards()));
                }
            } else
            {
                foreach (Player player in players)
                {
                    player.setPlayerCards(player.arrangeCardsByNum(player.getPlayerCards()));
                }
            }
        }

        public Player comparePlayersCardsByKind(List<Player> players)
        {
            return null;
        }

        public Player comparePlayersCardsByNum(List<Player> players)
        {
            return null;
        }

    }
}

