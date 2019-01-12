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

            game.addPlayer(new Player("Jack"));
            game.addPlayer(new Player("Tonny"));

            //create a card dealer
            Dealer dealer = new Dealer(game);

            //shuffle cards
            dealer.shuffle();

            //deal cards
            dealer.deal();

            //Print cards for each player
            List<Player> players = game.getAllPlayer();
            if (players != null)
            {
                foreach (Player player in players)
                {
                    player.printMyCards();
                }
            }
            Console.ReadKey();
        }
    }
}
