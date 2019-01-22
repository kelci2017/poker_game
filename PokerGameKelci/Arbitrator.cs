using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGameKelci
{
    class Arbitrator
    {
        List<Player> players = new List<Player>();
        private int ARRANGE_BY_KIND = 1;
        private int ARRANGE_BY_NUM = 2;
        private int FOUR_OF_ONE_KIND = 4;
        private int THREE_OF_ONE_KIND = 3;
        private int PAIRS = 3;
        private int TWO_PAIRS = 2;
        private int ONE_PAIR = 1;
        private int HIGH_CARD = 12;

        private int INDEX_ZERO = 0;

        public Arbitrator (List<Player> players)
        {
            this.players = players;
        }
        /**
         * get a code for each hand
         * @param cards
         */
        private int getCardsCode(Player player, List<Card> cards)
        {
            arrangePlayersCards(player, ARRANGE_BY_KIND);
 
            if (isSameCardType(cards))
            {
                if (isStraightCards(cards))
                {
                    if (cards[0].getNumber() == 14)
                    {
                        return Constants.ROYAL_FLUSH;
                    } else
                    {
                        return Constants.STRAIGHT_FLUSH;
                    }
                }
                return Constants.FLUSH;
            }

            arrangePlayersCards(player, ARRANGE_BY_NUM);

            List<Card> fourSameKindCards = new List<Card>();
            fourSameKindCards = getSameKind(cards, FOUR_OF_ONE_KIND);
            if (fourSameKindCards.Count > 0)
            {
                player.setPlayerCards(fourSameKindCards);
                return Constants.FOUR_ONE_KIND;
            }

            List<Card> threeSameKindCards = new List<Card>();
            threeSameKindCards = getSameKind(cards, THREE_OF_ONE_KIND);
            Console.WriteLine("the threesamekindcards count is: " + threeSameKindCards.Count);
            if (threeSameKindCards.Count > 0 && (threeSameKindCards[3].getNumber() == threeSameKindCards[4].getNumber()))
            {
                player.setPlayerCards(threeSameKindCards);
                return Constants.FULL_HOUSE;
            } else if (threeSameKindCards.Count > 0 && (threeSameKindCards[3].getNumber() != threeSameKindCards[4].getNumber()))
            {
                player.setPlayerCards(threeSameKindCards);
                return Constants.THREE_ONE_KIND;
            }

            if (isStraightCards(cards))
            {
                return Constants.STRAIGHT;
            }
            
            List<Card> pairCards = new List<Card>();
            pairCards = getPairCards(cards);
            if (pairCards.Count > 0)
            {
                player.setPlayerCards(pairCards);
                return Constants.PAIRS;                
            }
            
            return Constants.HIGH_CARD;
        }

        /**
         * arrange the cards by pairs then high to low rank
         * @param cards
         */
        private List<Card> getPairCards(List<Card> cards)
        {
            List<Card> pairCards = new List<Card>();
            List<Card> leftCards = new List<Card>(cards.Count);
            List<Card> arrangedCards = new List<Card>();

            Card previousCard = null;

            cards.ForEach((item) =>
            {
                leftCards.Add(item);
            });


            for (int i = 0; i < cards.Count; i++)
            {
                if (previousCard == null)
                {                   
                    previousCard = cards[i];
                    continue;
                }
                if (previousCard.getNumber() == cards[i].getNumber())
                {
                    pairCards.Add(previousCard);
                    pairCards.Add(cards[i]);

                    leftCards.Remove(cards[i]);
                    leftCards.Remove(previousCard);
                    previousCard = null;
                }
                else
                {
                    previousCard = cards[i];
                }
            }

            //put the pair cards and the left cards in the arranged cards
            if (pairCards.Count > 0)
            {
                arrangedCards.AddRange(pairCards);
                arrangedCards.AddRange(leftCards);
            }

            //foreach (Card card in arrangedCards)
            //{
            //    Console.WriteLine("the arranged cards after getpaircards is: " + card.getCard_type_desc() + ":" + card.getNumber() + "   ");
            //}
            //Console.WriteLine("the paircards count is: " + pairCards.Count);
            //Console.WriteLine("the leftcards count is: " + leftCards.Count);

            return arrangedCards;
        }
        /**
         * check flush with cards type
         * @param cards
         */
        private bool isSameCardType(List<Card> cards)
        {
            if (cards.ElementAt(0).getCard_type() == cards.ElementAt(cards.Count - 1).getCard_type())
            {
                return true;
            }
            return false;
        }
        /**
         * check straight cards
         * @param cards
         */
        private bool isStraightCards(List<Card> cards)
        {
            List<int> cardsNumDiff = new List<int>();
            for (int i=0; i<cards.Count - 1; i++)
            {
                cardsNumDiff.Add(cards[i].getNumber() - cards[i + 1].getNumber());
            }
            if (cardsNumDiff.All(o => o == 1))
            {
                return true;
            }
                return false;
        }
        /**
         * check same kind cards
         * @param cards
         */
        private List<Card> getSameKind(List<Card> cards, int kinds)
        {

            List<Card> sameKindCards = new List<Card>();
            List<Card> leftCards = new List<Card>();
            List<Card> arrangedCards = new List<Card>();
        
            int cardIndex = 5;
            for (int i = 0; i < cards.Count - kinds + 1; i++)
                {
                if (cards.ElementAt(i).getNumber() == cards.ElementAt(i + kinds - 1).getNumber())
                    {
                        
                        for (int j = 0; j < kinds; j++)
                        {
                            sameKindCards.Add(cards.ElementAt(i + j));
                        }
                    cardIndex = i;
                    Console.WriteLine("the same kind index i is: " + cardIndex);
                    break;
                    
                    } 
                }

            if (cardIndex > 0 && cardIndex < 5 )
            {
                for (int i = 0; i < cardIndex; i++)
                {
                    leftCards.Add(cards[i]);
                }
               
            }
            if (cardIndex == 0 || (cardIndex + kinds) <= cards.Count)
            {
                for (int i = (cardIndex + kinds); i < cards.Count; i++)
                {
                    leftCards.Add(cards[i]);
                }
            }

            //rankCards(leftCards);
            arrangedCards.AddRange(sameKindCards);
            arrangedCards.AddRange(leftCards);
            foreach (Card card in arrangedCards)
            {
                Console.WriteLine("the arrangedCards after getsamekind is: " + card.getCard_type_desc() + ":" + card.getNumber() + "   ");
            }
            Console.WriteLine("the sameKindCards count is: " + sameKindCards.Count);
            Console.WriteLine("the leftCards count is: " + leftCards.Count);
            return arrangedCards;
        }

        private List<Card> rankCards(List<Card> cards)
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
        public Dictionary<Player, int> outPutWinners(List<Player> players)
        {
            Dictionary<Player, int> playerCodeDic = new Dictionary<Player, int>();
            Dictionary<Player, int> winnerCodeDic = new Dictionary<Player, int>();

            foreach (Player player in players)
            {                
                playerCodeDic[player] = getCardsCode(player, player.getPlayerCards());
                Console.WriteLine(player.getPlayerName(player) + " " + playerCodeDic[player]);
            }
            //get the max player code
            winnerCodeDic = getMax(playerCodeDic);

            switch (playerCodeDic.Values.Max())
            {
                case 10: //royal flush
                    return winnerCodeDic;
                case 9:  //straight flush              
                     return getHighCard(winnerCodeDic, INDEX_ZERO);                                        
                case 8:  //four of a kind
                    return getHighCardFromLeft(winnerCodeDic, FOUR_OF_ONE_KIND);
                case 7:  //full house
                    return getHighCardFromLeft(winnerCodeDic, FOUR_OF_ONE_KIND);
                case 6:  //flush
                    return getHighCard(winnerCodeDic, INDEX_ZERO);
                case 5:  //straight                   
                    return getHighCard(winnerCodeDic, INDEX_ZERO);
                case 4:  //three of a kind
                    return getHighCardFromLeft(winnerCodeDic, THREE_OF_ONE_KIND);
                case 3:  // pairs
                    Console.WriteLine("it has pairs");
                    return getHighCardFromLeft(winnerCodeDic, PAIRS);
                case 2:  // high card
                    return getHighCardFromLeft(winnerCodeDic, HIGH_CARD);
            }
            
            return winnerCodeDic;
        }
        /**
         * get the max player code
         * @param player code dictionary
         */
        private Dictionary<Player, int> getMax (Dictionary<Player, int> playerCodeDic)
        {
            Dictionary<Player, int> scannedCodeDic = new Dictionary<Player, int>();

            for (int i=0; i < playerCodeDic.Count; i++)
            {
                if (playerCodeDic.Values.Max() == playerCodeDic.Values.ElementAt(i))
                {
                    scannedCodeDic[playerCodeDic.Keys.ElementAt(i)] = playerCodeDic.Values.ElementAt(i);
                }
            }

            return scannedCodeDic;
        }
        /**
         * get higher card player
         * @param player code dictionary
         * @param index
         */
        private Dictionary<Player, int> getHighCard (Dictionary<Player, int> playerCodeDic, int index)
        {
            Dictionary<Player, int> scannedCodeDic = new Dictionary<Player, int>();
            int highCard = 0;
 
            for (int i = 0; i < playerCodeDic.Count; i++)
            {
                if (playerCodeDic.Keys.ElementAt(i).getPlayerCards().ElementAt(index).getNumber() > highCard)
                {
                    highCard = playerCodeDic.Keys.ElementAt(i).getPlayerCards().ElementAt(index).getNumber();
                }              
            }

            for (int i = 0; i < playerCodeDic.Count; i++)
            {
                if (playerCodeDic.Keys.ElementAt(i).getPlayerCards().ElementAt(index).getNumber() == highCard)
                {

                    scannedCodeDic[playerCodeDic.Keys.ElementAt(i)] = playerCodeDic.Values.ElementAt(i);
                }
            }
            Console.WriteLine("the scanned dic count is: " + scannedCodeDic.Count);
            return scannedCodeDic;
        }
        /**
         * get higher pair card player
         * @param player code dictionary
         * @param kinds (TWO PAIRS or ONE PAIR)
         */
        private Dictionary<Player, int> getHighPairCardFromLeft(Dictionary<Player, int> playerCodeDic, int kinds)
        {
            Dictionary<Player, int> indexZeroCodeDic = getHighCard(playerCodeDic, 0);

            if (indexZeroCodeDic.Count > 1)
            {
                if (kinds == TWO_PAIRS)
                {
                    Dictionary<Player, int> indexTwoCodeDic = new Dictionary<Player, int>();
                    indexTwoCodeDic = getHighCard(indexZeroCodeDic, 2);

                    if (indexTwoCodeDic.Count > 1)
                    {
                        Dictionary<Player, int> indexFourCodeDic = new Dictionary<Player, int>();
                        indexFourCodeDic = getHighCard(indexTwoCodeDic, 4);
                        return indexFourCodeDic;
                    }
                    return indexTwoCodeDic;
                }
                else if (kinds == ONE_PAIR)
                {
                    Dictionary<Player, int> indexTwoCodeDic = new Dictionary<Player, int>();
                    indexTwoCodeDic = getHighCard(indexZeroCodeDic, 2);

                    if (indexTwoCodeDic.Count > 1)
                    {
                        Dictionary<Player, int> indexThreeCodeDic = new Dictionary<Player, int>();
                        indexThreeCodeDic = getHighCard(indexTwoCodeDic, 3);
                        if (indexThreeCodeDic.Count > 1)
                        {
                            Dictionary<Player, int> indexFourCodeDic = new Dictionary<Player, int>();
                            indexFourCodeDic = getHighCard(indexThreeCodeDic, 4);
                            return indexFourCodeDic;
                        }
                        else
                        {
                            return indexThreeCodeDic;
                        }
                    }
                    return indexTwoCodeDic;
                }            
            }
            return indexZeroCodeDic;
        }
        /**
         * get higher card player
         * @param player code dictionary
         * @param kinds
         */
        private Dictionary<Player, int> getHighCardFromLeft(Dictionary<Player, int> playerCodeDic, int kinds)
        {
            Dictionary<Player, int> indexZeroCodeDic = getHighCard(playerCodeDic, 0);

            if (indexZeroCodeDic.Count > 1)
            {
                Console.WriteLine("now the fist element at index 0 is the same");
                if (kinds == FOUR_OF_ONE_KIND)
                {
                    return getHighCard(indexZeroCodeDic, 4);
                } else if (kinds == THREE_OF_ONE_KIND)
                {
                    Dictionary<Player, int> indexThreeCodeDic = new Dictionary<Player, int>();
                    indexThreeCodeDic = getHighCard(indexZeroCodeDic, 3);
                    if (indexThreeCodeDic.Count > 1)
                    {
                        return getHighCard(indexThreeCodeDic, 4);
                    }
                    return indexThreeCodeDic;
                }
                else if (kinds == PAIRS)
                {
                    
                    Dictionary <Player, int> twoPairCodeDic = new Dictionary<Player, int>();
                    Dictionary<Player, int> onePairCodeDic = new Dictionary<Player, int>();
                    for (int i=0; i< indexZeroCodeDic.Count; i++)
                    {
                        
                        List<Card> pairCards = new List<Card>();
                        pairCards = getPairCards(indexZeroCodeDic.Keys.ElementAt(i).getPlayerCards());
                        indexZeroCodeDic.Keys.ElementAt(i).setPlayerCards(pairCards);
                        foreach (Card card in indexZeroCodeDic.Keys.ElementAt(i).getPlayerCards())
                        {
                            Console.WriteLine("the pair cards after get pair cards are : " + card.getCard_type_desc() + ":" + card.getNumber() + "   ");
                        }
                        if (pairCards[2].getNumber() == pairCards[3].getNumber())
                        {
                            twoPairCodeDic[indexZeroCodeDic.Keys.ElementAt(i)] = indexZeroCodeDic.Values.ElementAt(i);

                        } else
                        {
                            onePairCodeDic[indexZeroCodeDic.Keys.ElementAt(i)] = indexZeroCodeDic.Values.ElementAt(i);
                        }
                                               
                    }
                    if (twoPairCodeDic.Count > 0)
                    {
                        getHighPairCardFromLeft(twoPairCodeDic, TWO_PAIRS);
                    } else
                    {
                        getHighPairCardFromLeft(onePairCodeDic, ONE_PAIR);
                    }
                } else if (kinds == HIGH_CARD)
                {
                    Dictionary<Player, int> indexOneCodeDic = new Dictionary<Player, int>();
                    Console.WriteLine("now the first element at index 0 count is " + indexZeroCodeDic.Count);
                    indexOneCodeDic = getHighCard(indexZeroCodeDic, 1);
                    Console.WriteLine("now the second element at index 1 count is " + indexOneCodeDic.Count);
                    if (indexOneCodeDic.Count > 1)
                    {
                        Dictionary<Player, int> indexTwoCodeDic = new Dictionary<Player, int>();
                        indexTwoCodeDic = getHighCard(indexOneCodeDic, 2);
                        if (indexTwoCodeDic.Count > 1)
                        {
                            Dictionary<Player, int> indexThreeCodeDic = new Dictionary<Player, int>();
                            indexThreeCodeDic = getHighCard(indexTwoCodeDic, 3);
                            if (indexThreeCodeDic.Count > 1)
                            {
                                Dictionary<Player, int> indexFourCodeDic = new Dictionary<Player, int>();
                                indexFourCodeDic = getHighCard(indexThreeCodeDic, 4);
                                return indexFourCodeDic;
                            }
                            else
                            {
                                return indexThreeCodeDic;
                            }
                        }
                        else
                        {
                            return indexTwoCodeDic;
                        }
                    }
                    
                    return indexOneCodeDic;
                }

               }                    
               return indexZeroCodeDic;
        }
        /**
        * arrange players cards by type or number
        * @param players
        * @para arrange_way
        */
        public void arrangePlayersCards(Player player, int arrange_way)
        {
            if (arrange_way == ARRANGE_BY_KIND)
            {
                    player.setPlayerCards(player.arrangeCardsByKind(player.getPlayerCards()));                
            }
            else
            {               
                    player.setPlayerCards(player.arrangeCardsByNum(player.getPlayerCards()));                
            }
        }
    
    }
}