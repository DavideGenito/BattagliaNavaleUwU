using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class Player
    {
        public StatoTentativo[,] Campo {  get; private set; }
        public bool[,] CampoFeedback { get; private set; }
        public Player(StatoTentativo[,] campo, bool[,] campoFeedback,int posizioneNave1X,int posizioneNave1Y,int posizioneNave2X,int posizioneNave2Y,int posizioneNave3X, int posizioneNave3Y, int posizioneNave4X, int posizioneNave4Y)
        {
            Campo = campo;
            CampoFeedback = campoFeedback;
        }
        
            
        
    }
}
