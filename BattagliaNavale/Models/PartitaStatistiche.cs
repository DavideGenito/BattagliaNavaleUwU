using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class PartitaStatistiche
    {
        public int Id { get; set; }

        public Risultato RisultatoPartita { get; set; }
        
        public TimeSpan TempoPartita { get; set; }

        public string TempoPartitaTesto
        {
            get
            {
                return TempoPartita.Minutes.ToString() + ":" + TempoPartita.Seconds.ToString();
            }
        }

        public List<Tuple<int, int, bool>> BarcheBot { get; set; }

        public List<Tuple<int, int, bool>> BarchePlayer { get; set; }

        public List<Tuple<StatoCampo, int, int>> CampoBot { get; set; }

        public List<Tuple<StatoCampo, int, int>> CampoPlayer { get; set; }
    }
}
