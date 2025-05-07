using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    internal class RandomGenerator : IGenerator
    {
        private Random random = new Random();

        public int GeneraMossaX(int maxX)
        {
            return random.Next(0, maxX);
        }

        public int GeneraMossaY(int maxY)
        {
            return random.Next(0, maxY);
        }
    }
}
