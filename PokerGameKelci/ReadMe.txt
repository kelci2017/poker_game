# Quick Start
<li>Download the code
<li>Import the project to Microsoft Visual Studio
<li>Note that main method is located in Program.cs.

# Description of main method
<pre>
//if you want each player get 5 cards in a game
//Game game = new Game(5);
game.addPlayer(new Player("Joe"));
game.addPlayer(new Player("Kelci"));
    	
//create a card dealer
Dealer dealer = new Dealer(game);
    	
//Shuffle before dealing
dealer.shuffle();
    	
//deal cards
dealer.deal();

//output the winner with some brief explaination behind winner name
//the game winner is processed in the Arbitrator
game.outputWinner(players)

//Different cards list are listed in the test case, player cards could be changed
Bob.setPlayerCards(threeKind);
</pre>