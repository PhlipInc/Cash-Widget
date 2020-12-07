using System;
using System.Windows;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cash_Widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountDBContext aContext = new AccountDBContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists($"{Environment.ProcessorCount}.db"))
            {
                RegistrationCanvas.Visibility = Visibility.Collapsed;
                LoginCanvas.Visibility = Visibility.Visible;
            }

            else
            {
                LoginCanvas.Visibility = Visibility.Collapsed;
                RegistrationCanvas.Visibility = Visibility.Visible;
                aContext.Database.EnsureCreatedAsync();
            }
        }

        private void RegisterAccountButton(object sender, RoutedEventArgs e)
        {
            Account acc = new Account();
            acc.name = "POoper";
            acc.passcode = RegisterPasscodeBox.Password;
            aContext.InsertAccount(acc);
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            if (aContext.Checkpasscode(LoginPasswordBox.Password) == true)
            {
                Window w = new Window();
                w.Show();
            }
            else
            {
                MessageBox.Show("Please tru agaomfuck me in the ass hole jesus christ");
            }
        }
    }
}
