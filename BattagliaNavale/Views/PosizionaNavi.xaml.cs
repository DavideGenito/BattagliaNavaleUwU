using CommunityToolkit.Mvvm.Input;

namespace BattagliaNavale.Views;

public partial class PosizionaNavi : ContentPage
{
	const int LUNGHEZZA = 10;
	public PosizionaNavi()
	{
		InitializeComponent();
		for(int i = 0;  i < LUNGHEZZA; i++)
		{
			RowDefinition riga = new RowDefinition();
			grigliaPosizionaNavi.RowDefinitions.Add(riga);

			ColumnDefinition colonna = new ColumnDefinition();
			grigliaPosizionaNavi.ColumnDefinitions.Add(colonna);
		}
	}

    [RelayCommand]
    public async Task IniziaGioco()
    {
        await Navigation.PushAsync(new Gioco());
    }
}