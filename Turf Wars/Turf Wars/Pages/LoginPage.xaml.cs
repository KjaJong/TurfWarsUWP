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
            if(GameLogic.Players.Count == 0) GameLogic.Players.Add(new Player("g","g","g"));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;

            if (obj.Content != null && obj.Content.ToString() == null) return;

            switch (obj.Content.ToString().ToLower())
            {
                case "login":
                    foreach (var p in GameLogic.Players)
                    {
                        if (p.CheckLogin(UsernameBlock.Text, PasswordBlock.Password))
                        {
                            if (p.Team is NoTeam) Frame.Navigate(typeof(TeamChoserPage), p);
                            else Frame.Navigate(typeof(GamePage), p);
                        }
                        else Failed.Visibility = Visibility.Visible;
                    }
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
