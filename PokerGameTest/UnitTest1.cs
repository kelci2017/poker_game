using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGameKelci;
using System.Linq;

namespace PokerGameTest
{
    [TestClass]
    public class UnitTest1
    {
        Player Joe = new Player("Joe");
        Player Jen = new Player("Jen");
        Player Bob = new Player("Bob");
        Player Sally = new Player("Sally");
        //13H 10H 14H 12H 11H
        List<Card> royalFlush = new List<Card>{new Card(Constants.CARD_TYPE_HEART, 13), new Card(Constants.CARD_TYPE_HEART, 10), new Card(Constants.CARD_TYPE_HEART, 12), new Card(Constants.CARD_TYPE_HEART, 11), new Card(Constants.CARD_TYPE_HEART, 14)};
        //8C 7C 6C 5C 4C
        List<Card> straightFlush = new List<Card> { new Card(Constants.CARD_TYPE_CLUB, 7), new Card(Constants.CARD_TYPE_CLUB, 8), new Card(Constants.CARD_TYPE_CLUB, 5), new Card(Constants.CARD_TYPE_CLUB, 6), new Card(Constants.CARD_TYPE_CLUB, 4) };
        //C9 H9 S9 D9 6S
        List<Card> fourKind = new List<Card> { new Card(Constants.CARD_TYPE_DIAMOND, 9), new Card(Constants.CARD_TYPE_SPADE, 6), new Card(Constants.CARD_TYPE_HEART, 9), new Card(Constants.CARD_TYPE_SPADE, 9), new Card(Constants.CARD_TYPE_CLUB, 9) };
        //S7 D7 H7 D2 S2
        List<Card> fullHouse = new List<Card> { new Card(Constants.CARD_TYPE_DIAMOND, 7), new Card(Constants.CARD_TYPE_SPADE, 2), new Card(Constants.CARD_TYPE_HEART, 7), new Card(Constants.CARD_TYPE_DIAMOND, 2), new Card(Constants.CARD_TYPE_SPADE, 7) };
        //S4 H4 C4 D3 S5
        List<Card> threeKind = new List<Card> { new Card(Constants.CARD_TYPE_DIAMOND, 3), new Card(Constants.CARD_TYPE_CLUB, 4), new Card(Constants.CARD_TYPE_SPADE, 4), new Card(Constants.CARD_TYPE_SPADE, 5), new Card(Constants.CARD_TYPE_HEART, 4) };
        //D3 S3 4D 4S 7H
        List<Card> twoPairs = new List<Card> { new Card(Constants.CARD_TYPE_DIAMOND, 3), new Card(Constants.CARD_TYPE_SPADE, 3), new Card(Constants.CARD_TYPE_DIAMOND, 4), new Card(Constants.CARD_TYPE_HEART, 7), new Card(Constants.CARD_TYPE_SPADE, 4) };
        //14C 14D 2D 5C 8D
        List<Card> onePair = new List<Card> { new Card(Constants.CARD_TYPE_DIAMOND, 2), new Card(Constants.CARD_TYPE_CLUB, 5), new Card(Constants.CARD_TYPE_CLUB, 14), new Card(Constants.CARD_TYPE_DIAMOND, 8), new Card(Constants.CARD_TYPE_DIAMOND, 14) };
        //12S 11D 10C 9D 8S
        List<Card> straight = new List<Card> { new Card(Constants.CARD_TYPE_SPADE, 12), new Card(Constants.CARD_TYPE_SPADE, 8), new Card(Constants.CARD_TYPE_DIAMOND, 9), new Card(Constants.CARD_TYPE_DIAMOND, 11), new Card(Constants.CARD_TYPE_CLUB, 10) };
        //8H 5H 2H 3H 4H
        List<Card> flush = new List<Card> { new Card(Constants.CARD_TYPE_HEART, 4), new Card(Constants.CARD_TYPE_HEART, 5), new Card(Constants.CARD_TYPE_HEART, 2), new Card(Constants.CARD_TYPE_HEART, 3), new Card(Constants.CARD_TYPE_HEART, 8) };
        //3H 7S 8C 4D 8D
        List<Card> pairCard = new List<Card> { new Card(Constants.CARD_TYPE_HEART, 3), new Card(Constants.CARD_TYPE_SPADE, 7), new Card(Constants.CARD_TYPE_CLUB, 9), new Card(Constants.CARD_TYPE_DIAMOND, 4), new Card(Constants.CARD_TYPE_DIAMOND, 8) };
        //2H 3D 4C 5D 10H
        List<Card> highCard1 = new List<Card> { new Card(Constants.CARD_TYPE_HEART, 2), new Card(Constants.CARD_TYPE_DIAMOND, 3), new Card(Constants.CARD_TYPE_CLUB, 4), new Card(Constants.CARD_TYPE_DIAMOND, 5), new Card(Constants.CARD_TYPE_HEART, 10) };
        //5C 7D 8H 9S 12D
        List<Card> highCard2 = new List<Card> { new Card(Constants.CARD_TYPE_CLUB, 5), new Card(Constants.CARD_TYPE_DIAMOND, 7), new Card(Constants.CARD_TYPE_HEART, 8), new Card(Constants.CARD_TYPE_SPADE, 9), new Card(Constants.CARD_TYPE_DIAMOND, 12) };
        //2C 4D 5S 10C 12H 
        List<Card> highCard3 = new List<Card> { new Card(Constants.CARD_TYPE_CLUB, 2), new Card(Constants.CARD_TYPE_DIAMOND, 4), new Card(Constants.CARD_TYPE_SPADE, 5), new Card(Constants.CARD_TYPE_CLUB, 10), new Card(Constants.CARD_TYPE_HEART, 12) };

