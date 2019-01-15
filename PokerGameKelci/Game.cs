using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGameKelci
{
    public class Game
    {
        //card amount per player for this round
        private int cardCount = -1;

        private int ARRANGE_BY_KIND = 0;
        private int ARRANGE_BY_NUM = 1;

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
        public List<Player> outputWinnerList(List<Player> players)
        {
            
            List<Player> winnerList = new List<Player>();
            List<Player> flushList = new List<Player>();
            List<Player> fourKindList = new List<Player>();
            List<Player> threeKindList =  new List<Player>();

            arrangePlayersCards(players, ARRANGE_BY_KIND);

            if (getFlushPlayers(players).Count > 0)
            {
                flushList = getHighCardPlayers(flushList);
                winnerList.AddRange(flushList);
                return winnerList;
            } else
            {
                List<Player> fourKindPlayerList = getSameKindPlayers(players, 4);
                if (fourKindPlayerList.Count > 0)
                {
                    fourKindList = fourKindPlayerList;
                    winnerList.AddRange(fourKindList);
                } else
                {
                    List<Player> threeKindPlayerList = getSameKindPlayers(players, 3);
                    if (threeKindPlayerList.Count > 0)
                    {
                        threeKindList = getHighCardPlayers(threeKindPlayerList);
                        winnerList.AddRange(threeKindList);
                    } else
                    {
                        arrangePlayersCards(players, ARRANGE_BY_NUM);
                        return getWinnerListByNum(players);
                    }
                }
                return winnerList;
            }
        }
        /**
         * get winner list by card number
         * @param players
         */
        private List<Player> getWinnerListByNum (List<Player> players)
        {
            List<Player> pairList = new List<Player>();
            Dictionary<Player, List<Card>> playerPairCardsList = new Dictionary<Player, List<Card>>();
            //get the players with pairs
            foreach (Player player in players)
            {
                for (int i=0; i<player.getPlayerCards().Count -1; i++)
                {
                    if (player.getPlayerCards().ElementAt(i).getNumber() == player.getPlayerCards().ElementAt(i+1).getNumber())
                    {
                        List<Card> pairCards = new List<Card>();
                        pairCards.Add(player.getPlayerCards().ElementAt(i));
                        pairCards.Add(player.getPlayerCards().ElementAt(i + 1));

                        //only put the higher pair cards to the playe
                        //before set the player card list, put the original player cardlist in the dictionary
                        playerPairCardsList[player] = player.getPlayerCards();
                        player.setPlayerCards(pairCards);
                        pairList.Add(player);
                    }
                    break;
                }
            }
            //if no pair players, just get the high card player from initial players
            if (pairList.Count == 0)
            {
                return getHighCardPlayers(players);
            } else
            {
                List<Player> highPairPlayerList = getHighCardPlayers(pairList);
                if (highPairPlayerList.Count > 1)
                {
                    pairList.Clear();
                    foreach (Player player in highPairPlayerList)
                    {
                        //if there are more than one player has the same pairs, compair the left cards
                        player.setPlayerCards(playerPairCardsList[player]);
                        pairList.Add(player);
                    }
                }
                //get the high card players within the pair players
                return getHighCardPlayers(pairList);
            }
        }
        /**
        * get players with flush
        * @param players
        */
        private List<Player> getFlushPlayers(List<Player>players)
        {
            List<Player> flushList = new List<Player>();
            foreach (Player player in players)
            {
                if (player.getPlayerCards().ElementAt(0).getCard_type() == player.getPlayerCards().ElementAt(player.getPlayerCards().Count -1).getCard_type())
                {
                    Console.WriteLine(player.getPlayerName(player) + " " + player.getPlayerCards().ElementAt(0).getCard_type() + " " + player.getPlayerCards().ElementAt(player.getPlayerCards().Count - 1).getCard_type());
                    flushList.Add(player);
                }
            }
            return flushList;
        }
        /**
        * get players with three kind and four kind
        * @param players
        */
        private List<Player> getSameKindPlayers(List<Player> players, int kinds)
        {
            List<Player> sameKindList = new List<Player>();
            foreach (Player player in players)
            {
                for (int i = 0; i < player.getPlayerCards().Count - kinds; i++)
                {
                    if (player.getPlayerCards().ElementAt(i).getCard_type() == player.getPlayerCards().ElementAt(i + kinds - 1).getCard_type())
                    {
                        List<Card> sameKindCards = new List<Card>();
                        for (int j=0; j<kinds; j++)
                        {
                            sameKindCards.Add(player.getPlayerCards().ElementAt(i + j));
                        }

                        //only put the four kind cards to the player
                        player.setPlayerCards(sameKindCards);
                        //arrange the player's cards by accending so that the first card is the high card
                        player.arrangeCardsByNum(sameKindCards);
                        sameKindList.Add(player);
                        break;
                    }
                    
                }

            }
            return sameKindList;
        }
        /**
        * get players with high card
        * @param players
        */
        private List<Player> getHighCardPlayers (List<Player> players)
        {
            List<Player> highCardPlayerList = new List<Player>();
            Player highCardPlayer = null;
            if (players.Count == 1)
            {
                return players;
            }
            //find the player with high card
            for (int i=0; i<players.Count - 1; i++)
            {
                highCardPlayer = players[i];
                if (players[i + 1].getPlayerCards().ElementAt(0).getNumber() > highCardPlayer.getPlayerCards().ElementAt(0).getNumber()) {
                    highCardPlayer = players[i + 1];
                } 
            }

            //get other players who has the same high card with highcardplayer
            foreach (Player player in players)
            {
                if (player.getPlayerCards().ElementAt(0) == highCardPlayer.getPlayerCards().ElementAt(0))
                {
                    highCardPlayerList.Add(player);
                }
            }

            return highCardPlayerList;
        }
        /**
        * arrange players cards by type or number
        * @param players
        * @para arrange_way
        */
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

    }
}