# Rules-Agnostic Blackjack Learning and Execution System

# Abstract
The purpose of this project is to develop an AI that can determine the optimal behavior while playing Blackjack under multiple rule sets. Through the Monte Carlo Method, the AI can attempt to learn the best strategy and act as a guide when learninga specific variation of Blackjack.   

# Problem Description
Blackjack is a relatively simple card game — the dealer and player are both dealt two cards. The player can see both of their cards and one of the Dealer’s. The player wants to try and score as close to 21 as possible without exceeding, with up to five possible choices of actions available. While Blackjack is a solved game, each solution relies on a number assumption, aspects of the rules that can and do vary from casino to casino. 

My goal was to create a program that could learn the best way to play Blackjack when presented with a list of alterations to the game and optional rules in play. Though repeated practice, the program would be able determine the correct move of any situation and generate a table that a player could understand and reference.

# Methods
The AI relies on the Monte Carlo Method to get data for a specific ruleset about which option yielded winning results, stored in a trio of hash tables. Through repeated play, the program, over time should record the average return for an option on a specific hand — the highest of these is the “best.”

# Results
Initial results were interesting. For the scope of this project, the program was run under two rule sets, only varying by whether the dealer hits or stands on soft 17 (A hand totaling 17, where one card is a ace counting for 11 points). As expected, the AI’s output varied based on which rule set it was playing under. This output can be seen below.

Interestingly, the AI had some very unusual choices  recorded as being optimal. Most player strategy guides do not recommend surrendering with a soft hand, ever. I suspect coding issues with the Blackjack game system may be the cause.

![Alt text](https://dl.dropboxusercontent.com/u/4353601/MiscImages/usu/resultsHit.png "HotOnSoft17")
![Alt text](https://dl.dropboxusercontent.com/u/4353601/MiscImages/usu/resultsStand.png "StandOnSoft17")

Additionally, with the AI learning complete and recorded, the practiced playing repeated rounds as if it were a real player, recording the number of chips in its possession after each hand. A mismatch of strategies (i.e. the AI plays as if the dealer hits on a soft 17, when the dealer is actually hitting) was run to observe how important it was to play according to the correct strategy.

Although  all versions of the AI eventually reach 0 chips (The house always wins), it can be seen that the non-mismatched version of the AI performed significantly better than the mismatch versions.
![Alt text](https://dl.dropboxusercontent.com/u/4353601/MiscImages/usu/BJLineGraph.png "ChipCountGraph")

# Conclusion
The AI has significant limitations in learning how to play blackjack optimally, though I suspect some of the blame may lie with the program it plays in. Even so, it’s clear how important it to play to the correct strategy.
