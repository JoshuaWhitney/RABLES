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
    public partial class GameScreen : Form
    {
        //Game BJGame = new Game();
        GameState ActiveGame = new GameState();
            
        public GameScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(ActiveGame.deck.drawCard().toString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //BJGame = new Game();
            ActiveGame = new GameState();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActiveGame.BeginHand();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActiveGame.Hit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ActiveGame.Stand();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActiveGame.Double();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ActiveGame.Split();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ActiveGame.Surrender();
        }
    }
}
