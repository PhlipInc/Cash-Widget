using System;
using System.Windows;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cash_Widget.View_Model;

namespace Cash_Widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel avm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            avm.InitializeDatabase();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayCorrectCanvas();
        }

        private void RegisterAccountButton(object sender, RoutedEventArgs e)
        {
            try
            {
                Register();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            try
            {
                Login();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DisplayCorrectCanvas()
        {
            //Sets the correct canvas corresponding to if the user needs to register for an account or login to an account
            if (avm.IsRegistered == true)
            {
                if (avm.IsPasswordProtected == true)
                {
                    LoginCanvas.Visibility = Visibility.Visible;
                    RegistrationCanvas.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Dashboard d = new Dashboard();
                    d.Show();
                }
            }
            else
            {
                LoginCanvas.Visibility = Visibility.Collapsed;
                RegistrationCanvas.Visibility = Visibility.Visible;
            }
        }

        public void Login()
        {
            if (avm.IsPasscodeCorrect(LoginPasswordBox.Password) == true)
            {
                Dashboard w = new Dashboard();
                w.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please try again!");
            }
        }

        public void Register()
        {
            LoginCanvas.Visibility = Visibility.Visible;
            RegistrationCanvas.Visibility = Visibility.Collapsed;
            avm.NewAccount(RegisterPasscodeBox.Password);
        }
    }
}
