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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turf_Wars.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            switch (button.Name.ToLower())
            {
                case "signinbutton":
                    SignInCheck();
                    break;
                case "backbutton":
                    if(!Frame.CanGoBack) return;
                    Frame.GoBack();
                    break;
                default:
                    Debug.WriteLine(button.Name);
                    Debug.WriteLine("You're not suppose to be in here");
                    break;
            }
            
        }

        private void SignInCheck()
        {
            if (UsernameBox.Text == "" || PasswordBox.Password == "" || RePasswordBox.Password == "" ||
                EmailBox.Text == "")
            {
                IncorrectBlock.Visibility = Visibility.Collapsed;
                ForgetBlock.Visibility = Visibility.Visible;

                PasswordBox.Password = "";
                RePasswordBox.Password = "";
                return;
            }

            if (PasswordBox.Password != RePasswordBox.Password)
            {
                IncorrectBlock.Visibility = Visibility.Visible;
                ForgetBlock.Visibility = Visibility.Collapsed;

                PasswordBox.Password = "";
                RePasswordBox.Password = "";
                return;
            }

            GameLogic.Players.Add(new Player(UsernameBox.Text, PasswordBox.Password, EmailBox.Text));
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
