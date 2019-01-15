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
            
            //Print cards for each player
            if (players != null)
            {
                foreach (Player player in players)
                {
                    player.printMyCards();
                }
            }

            //output the winner
            List<Player> winners = game.outputWinnerList(players);

            String winnersName = "";
            foreach (Player player in winners)
            {
                winnersName = winnersName + " " + player.getPlayerName(player);
            }
            Console.WriteLine("The winners are: " + winnersName);
            
            Console.ReadKey();
        }
    }
}
