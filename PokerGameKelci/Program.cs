using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameKelci
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(5);

            game.addPlayer(new Player("Joe"));
            game.addPlayer(new Player("Bob"));
            game.addPlayer(new Player("Sally"));

            //create a card dealer
            Dealer dealer = new Dealer(game);

            //shuffle cards
            dealer.shuffle();

            //deal cards
            dealer.deal();

            List<Player> players = game.getAllPlayer();

            //output the winner
            Dictionary<Player, int> winners = game.outputWinnerList(players);
            String winnersName = "";

            for (int i = 0; i < winners.Count; i++)
            {
                winnersName = winnersName + " " + winners.Keys.ElementAt(i).getPlayerName(winners.Keys.ElementAt(i)) + " (" + getCard_code_desc(winners.Values.ElementAt(i)) + ")";
                Console.WriteLine("The winners name is: " + winners.Keys.ElementAt(i).getPlayerName(winners.Keys.ElementAt(i)));
            }

            //Print cards for each player
            if (players != null)
            {
                foreach (Player player in players)
                {
                    player.printMyCards();
                }
            }

            Console.WriteLine("The winners are: " + winnersName);

            Console.ReadKey();

        }
        public static String getCard_code_desc(int card_code)
        {
            switch (card_code)
            {
                case 10:
                    return Constants.STR_ROYAL_FLUSH;
                case 9:
                    return Constants.STR_STRAIGHT_FLUSH;
                case 8:
                    return Constants.STR_FOUR_ONE_KIND;
                case 7:
                    return Constants.STR_FULL_HOUSE;
                case 6:
                    return Constants.STR_FLUSH;
                case 5:
                    return Constants.STR_STRAIGHT;
                case 4:
                    return Constants.STR_THREE_ONE_KIND;
                case 3:
                    return Constants.STR_PAIRS;
                case 2:
                    return Constants.STR_HIGH_CARD;
                default:
                    return "";
            }

        }
    }

}