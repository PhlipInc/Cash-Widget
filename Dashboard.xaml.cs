using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cash_Widget
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        //private readonly AccountDBContext aContext = new AccountDBContext();
        /*private AccountDBContext.Account acc = new AccountDBContext.Account();
        private AccountDBContext.Transaction trans = new AccountDBContext.Transaction();*/
        View_Model.MainViewModel avm = new View_Model.MainViewModel();
        DateTime dt = DateTime.UtcNow;

        public Dashboard()
        {
            InitializeComponent();
            DataContext = avm;
            avm.OnDashboardLoad();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //aContext.GetTransactionsInRange();
            /*trans = new AccountDBContext.Transaction();
            trans.Name = "Proof";
            trans.Category = "Lorem";
            trans.Note = "Ipsum for the win";
            trans.PaymentType = "Debit";
            trans.TransactionDate = dt;
            trans.Amount = 1000;
            aContext.AddTransactions(acc, trans, "fart");
            trans = new AccountDBContext.Transaction();
            trans.Name = "Proof";
            trans.Category = "Lorem";
            trans.Note = "Ipsum for the win";
            trans.PaymentType = "Debit";
            trans.TransactionDate = dt;
            trans.Amount = 1000;
            aContext.AddTransactions(acc, trans, "fart");
            trans = new AccountDBContext.Transaction();
            trans.Name = "Proof";
            trans.Category = "Lorem";
            trans.Note = "Ipsum for the win";
            trans.PaymentType = "Debit";
            trans.TransactionDate = dt;
            trans.Amount = 1000;
            aContext.AddTransactions(acc, trans, "fart");
            trans = new AccountDBContext.Transaction();
            trans.Name = "Proof";
            trans.Category = "Lorem";
            trans.Note = "Ipsum for the win";
            trans.PaymentType = "Debit";
            trans.TransactionDate = dti.SelectedDate.Value;
            trans.Amount = 1000;
            aContext.AddTransactions(acc, trans, "fart");
            trans = new AccountDBContext.Transaction();
            trans.Name = "Proof";
            trans.Category = "Lorem";
            trans.Note = "Ipsum for the win";
            trans.PaymentType = "Debit";
            trans.TransactionDate = dt;
            trans.Amount = 1000;
            aContext.AddTransactions(acc, trans, "fart");
            trans = new AccountDBContext.Transaction();
            trans.Name = "Proof";
            trans.Category = "Lorem";
            trans.Note = "Ipsum for the win";
            trans.PaymentType = "Debit";
            trans.TransactionDate = dt;
            trans.Amount = 1000;
            aContext.AddTransactions(acc, trans, "fart");*/

        }
    }
}
