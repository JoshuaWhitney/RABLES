using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RABLES
{
    class GameState
    {
        private Deck deck = new Deck();
        private List<Card> dCards;
        private List<Card> playerInitialHand;
        private List<List<Card>> allHands = new List<List<Card>>(); // Array of hands for improved splitting
        private List<int> handTotals = new List<int>();
        private List<double> allBets = new List<double>();
        private AI solver = new AI();
        private int curHand = 0;
        private int betAmount;
        private int playerStartSum;
        private double ROI;

        private List<Card> p1Cards;
        private List<Card> p1SplitCards;
        private double p1Chips;
        private double p1Bet;
        private int pTotal;
        private int pSTotal;
        private int dTotal;
        private int splitActive = 0; // 0 = no split, 1 = split hand is in play, 2 = split hand as finished
        public bool playerTurn = false;

        public GameState()
        {
            deck.Shuffle();
            p1Chips = 1000;
            dCards = new List<Card>();
        }

        public void BeginHand()
        {
            if (deck.GetNumCards() < 54) { deck = new Deck(); deck.Shuffle(); }
            Console.WriteLine("Cards remaining: " + deck.GetNumCards());
            curHand = 0;
            ROI = 0;
            dCards.Clear();
            allHands.Clear();
            handTotals.Clear();

            allHands.Add(new List<Card>()); //Add first hand
            //p1SplitCards = new List<Card>();
            dCards.Add(deck.DrawCard());
            dCards.Add(deck.DrawCard());
            foreach (List<Card> hand in allHands){
                hand.Add(deck.DrawCard());
                hand.Add(deck.DrawCard());
            }
            playerInitialHand = new List<Card>(allHands[0]);

            Console.Write("You currently have " + p1Chips + "Enter bet ammount: ");
            betAmount = 5;
            allBets.Clear();
            allBets.Add(betAmount);
            //p1Bet = 5;
            p1Chips -= allBets[0];

            Console.WriteLine("Dealer shows " + dCards[0].face);

            int i = 0;
            foreach (List<Card> hand in allHands)
            {
                Console.WriteLine("\n Hand " + i + "has: " + hand[0].face + " " + hand[1].face);
                handTotals.Add(hand.Sum(item => item.value));
            }

            dTotal = dCards.Sum(item => item.value);

            while (curHand < allHands.Count && playerTurn == false)
            {
                if (HandPrecheck())
                {
                    playerTurn = true;
                }
                else
                {
                    curHand++;
                }
            }   
        }

        private bool HandPrecheck()
        {
            if (allHands[curHand].Count < 2)
            {
                allHands[curHand].Add(deck.DrawCard());
            }
            if (dTotal == 21 && handTotals[curHand] == 21)
            {
                Console.WriteLine("Player pushes");
                p1Chips += allBets[curHand];
            }
            else if (handTotals[curHand] == 21)
            {
                Console.WriteLine("Player has blackjack");
                p1Chips += allBets[curHand] * 2.5;
            }
            else if (dTotal == 21)
            {
                Console.WriteLine("Dealer has blackjack");
            }
            else
            {
                return true;
            }
            return false;
        }

        public void Hit()
        {
            if (playerTurn == true)
            {
                /*
                if (splitActive == 1)
                {
                    p1SplitCards.Add(deck.DrawCard());
                    checkAce(p1SplitCards);
                    pSTotal = p1SplitCards.Sum(item => item.value);
                    Console.WriteLine("Player hits. You Drew " + p1SplitCards[p1SplitCards.Count - 1].face + ". You now have " + pSTotal);
                    if (pSTotal > 21)
                    {
                        Console.WriteLine("Player bust.");
                        splitActive = 2;
                    }
                }
                else
                {
                
                */
                allHands[curHand].Add(deck.DrawCard());
                handTotals[curHand] = allHands[curHand].Sum(item => item.value);

                checkAce();
                Console.WriteLine("Player hits. You Drew " + allHands[curHand][allHands[curHand].Count - 1].face + ". You now have " + handTotals[curHand]);
                if (handTotals[curHand] > 21)
                {
                    Console.WriteLine("Hand " + curHand + " busts.");
                    curHand++;
                    if (curHand >= allHands.Count)
                    {
                        playerTurn = false;
                        EndTurn();
                    }
                }    
            }
        }

        public void Stand()
        {
            if (playerTurn == true)
            {
                Console.WriteLine("Hand " + curHand + " stands with " + allHands[curHand].Sum(item => item.value));
                curHand++;
                if (curHand >= allHands.Count)
                {
                    playerTurn = false;
                    EndTurn();
                }
            }
        }

        public void Double()
        {
            if (playerTurn == true && allHands[curHand].Count == 2)
            {
                // Double bet - do when betting is added
                p1Chips -= allBets[curHand];
                allBets[curHand] *= 2;
                allHands[curHand].Add(deck.DrawCard());
                handTotals[curHand] = allHands[curHand].Sum(item => item.value);

                checkAce();
                //handTotals[curHand] = allHands[curHand].Sum(item => item.value);
                Console.WriteLine("Player double downs. You Drew " + allHands[curHand][allHands[curHand].Count - 1].face + ". You now have " + handTotals[curHand]);
                if (handTotals[curHand] > 21)
                {
                    Console.WriteLine("Hand " + curHand + " busts.");
                }
                curHand++;
                if (curHand >= allHands.Count)
                {
                    playerTurn = false;
                    EndTurn();
                }
            }
        }

        public void Split()
        {

            if (playerTurn == true && allHands[curHand].Count == 2 && allHands[curHand][0]. value == allHands[curHand][1].value)
            {
                Console.WriteLine("Player split.");
                List<Card> temp = new List<Card>();
                allHands.Add(temp);
                allHands[allHands.Count-1].Add(allHands[curHand][1]);
                allHands[curHand].Remove(allHands[curHand][1]);

                allHands[curHand].Add(deck.DrawCard()); //Remove first card from hand, place into special hand;
                allHands[allHands.Count-1].Add(deck.DrawCard());

                handTotals[curHand] = allHands[curHand].Sum(item => item.value);
                handTotals.Add(allHands[allHands.Count-1].Sum(item => item.value));

                allBets.Add(betAmount);
                p1Chips -= betAmount;

                int i = 0;
                foreach (List<Card> hand in allHands)
                {
                    Console.WriteLine("\nHand " + i + "has: " + hand[0].face + " " + hand[1].face);
                    handTotals.Add(hand.Sum(item => item.value));
                    i++;
                }
            }
        }

        public void Surrender()
        {
            if (playerTurn == true)
            {
                Console.WriteLine("Player surrenders.");
                p1Chips += (allBets[curHand] / 2);
                playerTurn = false;
                ROI = -0.5;
            }
        }

        private void checkAce()
        {
            //handTotals = hand.Sum(item => item.value);
            foreach (Card curCard in allHands[curHand])
            {
                if (curCard.value == 11 && handTotals[curHand] > 21)
                {
                    curCard.value = 1;
                    handTotals[curHand] = allHands[curHand].Sum(item => item.value);
                }
            }
        }

        private void checkDealerAce(List<Card> hand)
        {
            int sum = hand.Sum(item => item.value);
            foreach (Card curCard in hand)
            {
                if (curCard.value == 11 && sum > 21)
                {
                    curCard.value = 1;
                }
                sum = hand.Sum(item => item.value);
            }
        }

        private void EndTurn()
        {
            
            Console.WriteLine("\n\nDealer has: " + dCards[0].face + " " + dCards[1].face + ", total of " + dTotal);

            while (dTotal < 17)
            {
                dCards.Add(deck.DrawCard());
                checkDealerAce(dCards);
                dTotal = dCards.Sum(item => item.value);
                Console.WriteLine("Dealer drew " + dCards[dCards.Count - 1].face + ". Dealer now has " + dTotal);
            }

            if (dTotal > 21)
            {
                Console.WriteLine("Dealer busts.");
            }


            for (int evalHand = 0; evalHand < allHands.Count; evalHand++)
            {
                if (handTotals[evalHand] > 21)
                {
                    Console.WriteLine("Hand " + evalHand + " busts.\nWin 0.");
                    ROI -= allBets[evalHand]/betAmount;
                }
                else if (dTotal > 21)
                {
                    p1Chips += allBets[evalHand] * 2;
                    Console.WriteLine("Win " + allBets[evalHand] * 2);
                    ROI += allBets[evalHand] / betAmount;
                }
                else if (handTotals[evalHand] > dTotal)
                {
                    Console.WriteLine("Hand " + evalHand + " beats dealer.\nWin " + allBets[evalHand] * 2);
                    p1Chips += allBets[evalHand] * 2;
                    ROI += allBets[evalHand] / betAmount;
                }
                else if (handTotals[evalHand] == dTotal)
                {
                    Console.WriteLine("Hand " + evalHand + " pushes.Win " + allBets[evalHand]);
                    p1Chips += allBets[evalHand];
                }
                else
                {
                    Console.WriteLine("Dealer beats Hand " + evalHand + "\nWin 0.");
                    ROI -= allBets[evalHand] / betAmount;
                }
            }
        }

        public void AISolve()
        {

            while(playerTurn == true)
            {
                bool canDouble = allHands[curHand].Count == 2;
                bool canSplit = true;
                bool canSurrender = true;
                bool matchingCards = (allHands[curHand][0].value == allHands[curHand][1].value || allHands[curHand][0].face == allHands[curHand][1].face) && allHands[curHand].Count == 2;

                int sol = solver.Solve(allHands[curHand], dCards[0].value, canDouble, canSplit, canSurrender, matchingCards);//Pass list of player cards, dealer up card, can double, can split

                //Output: Hit 0, Stand 1, Double 2, Surrender 3, Split 4, invalid -1
                bool temp1 = allHands[curHand].Count == 2, temp2 = allHands[curHand][0].value == allHands[curHand][1].value;
                Console.WriteLine("count == 2: " + temp1 + "count == " + allHands[curHand].Count + ";[0] == [1]: " + temp2 + "; debug: " + true);
                Console.Write("AI says to ");
                switch (sol)
                {
                    case 0:
                        Console.WriteLine("hit");
                        Hit();
                        break;
                    case 1:
                        Console.WriteLine("stand");
                        Stand();
                        break;
                    case 2:
                        Console.WriteLine("double");
                        Double();
                        break;
                    case 3:
                        Console.WriteLine("surrender");
                        Surrender();
                        break;
                    case 4:
                        Console.WriteLine("split");
                        if (allHands[curHand][0].face == 'A' && allHands[curHand][1].face == 'A')
                        {
                            allHands[curHand][0].value = 11;
                            allHands[curHand][1].value = 11;
                        }
                        Split();
                        break;
                }
            }
        }

        public void AITrain()
        {
            double hitRet, standRet, doubleRet, splitRet, surrRet;
            Deck deckHit = new Deck(deck);
            Deck deckStand = new Deck(deck);
            Deck deckDouble = new Deck(deck);
            Deck deckSplit = new Deck(deck);
            Deck deckSurr = new Deck(deck);

            deck = deckHit;
            BeginHand();
            if (playerTurn == false) return;
            Hit();
            AISolve();
            hitRet = ROI;

            deck = deckStand;
            BeginHand();
            Stand();
            standRet = ROI;

            deck = deckDouble;
            BeginHand();
            Double();
            doubleRet = ROI;

            deck = deckSurr;
            BeginHand();
            Surrender();
            surrRet = ROI;

            deck = deckSplit;
            BeginHand();
            Split();
            AISolve();
            splitRet = ROI;
            
            Console.WriteLine("Hit returns " + hitRet + "; Stand returns " + standRet + "; Sur returns " + surrRet + "; Split returns " + splitRet);
            
            solver.update(playerInitialHand, dCards[0].value, hitRet, standRet, doubleRet, surrRet, splitRet);
            //allHands[0]
        }

        public void AIPrint()
        {
            solver.Print();
        }

        public void AISave()
        {
            solver.Save();
        }

        public void AILoad()
        {
            solver.Load();
        }
    }
}
