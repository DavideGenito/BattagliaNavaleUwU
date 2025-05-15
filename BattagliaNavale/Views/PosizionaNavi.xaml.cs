using CommunityToolkit.Mvvm.Input;
using BattagliaNavale.ViewModels;

namespace BattagliaNavale.Views;

public partial class PosizionaNavi : ContentPage
{
	const int LUNGHEZZA = 10;
	public PosizionaNavi()
	{
		InitializeComponent();

        BindingContext = new ViewModels.PosizionaNaviViewModel(this.Navigation);

        for (int i = 0;  i < LUNGHEZZA; i++)
		{
			RowDefinition riga = new RowDefinition();
			grigliaPosizionaNavi.RowDefinitions.Add(riga);

			ColumnDefinition colonna = new ColumnDefinition();
			grigliaPosizionaNavi.ColumnDefinitions.Add(colonna);
		}

        btnSu.CommandParameter = new Point(-1, 0);
        btnGiu.CommandParameter = new Point(1, 0);
        btnSinistra.CommandParameter = new Point(0, -1);
        btnDestra.CommandParameter = new Point(0, 1);

        var tap = new TapGestureRecognizer
        {
            // CommandParameter = new int[] { Grid.GetRow() } // puoi usare anche oggetti custom
        };

    }
}