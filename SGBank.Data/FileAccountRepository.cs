using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {

        //declare file stuff
        private string _filePath;

        private static Account _account = new Account
        {
            Name = "FileTest",
            Balance = 100.00M,
            AccountNumber = "77777",
            Type = AccountType.Basic
        };

        public FileAccountRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Account LoadAccount(string AccountNumber)
        {

            //check if file exists, else return error
            //if file exists, open file read line by line return specific account, else return error

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
                return LookUpAccount(AccountNumber);
                
            }
            else
            {
                return LookUpAccount(AccountNumber);
            }

        }

        public Account LookUpAccount(string AccountNumber)
        {

            using (StreamReader sr = new StreamReader(_filePath))
            {
                sr.ReadLine();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Account newAccount = new Account();

                    string[] columns = line.Split(',');

                    newAccount.Name = columns[0];
                    newAccount.Balance = decimal.Parse(columns[1]);
                    newAccount.AccountNumber = columns[2];
                    //newAccount.Type = columns[4];
                    newAccount.Type = (AccountType)Enum.Parse(typeof(AccountType), columns[3]);

                    if (newAccount.AccountNumber == AccountNumber)
                    {
                        return newAccount;
                    }
                }
                Console.WriteLine("It appears the Account you are looking for does not exist!");
                return null;
            }

        }

        public void SaveAccount(Account account)
        {
            //check if file exists, open in Append Mode and write one line
            //else create a new file, add a header line, then add account line

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
                AccountAddHeaders();
                AccountAdd(account);
            }
            else
            {
                AccountUpdate(account);
            }
        }

        public void AccountUpdate(Account account)
        {
            //find account line, replace account data with new data from deposit/withdrawal
            using (StreamWriter sw = new StreamWriter(_filePath, true))
            {

                string line = string.Format("{0},{1},{2},{3}", account.Name, account.Balance,
                    account.AccountNumber, account.Type);

                sw.WriteLine(line);

            }

        }

        public void AccountAdd(Account account)
        {

            using (StreamWriter sw = File.AppendText(_filePath))
            {

                Account newAccount = new Account();

                sw.WriteLine(newAccount.Name,newAccount.Balance,newAccount.AccountNumber,newAccount.Type);

            };

        }
        
        public void AccountAddHeaders()
        {
            using (StreamWriter sw = new StreamWriter(_filePath, true))
            {

                string line = string.Format("{0},{1},{2},{3}", "Name", "Balance",
                    "AccountNumber", "Type");

                sw.WriteLine(line);
            }
        }
    }
}
