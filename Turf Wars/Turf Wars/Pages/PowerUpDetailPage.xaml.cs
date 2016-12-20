using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Turf_Wars.Powers;
using Turf_Wars.Teams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turf_Wars.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PowerUpDetailPage : Page
    {
        private PowerUp _powerUp;
        public PowerUpDetailPage()
        {
            this.InitializeComponent();

            if (GamePage.Player.Team is TeamBlue)
            {
                MyGrid.Background = new SolidColorBrush(Colors.Aqua);
                BackButton.Background = new SolidColorBrush(Colors.Aqua);
                BuyButton.Background = new SolidColorBrush(Colors.Aqua);

            }

            if (GamePage.Player.Team is TeamRed)
            {
                MyGrid.Background = new SolidColorBrush(Colors.Coral);
                BackButton.Background = new SolidColorBrush(Colors.Coral);
                BuyButton.Background = new SolidColorBrush(Colors.Coral);
            }

            if (GamePage.Player.Team is TeamYellow)
            {
                MyGrid.Background = new SolidColorBrush(Colors.Gold);
                BackButton.Background = new SolidColorBrush(Colors.Gold);
                BuyButton.Background = new SolidColorBrush(Colors.Gold);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var powerUp = e.Parameter as PowerUp;

            if (powerUp == null) return;

            _powerUp = powerUp;
            BuyButton.Content = _powerUp.IsBought ? "Bought" : "Buy";
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!Frame.CanGoBack) return;
            Frame.GoBack();
        }

        private void BuyButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (GamePage.Player.Coinz >= _powerUp.Cost && !_powerUp.IsBought && GamePage.Player.Level >= _powerUp.LevelRestriction)
            {
                GamePage.Player.Coinz -= _powerUp.Cost;
                GameLogic.PowerUps[(int)_powerUp.PowerUpType].Buy();

                GamePage.Player.Powers.Add(GameLogic.PowerUps[(int)_powerUp.PowerUpType]);
                GamePage.Player.Powers = new ObservableCollection<PowerUp>(GamePage.Player.Powers.OrderBy(x => x.PowerUpType));

                Frame.Navigate(typeof(StorePage));
            }
            else
            {
                NotEnoughMunz.Visibility = Visibility.Visible;
            }
        }
    }
}
