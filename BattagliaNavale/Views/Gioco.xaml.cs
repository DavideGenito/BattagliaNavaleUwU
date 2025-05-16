using BattagliaNavale.Models;
using BattagliaNavale.ViewModels;

namespace BattagliaNavale.Views;

public partial class Gioco : ContentPage
{
    const int LUNGHEZZA = 10;
    public Gioco(StatoCampo[,] campoLogico)
	{
		InitializeComponent();

        BindingContext = new ViewModels.GiocoViewModel(campoLogico);

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            grigliaBot.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grigliaBot.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grigliaGiocatore.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grigliaGiocatore.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            for (int j = 0; j < LUNGHEZZA; j++)
            {
                Button bottone = new Button();
                Grid.SetColumn(bottone, j);
                Grid.SetRow(bottone, i);
                bottone.Fill = new SolidColorBrush(Colors.LightBlue);
                bottone.Stroke = new SolidColorBrush(Colors.Black);
                bottone.StrokeThickness = 1;
                grigliaBot.Children.Add(bottone);

                Microsoft.Maui.Controls.Shapes.Rectangle cella = new Microsoft.Maui.Controls.Shapes.Rectangle();
                Grid.SetColumn(cella, j);
                Grid.SetRow(cella, i);
                cella.Fill = new SolidColorBrush(Colors.LightBlue);
                cella.Stroke = new SolidColorBrush(Colors.Black);
                cella.StrokeThickness = 1;
                grigliaGiocatore.Children.Add(cella);
            }
        }
    }
}