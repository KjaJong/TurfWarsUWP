using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Turf_Wars.DataWriting;
using Turf_Wars.Teams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turf_Wars.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private readonly ContentDialog _youSure;
        public SettingsPage()
        {
            this.InitializeComponent();
            _youSure = new ContentDialog();
            _youSure.Visibility = Visibility.Collapsed;

            if (GamePage.Player.Team is TeamBlue) MyGrid.Background = new SolidColorBrush(Colors.Aqua);
            if (GamePage.Player.Team is TeamRed) MyGrid.Background = new SolidColorBrush(Colors.Coral);
            if (GamePage.Player.Team is TeamYellow) MyGrid.Background = new SolidColorBrush(Colors.Gold);
        }

        private async void LogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            _youSure.Title = "Logout";
            _youSure.Content = "You sure you want to logout?";
            _youSure.IsPrimaryButtonEnabled = true;
            _youSure.IsSecondaryButtonEnabled = true;
            _youSure.SecondaryButtonText = "No";
            _youSure.PrimaryButtonText = "Yes";
            _youSure.Visibility = Visibility.Visible;

            var result = await _youSure.ShowAsync();
            if (result == ContentDialogResult.Secondary) return;
            
            var window = Window.Current.Content as Frame;
            if (window == null) return;

            await SaveLoadUtil.SavePlayerNames(GamePage.Player);
            GameLogic.ResetPowerUps();

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["FirstTime"] = true;

            window.Navigate(typeof(LoginPage));
        }

        private async void ChangeTeam_OnClick(object sender, RoutedEventArgs e)
        {
            _youSure.Title = "Logout";
            _youSure.Content = "You sure you want to change teams? It costs 1000 coinz";
            _youSure.IsPrimaryButtonEnabled = true;
            _youSure.IsSecondaryButtonEnabled = true;
            _youSure.SecondaryButtonText = "No";
            _youSure.PrimaryButtonText = "Yes";
            _youSure.Visibility = Visibility.Visible;

            var result = await _youSure.ShowAsync();
            if (result == ContentDialogResult.Secondary) return;

            var window = Window.Current.Content as Frame;
            if (window == null) return;

            if (GamePage.Player.Coinz < 1000)
            {
                NotEnoughMunz.Visibility = Visibility.Visible;
                return;
            }
            GamePage.Player.Coinz -= 1000;
            window.Navigate(typeof(TeamChoserPage), GamePage.Player);
        }
    }
}
