using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Defunct

namespace RABLES
{
    class Game
    {
        GameState activeGame = new GameState();

        public Game()
        {
            activeGame.BeginHand();
            activeGame.BeginHand();
        }
    }
}
