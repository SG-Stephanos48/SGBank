using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {

        private static Account _account = new Account
        {

            Name = "Premium Account",
            Balance = 100M,
            AccountNumber = "55555",
            Type = AccountType.Premium

        };

        public Account LoadAccount(string AccountNumber)
        {
            //Task 1.1 - 1.2, Finish Free Account Testing
            if (AccountNumber == _account.AccountNumber)
            {
                return _account;
            }
            else
            {
                return null;
            }
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }

    }
}
