using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cash_Widget.View_Model
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AccountDBContext aContext = new AccountDBContext();
        private readonly Account acc = new Account();
        private readonly Transaction trans = new Transaction();

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
        public void PropertyChange(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string _myText;
        public string MyText
        {
            get
            {
                return _myText;
            }
            set
            {
                _myText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MyText"));
            }
        }



        //private data fields
        private bool _IsRegistered;
        private bool _IsPasswordProtected;
        private string _MyName;
        private ObservableCollection<AccountViewModel> _Accounts;
        private AccountViewModel _Account;

        //public properties
        public ObservableCollection<AccountViewModel> Accounts
        {
            get
            {
                return _Accounts;
            }
            set
            {
                _Accounts = value;
                PropertyChange("MyAccount");
            }
        }
        public AccountViewModel Account
        {
            get => _Account;

            set
            {
                _Account = value;
                PropertyChange("Account");
            }
        }
        public string MyName
        {
            get => _MyName;
            set
            {
                _MyName = value;
                PropertyChange("MyName");
            }
        }
        public bool IsRegistered
        {
            get => _IsRegistered;

            set
            {
                _IsRegistered = value;
                PropertyChange("IsRegistered");
            }
        }
        public bool IsPasswordProtected
        {
            get => _IsPasswordProtected;

            set
            {
                _IsPasswordProtected = value;
                PropertyChange("IsPasswordProtected");
            }
        }


        //Loads the data into the ViewModel for the Dashboard View
        public void OnDashboardLoad()
        {
            Accounts = new ObservableCollection<AccountViewModel>()
            {
                new AccountViewModel
                {
                    MyAccount = aContext.GetAccountByName(Environment.UserName)
                }
            };
            MyName = Accounts[aContext.GetAccountByName(Environment.UserName).AccountID - 1].MyName;
        }

        //Returns the boolean check for the correct passcode
        public bool IsPasscodeCorrect(string password)
        {
            return aContext.Checkpasscode(Convert.ToInt32(password), Environment.UserName);
        }

        //Checks if the database is ensured or not, then lets the corresponding fields to know which canvas to display
        public async void InitializeDatabase()
        {
            if (File.Exists($"{Environment.ProcessorCount}.db") && aContext.GetAccountByName(Environment.UserName) != null)
            {
                if (aContext.GetAccountByName(Environment.UserName).PasswordProtection == true)
                {
                    IsRegistered = true;
                    IsPasswordProtected = true;
                }
                else
                {
                    IsRegistered = true;
                    IsPasswordProtected = false;
                }
            }
            else
            {
                await aContext.Database.EnsureCreatedAsync().ConfigureAwait(true);
                IsRegistered = false;
            }
        }

        //This won't be here for long for testing purposes to fill sample data
        public void NewAccount(string passcode)
        {
            acc.Name = Environment.UserName;
            acc.Passcode = Convert.ToInt32(passcode);
            acc.DebitAmount = 100;
            acc.CreditAmount = 500;
            acc.PasswordProtection = true;
            aContext.InsertAccount(acc);
            //aContext.Add(acc);
        }
        public void TestAccount(string passcode)
        {
            acc.Name = Environment.UserName;
            acc.Passcode = Convert.ToInt32(passcode);
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
        }
    }
}
