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
        private List<Card> p1Cards;
        private List<Card> p1SplitCards;
        private double p1Chips;
        private double p1Bet;
        private int pTotal;
        private int dTotal;
        public bool playerTurn = false;

        public GameState()
        {
            deck.Shuffle();
            p1Chips = 1000;
        }

        public void BeginHand()
        {
            dCards = new List<Card>();
            p1Cards = new List<Card>();
            p1SplitCards = new List<Card>();
            dCards.Add(deck.DrawCard());
            dCards.Add(deck.DrawCard());
            p1Cards.Add(deck.DrawCard());
            p1Cards.Add(deck.DrawCard());

            Console.Write("You currently have " + p1Chips + "Enter bet ammount: ");
            p1Bet = 5;
            p1Chips -= p1Bet;

            Console.WriteLine("Dealer shows " + dCards[0].face);
            Console.WriteLine("\nYou have: " + p1Cards[0].face + " " + p1Cards[1].face);

            pTotal = p1Cards.Sum(item => item.value);
            dTotal = dCards.Sum(item => item.value);

            if (dTotal == 21 && pTotal == 21)
            {
                Console.WriteLine("Player pushes");
                p1Chips += p1Bet;
            }
            else if (pTotal == 21)
            {
                Console.WriteLine("Player has blackjack");
                p1Chips += p1Bet * 2.5;
            }
            else if(dTotal == 21)
            {
                Console.WriteLine("Dealer has blackjack");
            }
            else
            {
                playerTurn = true;
            }
        }

        public void Hit()
        {
            if (playerTurn == true)
            {
                p1Cards.Add(deck.DrawCard());
                checkAce(p1Cards);
                pTotal = p1Cards.Sum(item => item.value);
                Console.WriteLine("Player hits. You Drew " + p1Cards[p1Cards.Count - 1].face + ". You now have " + pTotal);
                if (pTotal > 21)
                {
                    Console.WriteLine("Player bust.");
                    playerTurn = false;
                    EndTurn();
                }
            }
        }

        public void Stand()
        {

            if (playerTurn == true)
            {
                Console.WriteLine("Player stands with " + p1Cards.Sum(item => item.value));
                playerTurn = false;
                EndTurn();
            }
        }

        public void Double()
        {
            if (playerTurn == true && p1Cards.Count == 2)
            {
                // Double bet - do when betting is added
                p1Chips -= p1Bet;
                p1Bet *= 2;
                p1Cards.Add(deck.DrawCard());
                checkAce(p1Cards);
                pTotal = p1Cards.Sum(item => item.value);
                Console.WriteLine("Player double downs. You Drew " + p1Cards[p1Cards.Count - 1].face + ". You now have " + pTotal);
                if (pTotal > 21)
                {
                    Console.WriteLine("Player bust.");
                }
                playerTurn = false;
                EndTurn();
            }
        }

        public void Split()
        {
            if (playerTurn == true && p1Cards.Count == 2 && p1Cards[0]. value == p1Cards[1].value)
            {
                Console.WriteLine("Player split.");
                p1SplitCards = new List<Card>();
                p1SplitCards.Add(p1Cards[0]);
                p1Cards.Remove(p1Cards[0]);
                p1SplitCards.Add(deck.DrawCard()); //Remove first card from hand, place into special hand;







                playerTurn = false;
                EndTurn();
            }
        }

        public void Surrender()
        {
            if (playerTurn == true)
            {
                Console.WriteLine("Player surrenders.");
                p1Chips += (p1Bet / 2);
                playerTurn = false;
            }
        }
        private void checkAce(List<Card> hand)
        {
            int sum = hand.Sum(item => item.value);
            foreach (Card curCard in hand)
            {
                if (sum < 21) return;
                if (curCard.value == 11)
                {
                    curCard.value = 1;
                }
            }
        }

        private void EndTurn()
        {
            Console.WriteLine("\n\nDealer has: " + dCards[0].face + " " + dCards[1].face + ", total of " + dTotal);

            while (dTotal < 17)
            {
                dCards.Add(deck.DrawCard());
                checkAce(dCards);
                dTotal += dCards[dCards.Count - 1].value;
                Console.WriteLine("Dealer drew " + dCards[dCards.Count - 1].face + ". Dealer now has " + dTotal);
            }

            if (pTotal > 21)
            {
                Console.WriteLine("Player busts.");
            }
            else if (dTotal > 21)
            {
                Console.WriteLine("Dealer busts.");
                p1Chips += p1Bet * 2;
            }
            else if(pTotal > dTotal)
            {
                Console.WriteLine("Player beats dealer.");
                p1Chips += p1Bet * 2;
            }
            else if (pTotal == dTotal)
            {
                Console.WriteLine("Player pushes.");
                p1Chips += p1Bet;
            }
            else
            {
                Console.WriteLine("Dealer beats Player.");
            }
        }
    }
}
