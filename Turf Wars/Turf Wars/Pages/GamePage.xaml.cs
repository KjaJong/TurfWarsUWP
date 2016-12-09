using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Turf_Wars.Teams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turf_Wars.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private Player _player;
        public GamePage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(MapPage));
        }

        private void Hamburger_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            switch (button.Name.ToLower())
            {
                case "backbutton":
                    if(!Frame.CanGoBack) return;
                    Frame.GoBack();
                    break;
                case "hamburger":
                    MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
                    break;
                default:
                    Debug.WriteLine(button.Name);
                    Debug.WriteLine("You're not suppose to be in here.");
                    break;
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_player.Team is NoTeam) Frame.Navigate(typeof(TeamChoserPage), _player);

            if (Home.IsSelected)
            {
                MyFrame.Navigate(typeof(MapPage));
            }

            if (Store.IsSelected)
            {
                MyFrame.Navigate(typeof(StorePage));
            }

            if (Bag.IsSelected)
            {
                MyFrame.Navigate(typeof(InventoryPage));
            }

            if (Settings.IsSelected)
            {
                MyFrame.Navigate(typeof(SettingsPage));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var player = e.Parameter as Player;

            if (player == null) return;

            WelcomeBlock.Text = $"Welcome {player.Name}";
            _player = player;
        }
    }
}
