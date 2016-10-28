using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//enum show { break0, break1, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king, ace}


namespace RABLES
{
    class Card
    {
        public int value { get; set; } // 1 to 11 (ace)
        public char face { get; set; } // 11 = jack; 12 = queen; 13 = king; 14 = ace
        public int suit { get; set; } // spades, hearts, diamonds, clubs

        public Card(int inSuit, char inface, int inValue)
        {
            suit = inSuit;
            face = inface;
            value = inValue;
        }

        public string toString()
        {
            string output = "";
            if (value < 10)
                output += face.ToString();
            else if (face == 'T')
                output += "Ten";
            else if (face == 'J')
                output += "Jack";
            else if (face == 'Q')
                output += "Queen";
            else if (face == 'K')
                output += "King";
            else if (face == 'A')
                output += "Ace";

            if (suit == 0)
                output += " of spades";
            else if (suit == 1)
                output += " of hearts";
            else if (suit == 2)
                output += " of diamonds";
            else if (suit == 3)
                output += " of clubs";

            return output;
        }
    }
}
