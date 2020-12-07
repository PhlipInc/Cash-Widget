using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Cash_Widget
{
    public class AccountDBContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Environment.ProcessorCount}.db");
        }

        public IEnumerable<Account> GetAccounts()
        {
            return from accounts
                   in Account
                   orderby accounts.accountID
                   select accounts;
        }

        public Account GetAccountByName(string name)
        {
            return (from accounts
                   in GetAccounts()
                   where accounts.name == name 
                   select accounts).FirstOrDefault();
        }

        public bool Checkpasscode(string inputPasscode)
        {
            string pass = GetAccountByName("").passcode;

            bool check = pass.Equals(inputPasscode);
            return check;
        }

        public async void InsertAccount(Account account)
        {
            Account.Add(account);
            await SaveChangesAsync();
        }
    }

    [Table("cash_accounts")]
    public class Account
    {
        [Column("Id")]
        public int accountID { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("passcode")]
        public string passcode { get; set; }
    }

    [Table("cash_transactions")]
    public class Transactions
    {

    }
}
