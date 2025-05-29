using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattagliaNavale.Models;
using BattagliaNavale.Infrastucture;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace BattagliaNavale.ViewModels
{
    public partial class VisualizzatoreCampoViewModel : ObservableObject
    {
        public PartitaStatistiche partitaCorrente;

        public VisualizzatoreCampoViewModel(int id)
        {
            partitaCorrente = PreferencesUtilities.GetField(id);
        }

        
    }
}
