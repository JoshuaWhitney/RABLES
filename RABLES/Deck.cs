using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RABLES
{
    class Deck
    {
        private Random rng = new Random();
        private List<Card> deck = new List<Card>();

        public Deck(int decks = 8)
        {
            for (int i = 0; i < decks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 2; k < 10; k++)
                    {
                        Card newCard = new Card(j, (char)(k + 48), k);
                        deck.Add(newCard);
                        //Console.WriteLine("Created " + k + " of " + j + "for deck " + i);
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        char[] temp = { 'T', 'J', 'Q', 'K' };
                        Card newCard = new Card(j, temp[k], 10);
                        deck.Add(newCard);
                        //Console.WriteLine("Created " + k + " of " + j + "for deck " + i);
                    }
                    Card aceCard = new Card(j, 'A', 11);
                    deck.Add(aceCard);
                    //Console.WriteLine("Created ace of " + j + " for deck " + i);
                }
            }
            Console.WriteLine("Cards: " + deck.Count());
        }

        //pulls a card, and removes it from the list
        public Card DrawCard()
        {
            Card card = deck[0];
            deck.Remove(card);
            return card;
        }

        //shuffle function lightly modified version of http://stackoverflow.com/questions/273313/randomize-a-listt/1262619#1262619
        public void Shuffle()
        {
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }
    }
}
