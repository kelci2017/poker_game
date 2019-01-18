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
        [TestMethod]
        public void TestMethod1()
        {
            Game game = new Game(5);

            game.addPlayer(new Player("Joe"));
            game.addPlayer(new Player("Jen"));
            game.addPlayer(new Player("Bob"));

            //Print cards for each player
            List<Player> players = game.getAllPlayer();
            if (players != null)
            {
                foreach (Player player in players)
                {
                    player.printMyCards();
                }
            }
            
            game.deal(0, new Card(Constants.CARD_TYPE_HEART, 2));
            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 3));
            game.deal(0, new Card(Constants.CARD_TYPE_CLUB, 4));
            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 5));
            game.deal(0, new Card(Constants.CARD_TYPE_HEART, 10));

            game.deal(1, new Card(Constants.CARD_TYPE_CLUB, 5));
            game.deal(1, new Card(Constants.CARD_TYPE_DIAMOND, 7));
            game.deal(1, new Card(Constants.CARD_TYPE_HEART, 8));
            game.deal(1, new Card(Constants.CARD_TYPE_SPADE, 9));
            game.deal(1, new Card(Constants.CARD_TYPE_DIAMOND, 12));

            game.deal(2, new Card(Constants.CARD_TYPE_CLUB, 2));
            game.deal(2, new Card(Constants.CARD_TYPE_DIAMOND, 4));
            game.deal(2, new Card(Constants.CARD_TYPE_SPADE, 5));
            game.deal(2, new Card(Constants.CARD_TYPE_CLUB, 10));
            game.deal(2, new Card(Constants.CARD_TYPE_HEART, 11));

            //output the winner
            Dictionary<Player, int> winners = game.outputWinnerList(players);

            Console.WriteLine("the winner from calculation is: " + winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)));
            Assert.AreEqual(winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)), game.getPlayer(1).getPlayerName(game.getPlayer(1)));
        }

        public void TestMethod2()
        {
            Game game = new Game(5);

            game.addPlayer(new Player("Joe"));
            game.addPlayer(new Player("Bob"));
            game.addPlayer(new Player("Sally"));

            //Print cards for each player
            List<Player> players = game.getAllPlayer();
            if (players != null)
            {
                foreach (Player player in players)
                {
                    player.printMyCards();
                }
            }

            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 12));
            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 8));
            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 13));
            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 7));
            game.deal(0, new Card(Constants.CARD_TYPE_DIAMOND, 3));

            game.deal(0, new Card(Constants.CARD_TYPE_SPADE, 1));
            game.deal(0, new Card(Constants.CARD_TYPE_SPADE, 12));
            game.deal(0, new Card(Constants.CARD_TYPE_SPADE, 8));
            game.deal(0, new Card(Constants.CARD_TYPE_SPADE, 6));
            game.deal(0, new Card(Constants.CARD_TYPE_SPADE, 4));

            game.deal(0, new Card(Constants.CARD_TYPE_SPADE, 4));
            game.deal(0, new Card(Constants.CARD_TYPE_HEART, 4));
            game.deal(0, new Card(Constants.CARD_TYPE_HEART, 3));
            game.deal(0, new Card(Constants.CARD_TYPE_CLUB, 12));
            game.deal(0, new Card(Constants.CARD_TYPE_CLUB, 8));

            //output the winner
            Dictionary<Player, int> winners = game.outputWinnerList(players);

            Console.WriteLine("the winner from calculation is: " + winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)));
            Assert.AreEqual(winners.Keys.ElementAt(0).getPlayerName(winners.Keys.ElementAt(0)), game.getPlayer(1).getPlayerName(game.getPlayer(1)));
        }
    }
}
