using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RABLES
{
    public class Deck 
    {
        private Random rng = new Random();
        public List<Card> deck = new List<Card>(); //Should be private

        public Deck(Deck copyDeck)
        {
            deck =  new List<Card>(copyDeck.getDeck());
            //Console.WriteLine("Creating new deck. orig deck has " + copyDeck.getDeck()[0].face + ", new deck has " + deck[0].face);
        }

        public Deck(int decks = 8)
        {
            /* Quarantine: Split test deck
            
            Card newCard0 = new Card(0, (char)('2'), 2);
            Card newCard1 = new Card(0, (char)('A'), 11);
            Card newCard12 = new Card(3, (char)('T'), 10);
            Card newCard2 = new Card(1, (char)('A'), 11);
            Card newCard3 = new Card(2, (char)('A'), 11);
            Card newCard4 = new Card(0, (char)('T'), 10);
            Card newCard5 = new Card(1, (char)('4'), 4);
            Card newCard6 = new Card(2, (char)('A'), 11);
            deck.Add(newCard0);
            deck.Add(newCard12);
            deck.Add(newCard1);
            deck.Add(newCard2);
            deck.Add(newCard3);
            deck.Add(newCard4);
            deck.Add(newCard5);
            deck.Add(newCard6);
            deck.Add(newCard0);
            deck.Add(newCard12);
            deck.Add(newCard12);
            deck.Add(newCard12);
            */
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

        public int GetNumCards()
        {
            return deck.Count;
        }

        public List<Card> getDeck()
        {
            return deck;
        }
    }
}
