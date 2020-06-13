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
    public class PremiumAccountTests
    {

        [Test]
        [TestCase("55555", "PremiumAccount", 100, AccountType.Free, 250, false)]
        [TestCase("55555", "PremiumAccount", 100, AccountType.Premium, -100, false)]
        [TestCase("55555", "PremiumAcount", 100, AccountType.Premium, 250, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
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
        [TestCase("55555", "PremiumAccount", 1500, AccountType.Premium, -1000, 1500, false)]
        [TestCase("55555", "PremiumAccount", 100, AccountType.Free, -100, 100, false)]
        [TestCase("55555", "PremiumAccount", 100, AccountType.Premium, 100, 100, false)]
        [TestCase("55555", "PremiumAccount", 150, AccountType.Premium, -50, 100, true)]
        [TestCase("55555", "PremiumAccount", 100, AccountType.Premium, -610, -520, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
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

