using CommunityToolkit.Mvvm.Input;
using BattagliaNavale.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using Plugin.Maui.Audio;
using System.Runtime.CompilerServices;
namespace BattagliaNavale.Views;

public partial class PosizionaNavi : ContentPage
{
    const int LUNGHEZZA = 10;
    public PosizionaNavi()
    {
        InitializeComponent();
        _audioManager = AudioManager.Current;
        PlayBackgroundMusic();

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            grigliaPosizionaNavi.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grigliaPosizionaNavi.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            for (int j = 0; j < LUNGHEZZA; j++)
            {
                Microsoft.Maui.Controls.Shapes.Rectangle cella = new Microsoft.Maui.Controls.Shapes.Rectangle();
                Grid.SetColumn(cella, j);
                Grid.SetRow(cella, i);
                cella.Fill = new SolidColorBrush(Colors.LightBlue);
                cella.Stroke = new SolidColorBrush(Colors.CadetBlue);
                cella.StrokeThickness = 1;
                grigliaPosizionaNavi.Children.Add(cella);
            }
        }

        btnSu.CommandParameter = new Point(-1, 0);
        btnGiu.CommandParameter = new Point(1, 0);
        btnSinistra.CommandParameter = new Point(0, -1);
        btnDestra.CommandParameter = new Point(0, 1);

        BindingContext = new ViewModels.PosizionaNaviViewModel(this.Navigation);
        
    }
    private readonly IAudioManager _audioManager;
    public  IAudioPlayer? Player1;

    private async void PlayBackgroundMusic()
    {


        if (Player1 == null)
        {
            var file = await FileSystem.OpenAppPackageFileAsync("jack-sparrow_DVwcYAwc.mp3");
            Player1 = _audioManager.CreatePlayer(file);
            Player1.Loop = true;
        }

        Player1.Play();
    }

    [RelayCommand]
    public async Task Gioco()
    {
        PosizionaNavi _player1 = new PosizionaNavi();
        _player1.Player1.Stop();
        await Navigation.PushAsync(new PosizionaNavi());
    }
}