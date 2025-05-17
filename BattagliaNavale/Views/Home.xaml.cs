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
    private IAudioPlayer? _player;

    private async void PlayBackgroundMusic()
    {
        if (_player == null)
        {
            var file = await FileSystem.OpenAppPackageFileAsync("jack_sparrow.mp3");
            _player = _audioManager.CreatePlayer(file);
            _player.Loop = true;
        }

        _player.Play();
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
}