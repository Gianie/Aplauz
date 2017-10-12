using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine;

namespace Aplauz.Launcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var board = new Board();
            board.StartNewGame(new[] {"adam", "basia", "cyprian", "danuta"});
        }
    }
}
