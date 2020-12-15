using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Cash_Widget
{
    public class AccountDBContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Environment.ProcessorCount}.db");
        }

        public IEnumerable<Account> GetAccounts()
        {
            return Accounts.OrderBy(a => a.AccountID);
        }

        public IEnumerable<Transaction> GetTransactionsInRange(string name, DateTime first, DateTime last)
        {
            return GetAccountByName(name).Transactions.Where(a => a.TransactionDate >= first && a.TransactionDate <= last);
            //return GetAccountByName(name).SelectMany(a => a.Transactions.Where(b => b.TransactionDate >= DateTime.Now && b.TransactionDate <= DateTime.Now));
        }

        public Account GetAccountByName(string name)
        {
            try
            {
                return Accounts.FirstOrDefault(filterByName);

                bool filterByName(Account account)
                {
                    return account.Name == name;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Checkpasscode(int inputPasscode, string name)
        {
            bool check = GetAccountByName(name).Passcode.Equals(inputPasscode);
            return check;
        }

        public async void InsertAccount(Account account)
        {
            Accounts.Add(account);
            await SaveChangesAsync().ConfigureAwait(true);
        }
        public async void UpdateAccount(Account account)
        {
            Accounts.Update(account);
            await SaveChangesAsync().ConfigureAwait(true);
        }
        public async void AddTransactions(Account account, Transaction transaction, string name)
        {
            try
            {
                GetAccountByName(name).Transactions.Add(transaction);
                Accounts.UpdateRange(GetAccountByName(name));
                await SaveChangesAsync().ConfigureAwait(true);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    [Table("accounts")]
    public class Account
    {
        [Column("Id")]
        public int AccountID { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Passcode")]
        public int Passcode { get; set; }
        [Column("DebitAmount")]
        public double DebitAmount { get; set; }
        [Column("CreditAmount")]
        public double CreditAmount { get; set; }
        [Column("PasswordProtection", TypeName = "BOOL")]
        public bool PasswordProtection { get; set; }
        [Column("Transactions")]
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public double Balance
        {
            get => DebitAmount + CreditAmount;
        }

        public void NewAaccount(string passcode)
        {
            Name = Environment.UserName;
            Passcode = Convert.ToInt32(passcode);
            DebitAmount = 0;
            CreditAmount = 0;
            PasswordProtection = true;
        }
    }
    [Table("transactions")]
    public class Transaction
    {
        [Column("Id", TypeName = "INTEGER")]
        public int TransactionID { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Note")]
        public string Note { get; set; }
        [Column("TransactionDate")]
        public DateTime TransactionDate { get; set; }
        [Column("Category")]
        public string Category { get; set; }
        [Column("PaymentType")]
        public string PaymentType { get; set; }
        [Column("Amount")]
        public int Amount { get; set; }
    }
}
