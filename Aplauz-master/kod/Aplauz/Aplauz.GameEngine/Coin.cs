using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class Coin
    {

        public Coin(string color)
        {
            this.color = color;
        }

        readonly string color;

        public string Color => color;

    }
}
