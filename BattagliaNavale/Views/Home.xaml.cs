using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Runtime.CompilerServices;

namespace BattagliaNavale.Views;

public partial class Home : ContentPage
{
    public Home()
	{
		InitializeComponent();

        _audioManager = AudioManager.Current;
        PlayBackgroundMusic();
    }

    private readonly IAudioManager _audioManager;
    public IAudioPlayer? Player { get; private set; }

    private async void PlayBackgroundMusic()
    {
        

        if (Player == null)
        {
            var file = await FileSystem.OpenAppPackageFileAsync("The Legend Of Monkey Island (Main Theme) - Sea Of Thieves Soundtrack.mp3");
            Player = _audioManager.CreatePlayer(file);
            Player.Loop = true;
        }

        Player.Play();
    }

    [RelayCommand]
	public async Task OpenRegolamento()
	{
		await Navigation.PushAsync(new Regolamento());
	}

    [RelayCommand]
    public async Task OpenGioco()
    {
        Player.Stop();
        await Navigation.PushAsync(new PosizionaNavi());
    }

    private void EsciClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
        

    }
}