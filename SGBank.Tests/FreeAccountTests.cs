using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {

        [Test]
        public void CanLoadFreeAccountTestData()
        {

            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345"); // Task 1.3 - 1.4 - Finish Free Account Testing

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);

        }

        [Test]
        [TestCase("12345","FreeAccount",100,AccountType.Free,250,false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free,-100,false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Basic,50,false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free,50,true)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {

            IDeposit ruke = new FreeAccountDepositRule();
            Account juke = new Account();

            juke.AccountNumber = accountNumber;
            juke.Name = name;
            juke.Balance = balance;
            juke.Type = accountType;

            AccountDepositResponse adr = ruke.Deposit(juke, amount);

            Assert.AreEqual(expectedResult, adr.Success);

        }

        [Test]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 250, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -150, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Basic, 50, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -110, false)] 
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -50, true)]
        public void FreeAccountWithdrawalRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {

            IWithdraw ruke = new FreeAccountWithdrawRule();
            Account juke = new Account();

            juke.AccountNumber = accountNumber;
            juke.Name = name;
            juke.Balance = balance;
            juke.Type = accountType;

            AccountWithdrawResponse adr = ruke.Withdraw(juke, amount);

            Assert.AreEqual(expectedResult, adr.Success);

        }
    }
}
