using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public interface IGenerator
    {
        public int GeneraMossaX (int maxX);

        public int GeneraMossaY (int maxY);

        public bool GeneraOrientamentoBarca();

        public int GeneraOrientamentoMossa();
    }
}
