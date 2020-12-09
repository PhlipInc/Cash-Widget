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
        private readonly AccountDBContext aContext = new AccountDBContext();
        private readonly AccountDBContext.Account acc = new AccountDBContext.Account();
        private readonly AccountDBContext.Transaction trans = new AccountDBContext.Transaction();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists($"{Environment.ProcessorCount}.db"))
            {
                if (aContext.GetAccountByName(Environment.UserName).PasswordProtection == true)
                {
                    RegistrationCanvas.Visibility = Visibility.Collapsed;
                    LoginCanvas.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show(acc.PasswordProtection.ToString());
                    Window w = new Window();
                    w.Show();
                }
            }
            else
            {
                LoginCanvas.Visibility = Visibility.Collapsed;
                RegistrationCanvas.Visibility = Visibility.Visible;
                await aContext.Database.EnsureCreatedAsync().ConfigureAwait(true);
            }
        }

        private void RegisterAccountButton(object sender, RoutedEventArgs e)
        {
            try
            {
                TestAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            if (aContext.Checkpasscode(Convert.ToInt32(LoginPasswordBox.Password), Environment.UserName) == true)
            {
                Dashboard w = new Dashboard();
                w.Show();
            }
            else
            {
                MessageBox.Show("Please try again!");
            }
        }

        private void NewAccount()
        {
            acc.Name = Environment.UserName;
            acc.Passcode = Convert.ToInt32(RegisterPasscodeBox.Password);
            acc.DebitAmount = 0;
            acc.CreditAmount = 0;
            acc.PasswordProtection = true;
            aContext.InsertAccount(acc);
            RegistrationCanvas.Visibility = Visibility.Collapsed;
            LoginCanvas.Visibility = Visibility.Visible;
        }

        private void TestAccount()
        {
            acc.Name = Environment.UserName;
            acc.Passcode = Convert.ToInt32(RegisterPasscodeBox.Password);
            acc.DebitAmount = 1000;
            acc.CreditAmount = 54000204;
            acc.PasswordProtection = true;
            trans.Name = "Pizza";
            trans.Category = "Food";
            trans.Note = "Pizza for delivery";
            trans.PaymentType = "Debit";
            DateTime dt = DateTime.UtcNow;
            trans.TransactionDate = dt;
            trans.Amount = 1000;
            acc.Transactions.Add(trans);
            aContext.InsertAccount(acc);
            RegistrationCanvas.Visibility = Visibility.Collapsed;
            LoginCanvas.Visibility = Visibility.Visible;
        }
    }
}
