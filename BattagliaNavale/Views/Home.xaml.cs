using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;

namespace BattagliaNavale.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

	[RelayCommand]
	public async Task OpenRegolamento()
	{
		await Navigation.PushAsync(new Regolamento());
	}

    [RelayCommand]
    public async Task OpenGioco()
    {
        await Navigation.PushAsync(new PosizionaNavi());
    }

    [RelayCommand]
    public async Task Esci()
    {
        await Navigation.PushAsync(new PosizionaNavi());
    }
}