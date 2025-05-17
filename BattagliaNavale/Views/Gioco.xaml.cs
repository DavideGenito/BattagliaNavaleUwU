using BattagliaNavale.Models;
using BattagliaNavale.ViewModels;

namespace BattagliaNavale.Views;

public partial class Gioco : ContentPage
{
    public Gioco(StatoCampo[,] campoLogico)
    {
        InitializeComponent();
    }
}