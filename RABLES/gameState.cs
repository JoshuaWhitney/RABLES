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
        public bool playerTurn = false;

        public GameState()
        {
            deck.Shuffle();
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

            Console.WriteLine("Dealer shows " + dCards[0].face);
            Console.WriteLine("\nYou have: " + p1Cards[0].face + " " + p1Cards[1].face);

            if (p1Cards.Sum(item => item.value) == 21)
            {
                Console.WriteLine("Player has blackjack");
                //pay back 3:2 if dealer doesn't also have blackjack
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
                int pTotal = p1Cards.Sum(item => item.value);
                Console.WriteLine("Player stands. You Drew " + p1Cards[p1Cards.Count - 1].face + ". You now have " + pTotal);
                if (pTotal > 21)
                {
                    Console.WriteLine("Player bust.");
                    playerTurn = false;
                    DealerAction();
                }
            }
        }

        public void Stand()
        {

            if (playerTurn == true)
            {
                Console.WriteLine("Player stands with " + p1Cards.Sum(item => item.value));
                playerTurn = false;
                DealerAction();
            }
        }

        public void Double()
        {
            if (playerTurn == true && p1Cards.Count == 2)
            {
                // Double bet - do when betting is added
                p1Cards.Add(deck.DrawCard());
                int pTotal = p1Cards.Sum(item => item.value);
                Console.WriteLine("Player double downs. You Drew " + p1Cards[p1Cards.Count - 1].face + ". You now have " + pTotal);
                if (pTotal > 21)
                {
                    Console.WriteLine("Player bust.");
                }
                playerTurn = false;
                DealerAction();
            }
        }

        public void Split()
        {
            if (playerTurn == true && p1Cards.Count == 2 && p1Cards[0]. value == p1Cards[1].value)
            {
                Console.WriteLine("Player split.");
                playerTurn = false;
                DealerAction();
            }
        }

        public void Surrender()
        {
            if (playerTurn == true)
            {

            }
        }

        private void DealerAction()
        {
            int dTotal = dCards.Sum(item => item.value);

            Console.WriteLine("\n\nDealer has: " + dCards[0].face + " " + dCards[1].face + ", total of " + dTotal);

            while (dTotal < 17)
            {
                dCards.Add(deck.DrawCard());
                dTotal += dCards[dCards.Count - 1].value;
                Console.WriteLine("Dealer drew " + dCards[dCards.Count - 1].face + ". Dealer now has " + dTotal);
            }
        }
    }
}
