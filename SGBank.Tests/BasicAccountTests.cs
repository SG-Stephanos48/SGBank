using NUnit.Framework;
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
    public class BasicAccountTests
    {
        [Test]
        [TestCase("33333", "BasicAccount", 100, AccountType.Free, 250, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, -100, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, 250, true)]
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {

            IDeposit ruke = new NoLimitDepositRule();
            Account juke = new Account();

            juke.AccountNumber = accountNumber;
            juke.Name = name;
            juke.Balance = balance;
            juke.Type = accountType;

            AccountDepositResponse adr = ruke.Deposit(juke, amount);

            Assert.AreEqual(expectedResult, adr.Success);

        }

        [Test]
        [TestCase("33333", "BasicAccount", 1500, AccountType.Basic, -1000, 1500, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Free, -100, 100, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, 100, 100, false)]
        [TestCase("33333", "BasicAccount", 150, AccountType.Basic, -50, 100, true)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, -150, -60, true)]

        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {

            IWithdraw ruke = new BasicAccountWithdrawRule();
            Account juke = new Account();

            juke.AccountNumber = accountNumber;
            juke.Name = name;
            juke.Balance = balance;
            juke.Type = accountType;

            AccountWithdrawResponse adr = ruke.Withdraw(juke, amount);

            Assert.AreEqual(expectedResult, adr.Success);
            if (adr.Success == true)
            {
                Assert.AreEqual(newBalance, balance);
            };
            
        }
    }
}
