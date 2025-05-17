using CommunityToolkit.Mvvm.Input;
using BattagliaNavale.ViewModels;
using Microsoft.Maui.Controls.Shapes;

namespace BattagliaNavale.Views;

public partial class PosizionaNavi : ContentPage
{
	const int LUNGHEZZA = 10;
	public PosizionaNavi()
	{
		InitializeComponent();

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            grigliaPosizionaNavi.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grigliaPosizionaNavi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < LUNGHEZZA; i++)
		{
			for (int j = 0; j < LUNGHEZZA; j++)
            {
                Microsoft.Maui.Controls.Shapes.Rectangle cella = new Microsoft.Maui.Controls.Shapes.Rectangle();
                Grid.SetColumn(cella, j);
                Grid.SetRow(cella, i);
                cella.Fill = new SolidColorBrush(Colors.LightBlue);
                cella.Stroke = new SolidColorBrush(Colors.CadetBlue);
                cella.StrokeThickness = 1;
                grigliaPosizionaNavi.Children.Add(cella);
            }
        }

        btnSu.CommandParameter = new Point(-1, 0);
        btnGiu.CommandParameter = new Point(1, 0);
        btnSinistra.CommandParameter = new Point(0, -1);
        btnDestra.CommandParameter = new Point(0, 1);

        BindingContext = new ViewModels.PosizionaNaviViewModel(this.Navigation);
    }
}