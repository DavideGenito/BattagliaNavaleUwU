using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public interface IGenerator
    {
        public int GeneraMossa(int maxX, int maxY);

    }
}
