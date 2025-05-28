using CommunityToolkit.Mvvm.Input;

namespace BattagliaNavale.Views;

public partial class CronologiaPartite : ContentPage
{
	public CronologiaPartite()
	{
		InitializeComponent();
	}

    [RelayCommand]
    public async Task OpenPartita()
    {
        await Navigation.PushAsync(new ());
    }
}