using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class StorePage : Page
    {
        private readonly ObservableCollection<PowerUp> _powerUps;
        public StorePage()
        {
            this.InitializeComponent();
            if (GamePage.Player.Team is TeamBlue)
            {
                MyGrid.Background = new SolidColorBrush(Colors.Aqua);

                Application.Current.Resources["SystemControlHighlightListAccentLowBrush"] = new SolidColorBrush(Colors.Aqua);
                Application.Current.Resources["SystemControlHighlightListAccentMediumBrush"] = new SolidColorBrush(Colors.Aqua);
            }
            if (GamePage.Player.Team is TeamRed)
            {
                MyGrid.Background = new SolidColorBrush(Colors.Coral);

                Application.Current.Resources["SystemControlHighlightListAccentLowBrush"] = new SolidColorBrush(Colors.Coral);
                Application.Current.Resources["SystemControlHighlightListAccentMediumBrush"] = new SolidColorBrush(Colors.Coral);
            }

            if (GamePage.Player.Team is TeamYellow)
            {
                MyGrid.Background = new SolidColorBrush(Colors.Gold);

                Application.Current.Resources["SystemControlHighlightListAccentLowBrush"] = new SolidColorBrush(Colors.Gold);
                Application.Current.Resources["SystemControlHighlightListAccentMediumBrush"] = new SolidColorBrush(Colors.Gold);
            }

            _powerUps = GameLogic.PowerUps;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var obj = (ListView)sender;
            var thingy = (PowerUp)obj.SelectedItem;

            if (thingy == null) return;

            Frame.Navigate(typeof(PowerUpDetailPage), thingy);
        }
    }
}
