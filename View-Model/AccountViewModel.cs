using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cash_Widget.View_Model
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        //private fields

        //Public Properites
        public Account MyAccount;
        public double MyBalance
        {
            get
            {
                return MyAccount.Balance;
            }
        }
        public double MyDebitAmount
        {
            get
            {
                return MyAccount.DebitAmount;
            }
            set
            {
                MyAccount.DebitAmount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MyDebitAmount"));
            }
        }
        public double MyCreditAmount
        {
            get
            {
                return MyAccount.CreditAmount;
            }
            set
            {
                MyAccount.CreditAmount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MyCreditAmount"));
            }
        }
        public string MyName
        {
            get => MyAccount.Name;

            set
            {
                MyAccount.Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MyName"));
            }
        }
    }
}
