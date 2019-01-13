using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGameKelci;

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

            game.outputWinner(players);
        }
    }
}
