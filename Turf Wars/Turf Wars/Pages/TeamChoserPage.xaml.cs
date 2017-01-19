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
    public sealed partial class TeamChoserPage : Page
    {
        private Player _player;
        public TeamChoserPage()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            switch (button.Name.ToLower())
            {
                case "red":
                    _player.Team = new TeamRed();
                    break;
                case "blueton":
                    _player.Team = new TeamBlue();
                    break;
                case "yellowbutton":
                    _player.Team = new TeamYellow();
                    break;
            }

            GamePage.Player = _player;
            Frame.Navigate(typeof(GamePage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var player = e.Parameter as Player;

            if (player == null) return;
            _player = player;
        }
    }
}
