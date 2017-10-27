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
            this.Color = color;
        }

        public Coin(Coin coin)
        {
            this.Color = coin.Color;

            this.Code = coin.Code;
        }

        public string Color { get; }

        public string Code { get; }


    }
}
