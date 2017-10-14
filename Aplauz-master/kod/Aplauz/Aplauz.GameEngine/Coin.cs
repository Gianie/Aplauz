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
            if (color == "black")
                Code = "k";
            else
            {
                Code = color[0].ToString();
            }
        }

        public string Color { get; }

        public string Code { get; }


    }
}
