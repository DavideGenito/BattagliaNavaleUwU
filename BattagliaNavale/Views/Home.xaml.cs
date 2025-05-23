using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using BattagliaNavale.Services;
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

    private void EsciClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private void MusicaClicked(object sender, EventArgs e)
    {
        AudioPlayerService.Instance.Mute();

        if (AudioPlayerService.Instance.IsMuted)
        {
            btnMusica.Source = "../Resources/Images/musica_sbarrata.png";
        }
        else
        {
            btnMusica.Source = "../Resources/Images/musica.png";
        }
    }
}