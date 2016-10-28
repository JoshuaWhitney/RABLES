using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RABLES
{
    public partial class Form1 : Form
    {
        game BJGame = new game();
            
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(BJGame.drawCard().toString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BJGame.createDeck(6);
            BJGame.Shuffle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BJGame.playHand();
        }
    }
}
