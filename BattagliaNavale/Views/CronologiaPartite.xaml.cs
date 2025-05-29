using CommunityToolkit.Mvvm.Input;
using BattagliaNavale.ViewModels;

namespace BattagliaNavale.Views;

public partial class CronologiaPartite : ContentPage
{
	public CronologiaPartite()
	{
		InitializeComponent();
        BindingContext = new CronologiaPartiteViewModel();
    }

    [RelayCommand]
    public async Task OpenPartita(int id)
    {
        await Navigation.PushAsync(new VisualizzatoreCampo(id));
    }

    [RelayCommand]
    public async Task Esci()
    {
        await Navigation.PopAsync();
    }
}