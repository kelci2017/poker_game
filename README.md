# poker game with c#
 
 ## Quick Start
* Download the code
* Import the project to Microsoft Visual Studio
* Note that main method is located in Program.cs.

## Idea
* The rules could be find in https://www.adda52.com/poker/poker-rules/cash-game-rules/tie-breaker-rules
* It's a console application
* Objects were created (Card, Dealer, Deck, Game, Player, Arbitrator)
* Players can be added in the game, the dealer shuffle the cards and distribute cards to player
* The arbitrator output the winner
* Different hands were listed in the test case, such as flush, straight, pairs...

## Description of main method

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

## Description of output winner method in Arbitrator object

* When comparing the players' cards, mainly compare the number and suit, so each player's cards were arranged with suit(type) or number with function arrangePlayersCards(Player player, int arrange_way) 
* Based on the rules, the playercodedic is defined, when output the winner, the player who has the higher code wins
* The code for each player is in the function getCardsCode(Player player, List<Card> cards)
* get higher card player with getHighCard (Dictionary<Player, int> playerCodeDic, int ind
* The next consideration is that if players have same codes, then the players with same codes were compared with high card or with left cards
* The functions used when players have same codes were: isSameCardType(Check the card suit), isStraightCards, getSameKind, getHighCard, getHighPairCardFromLeft, getHighCardFromLeft 

