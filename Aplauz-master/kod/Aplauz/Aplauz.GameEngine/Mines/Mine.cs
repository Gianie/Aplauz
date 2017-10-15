using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    class Mine // TODO Refactor
    {
        public int level { get; }
        public int prestige { get; }
        public string color { get; }
        public Dictionary<string, int> Price { get => price; }

        private Dictionary<string, int> price = new Dictionary<string, int>();

        public Mine(int level, string color, int prestige, string prices)
        {
            this.level = level;
            this.color = color;
            this.prestige = prestige;

            price.Add("white", prices[0] - 48);
            price.Add("blue", prices[1] - 48);
            price.Add("green", prices[2] - 48);
            price.Add("red", prices[3] - 48);
            price.Add("black", prices[4] - 48);
        }


    }
}
