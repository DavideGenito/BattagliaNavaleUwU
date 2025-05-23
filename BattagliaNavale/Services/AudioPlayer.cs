using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Services
{
    public class AudioPlayer
    {
        private static AudioPlayer _instance;
        public static AudioPlayer Instance => _instance ?? (_instance = new AudioPlayer());
    }
}
