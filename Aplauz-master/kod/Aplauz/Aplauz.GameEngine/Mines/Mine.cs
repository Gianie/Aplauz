using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class Mine // TODO Refactor
    {
        public int Level { get; }
        public int Prestige { get; }
        public string Color { get; }
       

        public Dictionary<string, int> Prices = new Dictionary<string, int>();

        /// <summary>  
        ///  Prices sequence goes: w,b,g,r,k  
        /// </summary>
        public Mine(int level, string color, int prestige, string prices)
        {
            this.Level = level;
            this.Color = color;
            this.Prestige = prestige;

            Prices.Add("w", prices[0] - 48);
            Prices.Add("b", prices[1] - 48);
            Prices.Add("g", prices[2] - 48);
            Prices.Add("r", prices[3] - 48);
            Prices.Add("k", prices[4] - 48);
        }

        public override string ToString()
        {
            string result = Level + Color + Prestige + Prices["w"] + Prices["b"] + Prices["g"] +
                            Prices["r"] + Prices["k"];
            return result;
        }
    }
}
