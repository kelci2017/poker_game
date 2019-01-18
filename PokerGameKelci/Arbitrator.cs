using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGameKelci
{
    class Arbitrator
    {
        List<Player> players = new List<Player>();
        private int ARRANGE_BY_KIND = 55;
        private int ARRANGE_BY_NUM = 66;
        private int FOUR_OF_ONE_KIND = 44;
        private int THREE_OF_ONE_KIND = 33;
        private int TWO_PAIRS = 22;
        private int ONE_PAIR = 11;
        private int HIGH_CARD = 12;

        private int INDEX_ZERO = 0;
        private int INDEX_ONE = 1;
        private int INDEX_TWO = 2;
        private int INDEX_THREE = 3;
        private int INDEX_FOUR = 4;
        public Arbitrator (List<Player> players)
        {
            this.players = players;
        }

        private int getCardsCode(List<Card> cards)
        {
            arrangePlayersCards(players, ARRANGE_BY_KIND);
 
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

            List<Card> fourSameKindCards = new List<Card>();
            fourSameKindCards = getSameKind(cards, FOUR_OF_ONE_KIND);
            if (fourSameKindCards.Count > 0)
            {
                return Constants.FOUR_ONE_KIND;
            }

            List<Card> threeSameKindCards = new List<Card>();
            threeSameKindCards = getSameKind(cards, FOUR_OF_ONE_KIND);
            if (threeSameKindCards.Count > 0 && (threeSameKindCards[4].getNumber() == threeSameKindCards[5].getNumber()))
            {
                return Constants.FULL_HOUSE;
            } else if (threeSameKindCards.Count > 0 && (threeSameKindCards[4].getNumber() != threeSameKindCards[5].getNumber()))
            {
                return Constants.THREE_ONE_KIND;
            }

            arrangePlayersCards(players, ARRANGE_BY_NUM);

            if (isStraightCards(cards))
            {
                return Constants.STRAIGHT;
            }

            List<Card> pairCards = new List<Card>();
            pairCards = getPairCards(cards);

            if (pairCards.Count > 0)
            {
                if (pairCards[2].getNumber() == pairCards[3].getNumber())
                {
                    return Constants.TWO_PAIRS;
                } else
                {
                    return Constants.ONE_PAIRS;
                }
            }

            return Constants.HIGH_CARD;
        }

        
        private List<Card> getPairCards(List<Card> cards)
        {
            List<Card> pairCards = new List<Card>();
            List<Card> leftCards = cards;
            List<Card> arrangedCards = new List<Card>();
            Card previousCard = null;


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

            //int pairIndex = 0;
            //    for (int j = 0; j < cards.Count - 1; j++)
            //    {
            //        if (cards[j].getNumber() == cards[j+1].getNumber())
            //        {
            //            pairCards.Add(cards[j]);
            //            pairCards.Add(cards[j + 1]);
            //            leftCards.Remove(cards[j]);
            //            leftCards.Remove(cards[j+1]);
            //            pairIndex = j + 1;
            //            break;
            //        }

            //    }
            //if (pairIndex >0 && pairIndex < cards.Count -1)
            //{
            //    for (int j = pairIndex + 1; j < leftCards.Count - 1; j++)
            //    {
            //        if (cards[j].getNumber() == cards[j + 1].getNumber())
            //        {
            //            pairCards.Add(cards[j]);
            //            pairCards.Add(cards[j + 1]);
            //            leftCards.Remove(cards[j]);
            //            leftCards.Remove(cards[j + 1]);
            //            break;
            //        }

            //    }
            //}


            //put the pair cards first in the arranged cards
            if (pairCards.Count > 0)
            {
                arrangedCards.AddRange(pairCards);
            }
            
            //then put the left cards, the left cards are arranged by number from high to low already
            arrangedCards.AddRange(leftCards);

            return arrangedCards;
        }
        private bool isSameCardType(List<Card> cards)
        {
            if (cards.ElementAt(0).getCard_type() == cards.ElementAt(cards.Count - 1).getCard_type())
            {
                return true;
            }
            return false;
        }

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
        private List<Card> getSameKind(List<Card> cards, int kinds)
        {

            List<Card> sameKindCards = new List<Card>();
            List<Card> leftCards = new List<Card>();
            List<Card> arrangedCards = new List<Card>();

            int cardIndex = 0;

            for (int i = 0; i < cards.Count - kinds; i++)
                {
                    if (cards.ElementAt(i).getCard_type() == cards.ElementAt(i + kinds - 1).getCard_type())
                    {
                        
                        for (int j = 0; j < kinds; j++)
                        {
                            sameKindCards.Add(cards.ElementAt(i + j));
                        }
                    cardIndex = i;
                    break;
                    
                    } 
                }

            if (cardIndex > 0 )
            {
                for (int i = 0; i < cardIndex; i++)
                {
                    leftCards.Add(cards[i]);
                }
               
            }
            else if (cardIndex == 0 || (cardIndex + kinds) <= cards.Count)
            {
                for (int i = (cardIndex + kinds); i < cards.Count; i++)
                {
                    leftCards.Add(cards[i]);
                }
            }

            rankCards(leftCards);
            arrangedCards.AddRange(sameKindCards);
            arrangedCards.AddRange(leftCards);

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
                playerCodeDic[player] = getCardsCode(player.getPlayerCards());
                Console.WriteLine(player.getPlayerName(player) + playerCodeDic[player]);
            }
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
                case 3:  // two pairs
                    return getHighCardFromLeft(winnerCodeDic, TWO_PAIRS);
                case 2:  //one pair
                    return getHighCardFromLeft(winnerCodeDic, ONE_PAIR);
                case 1:  // high card
                    return getHighCardFromLeft(winnerCodeDic, HIGH_CARD);
            }
            
            return winnerCodeDic;
        }

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
        private Dictionary<Player, int> getHighCard (Dictionary<Player, int> playerCodeDic, int index)
        {
            Dictionary<Player, int> scannedCodeDic = new Dictionary<Player, int>();
            int highCard = 0;
            for (int i = 0; i < playerCodeDic.Count; i++)
            {
                if (playerCodeDic.Keys.ElementAt(i).getPlayerCards().ElementAt(index).getNumber() > highCard)
                {
                    highCard = playerCodeDic.Keys.ElementAt(i).getPlayerCards().ElementAt(0).getNumber();
                }              
            }

            for (int i = 0; i < playerCodeDic.Count; i++)
            {
                if (playerCodeDic.Keys.ElementAt(i).getPlayerCards().ElementAt(index).getNumber() == highCard)
                {
                    scannedCodeDic[playerCodeDic.Keys.ElementAt(i)] = playerCodeDic.Values.ElementAt(i);
                }
            }
            return scannedCodeDic;
        }

        private Dictionary<Player, int> getHighCardFromLeft(Dictionary<Player, int> playerCodeDic, int kinds)
        {
            Dictionary<Player, int> indexZeroCodeDic = getHighCard(playerCodeDic, 0);
            Dictionary<Player, int> indexFourCodeDic = new Dictionary<Player, int>();
            indexFourCodeDic = getHighCard(playerCodeDic, 4);
            Dictionary<Player, int> indexThreeCodeDic = new Dictionary<Player, int>();
            indexThreeCodeDic = getHighCard(playerCodeDic, 3);
            Dictionary<Player, int> indexTwoCodeDic = new Dictionary<Player, int>();
            indexTwoCodeDic = getHighCard(playerCodeDic, 2);
            Dictionary<Player, int> indexOneCodeDic = new Dictionary<Player, int>();
            indexOneCodeDic = getHighCard(playerCodeDic, 1);

            if (indexZeroCodeDic.Count > 1)
            {
                if (kinds == FOUR_OF_ONE_KIND)
                {
                    return indexFourCodeDic;
                } else if (kinds == THREE_OF_ONE_KIND)
                {                   
                    if (indexThreeCodeDic.Count > 1)
                    {
                        return indexFourCodeDic;
                    }
                    return indexThreeCodeDic;
                } else if (kinds == TWO_PAIRS)
                {
                    if (indexTwoCodeDic.Count > 1)
                    {
                        return indexFourCodeDic;
                    }
                    return indexTwoCodeDic;
                } else if (kinds == ONE_PAIR)
                {
                    if (indexTwoCodeDic.Count > 1)
                    {
                        if (indexThreeCodeDic.Count >1)
                        {
                            return indexFourCodeDic;
                        } else
                        {
                            return indexThreeCodeDic;
                        }
                    }
                    return indexTwoCodeDic;
                } else
                {
                    if (indexOneCodeDic.Count > 1)
                    {
                        if (indexTwoCodeDic.Count > 1)
                        {
                            if (indexThreeCodeDic.Count > 1)
                            {
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
        public void arrangePlayersCards(List<Player> players, int arrange_way)
        {
            if (arrange_way == ARRANGE_BY_KIND)
            {
                foreach (Player player in players)
                {
                    player.setPlayerCards(player.arrangeCardsByKind(player.getPlayerCards()));
                }
            }
            else
            {
                foreach (Player player in players)
                {
                    player.setPlayerCards(player.arrangeCardsByNum(player.getPlayerCards()));
                }
            }
        }
    
    }
}