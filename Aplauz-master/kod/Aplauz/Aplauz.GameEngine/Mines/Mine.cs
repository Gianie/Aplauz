using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class Mine // TODO Refactor
    {
        public int level { get; }
        public int prestige { get; }
        public string color { get; }
       

        public Dictionary<string, int> Prices = new Dictionary<string, int>();

        /// <summary>  
        ///  Prices sequence goes: w,b,g,r,k  
        /// </summary>
        public Mine(int level, string color, int prestige, string prices)
        {
            this.level = level;
            this.color = color;
            this.prestige = prestige;

            Prices.Add("w", prices[0] - 48);
            Prices.Add("b", prices[1] - 48);
            Prices.Add("g", prices[2] - 48);
            Prices.Add("r", prices[3] - 48);
            Prices.Add("k", prices[4] - 48);
        }

        public override string ToString()
        {
            string result = level + color + prestige + Prices["w"] + Prices["b"] + Prices["g"] +
                            Prices["r"] + Prices["k"];
            return result;
        }
    }
}
