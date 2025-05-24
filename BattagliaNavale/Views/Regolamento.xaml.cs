using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace BattagliaNavale.Views;

public partial class Regolamento : ContentPage
{
	public Regolamento()
	{
		InitializeComponent();
    }

    [RelayCommand]
    public async Task OpenHome()
    {
        await Navigation.PopAsync();
    }

}