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
using Turf_Wars.Teams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turf_Wars.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private bool _isLogOut;
        public SettingsPage()
        {
            this.InitializeComponent();
            if (GamePage.Player.Team is TeamBlue) MyGrid.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
            if (GamePage.Player.Team is TeamRed) MyGrid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 127, 80));
            if (GamePage.Player.Team is TeamYellow) MyGrid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 215, 0));
        }

        private void LogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            _isLogOut = true;
            YouSureBlock.Text = "You sure?";
            ChangeTeamPop.IsOpen = true;
        }

        private void ChangeTeam_OnClick(object sender, RoutedEventArgs e)
        {
            _isLogOut = false;
            YouSureBlock.Text = "Changing teams will cost 1000 coins";
            ChangeTeamPop.IsOpen = true;
        }

        private void NONONONONONO_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeTeamPop.IsOpen = false;
        }

        private void YESYESYESYESYES_OnClick(object sender, RoutedEventArgs e)
        {
            var window = Window.Current.Content as Frame;
            if (window == null) return;

            if (_isLogOut)
            {
                window.Navigate(typeof(LoginPage));
            }
            else
            {
                if (GamePage.Player.Coinz < 1000)
                {
                    NotEnoughMunz.Visibility = Visibility.Visible;
                    ChangeTeamPop.IsOpen = false;
                    return;
                }
                GamePage.Player.Coinz -= 1000;
                window.Navigate(typeof(TeamChoserPage), GamePage.Player);
            }
        }
    }
}
