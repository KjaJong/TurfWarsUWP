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
using Windows.Web.Syndication;
using Turf_Wars.DataWriting;
using Turf_Wars.Teams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turf_Wars.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;

            if (obj.Content == null) return;

            switch (obj.Content.ToString().ToLower())
            {
                case "login":
                    var player = await SaveLoadUtil.LoadPlayerNames(UsernameBlock.Text);
                    if (player == null)
                    {
                        Failed.Visibility = Visibility.Visible;
                        return;
                    }
                    
                    if (player.CheckLogin(UsernameBlock.Text, PasswordBlock.Password))
                    {
                        if (player.Team is NoTeam) Frame.Navigate(typeof(TeamChoserPage), player);
                        else
                        {
                            GamePage.Player = player;
                            Frame.Navigate(typeof(GamePage));
                        }
                    }
                    Failed.Visibility = Visibility.Visible;
                    break;
                case "sign up":
                    Frame.Navigate(typeof(SignUpPage));
                    break;
                default:
                    Debug.WriteLine(obj.Content.ToString());
                    Debug.WriteLine("You're not suppose to be in here");
                    break;
            }
        }
    }
}
