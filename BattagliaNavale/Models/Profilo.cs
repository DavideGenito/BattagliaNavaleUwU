using BattagliaNavale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattagliaNavale
{
    public class Profilo
    {
        public int PartiteGiocate { get; private set; }
        public Risultato RisultatoPrecedente { get; private set; }
        public int VittorieSchiaccianti { get; private set; }
        public int PartitePerse {  get; private set; }
        public int PartiteVinte {  get; private set; }
        public Profilo(int partiteGiocate,Risultato risultatoPrecendente,int vittorieSchiaccianti,int partitePerse,int partiteVinte) 
        {
            PartiteGiocate = partiteGiocate;
            RisultatoPrecedente = risultatoPrecendente;
            VittorieSchiaccianti = vittorieSchiaccianti;
            PartitePerse = partitePerse;
            PartiteVinte = partiteVinte;
        }

    }
}