        [TestMethod]
        public void TestFlush()
        {
            Game game = new Game(5);

            game.addPlayer(Joe);
            game.addPlayer(Jen);
            game.addPlayer(Bob);

            List<Player> players = game.getAllPlayer();

            Joe.setPlayerCards(onePair);
            Jen.setPlayerCards(twoPairs);
            Bob.setPlayerCards(threeKind);

            //output the winner
            Dictionary<Player, int> winners = game.outputWinnerList(players);

            Console.WriteLine("the winner from calculation is: " + winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)));
            Assert.AreEqual(winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)), game.getPlayer(2).getPlayerName(game.getPlayer(2)));
        }

        [TestMethod]
        public void TestPair()
        {
            Game game = new Game(5);

            game.addPlayer(Joe);
            game.addPlayer(Jen);
            game.addPlayer(Bob);

            //Print cards for each player
            List<Player> players = game.getAllPlayer();

            Joe.setPlayerCards(onePair);
            Jen.setPlayerCards(twoPairs);
            Bob.setPlayerCards(pairCard);

            //output the winner
            Dictionary<Player, int> winners = game.outputWinnerList(players);

            Console.WriteLine("the winner from calculation is: " + winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)));
            Assert.AreEqual(winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)), game.getPlayer(0).getPlayerName(game.getPlayer(0)));
        }

        [TestMethod]
        public void TestHighCard()
        {
            Game game = new Game(5);

            game.addPlayer(Joe);
            game.addPlayer(Jen);
            game.addPlayer(Bob);

            //Print cards for each player
            List<Player> players = game.getAllPlayer();

            Joe.setPlayerCards(highCard1);
            Jen.setPlayerCards(highCard2);
            Bob.setPlayerCards(highCard3);

            //output the winner
            Dictionary<Player, int> winners = game.outputWinnerList(players);

            Console.WriteLine("the winner from calculation is: " + winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)));
            Assert.AreEqual(winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)), game.getPlayer(2).getPlayerName(game.getPlayer(2)));
        }
    }
}
