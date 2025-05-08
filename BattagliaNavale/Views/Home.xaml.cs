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
}