using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;
using BankSystemAssignmentCSharp.Util;
using MySql.Data.MySqlClient;
using QuanLySinhVienVoiCSharp.Util;

namespace BankSystemAssignmentCSharp.Model
{
    public class TransactionModel : ITransactionModel
    {
        private AccountModel AccountModel = new AccountModel();

        public bool saveTransactionHistory(TransactionHistory transactionHistory)
        {
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                string querry =
                    $"insert into transactionhistory (ID,SenderAccountNumber,ReceiverAccountNumber,Type,Amount,Message,CreateAt,UpdateAt,DeleteAt,Status) " +
                    $"values ('{transactionHistory.ID}','{transactionHistory.SenderAccountNumber}','{transactionHistory.ReceiverAccountNumber}',{transactionHistory.Type}," +
                    $"{transactionHistory.Amount},'{transactionHistory.Message}',{transactionHistory.CreateAt},{transactionHistory.UpdateAt},{transactionHistory.DeleteAt},{transactionHistory.Status})";
                MySqlCommand mySqlCommand = new MySqlCommand(querry, cnn);
                var result = mySqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        } //done

        public List<TransactionHistory> FindTransactionHistoryByAccountNumber(string accountNumber, string startTime,
            string endTime, int offset, int limit)
        {
            List<TransactionHistory> transactionHistoriesList = new List<TransactionHistory>();
            var startTimeConverted = ConvertStringDateTimeToMilisecond.ToMiliSecond(startTime);
            var endTimeConverted = ConvertStringDateTimeToMilisecond.ToMiliSecond(endTime);
            var account = AccountModel.FindById(accountNumber);
            if (account == null)
            {
                return null;
            }
            else if (account.LockTransaction == 1 || account.Status == 0 || account.Status == -1) //check khoa giao dich
            {
                Console.WriteLine("The account is not active, please contact the bank staff for processing !!!");
                return null;
            }

            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand mySqlCommand =
                    new MySqlCommand(
                        $"select * from transactionhistory where " +
                        $"SenderAccountNumber = '{accountNumber}' and " +
                        $"CreateAt >= {startTimeConverted} and CreateAt <= {endTimeConverted} LIMIT {limit} OFFSET {offset}",
                        cnn);
                var result = mySqlCommand.ExecuteReader();
                if (result == null)
                {
                    return null;
                }

                while (result.Read())
                {
                    TransactionHistory transactionHistory = new TransactionHistory();
                    transactionHistory.ID = result.GetString("ID");
                    transactionHistory.SenderAccountNumber = result.GetString("SenderAccountNumber");
                    transactionHistory.ReceiverAccountNumber = result.GetString("ReceiverAccountNumber");
                    transactionHistory.Type = result.GetInt32("Type");
                    transactionHistory.Amount = result.GetDouble("Amount");
                    transactionHistory.Message = result.GetString("Message");
                    transactionHistory.CreateAt = result.GetInt64("CreateAt");
                    transactionHistory.UpdateAt = result.GetInt64("UpdateAt");
                    transactionHistory.DeleteAt = result.GetInt64("DeleteAt");
                    transactionHistory.Status = result.GetInt32("Status");
                    transactionHistoriesList.Add(transactionHistory);
                }
            }

            return transactionHistoriesList;
        }

        public TransactionHistory FinByID(string ID)
        {
            TransactionHistory transactionHistory1 = new TransactionHistory();
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand mySqlCommand =
                    new MySqlCommand(
                        $"select * from transactionhistory where ID  = '{ID}'", cnn);
                var result = mySqlCommand.ExecuteReader();
                if (result == null)
                {
                    return null;
                }

                if (result.Read())
                {
                    TransactionHistory transactionHistory = new TransactionHistory();
                    transactionHistory.ID = result.GetString("ID");
                    transactionHistory.SenderAccountNumber = result.GetString("SenderAccountNumber");
                    transactionHistory.ReceiverAccountNumber = result.GetString("ReceiverAccountNumber");
                    transactionHistory.Type = result.GetInt32("Type");
                    transactionHistory.Amount = result.GetDouble("Amount");
                    transactionHistory.Message = result.GetString("Message");
                    transactionHistory.CreateAt = result.GetInt64("CreateAt");
                    transactionHistory.UpdateAt = result.GetInt64("UpdateAt");
                    transactionHistory.DeleteAt = result.GetInt64("DeleteAt");
                    transactionHistory.Status = result.GetInt32("Status");
                    return transactionHistory;
                }
            }

            return null;
        }

        public List<TransactionHistory> FindAllTransactionHistory(int offset, int limit)
        {
            List<TransactionHistory> transactionHistoriesList = new List<TransactionHistory>();
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand mySqlCommand =
                    new MySqlCommand(
                        $"select * from transactionhistory LIMIT {limit} OFFSET {offset}",
                        cnn);
                var result = mySqlCommand.ExecuteReader();
                if (result == null)
                {
                    return null;
                }

                while (result.Read())
                {
                    TransactionHistory transactionHistory = new TransactionHistory();
                    transactionHistory.ID = result.GetString("ID");
                    transactionHistory.SenderAccountNumber = result.GetString("SenderAccountNumber");
                    transactionHistory.ReceiverAccountNumber = result.GetString("ReceiverAccountNumber");
                    transactionHistory.Type = result.GetInt32("Type");
                    transactionHistory.Amount = result.GetDouble("Amount");
                    transactionHistory.Message = result.GetString("Message");
                    transactionHistory.CreateAt = result.GetInt64("CreateAt");
                    transactionHistory.UpdateAt = result.GetInt64("UpdateAt");
                    transactionHistory.DeleteAt = result.GetInt64("DeleteAt");
                    transactionHistory.Status = result.GetInt32("Status");
                    transactionHistoriesList.Add(transactionHistory);
                }
            }

            return transactionHistoriesList;
        }

        public int CountNumberOffTransactionHistory()
        {
            var count = 0;
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand mySqlCommand =
                    new MySqlCommand(
                        $"select * from transactionhistory",
                        cnn);
                var result = mySqlCommand.ExecuteReader();
                if (result == null)
                {
                    return 0;
                }

                while (result.Read())
                {
                    count++;
                }
            }

            return count;
        }
        //done

        //gui tien
        public TransactionHistory Deposit(string accountNumber, double amount)
        {
            var account = AccountModel.FindById(accountNumber);
            if (account == null)
            {
                return null;
            }
            else if (account.LockTransaction == 1 || account.Status == 0 || account.Status == -1) //check khoa giao dich
            {
                Console.WriteLine("The account is not active, please contact the bank staff for processing !!!");
                return null;
            }

            account.Balance += amount;
            var result = AccountModel.Update(account.AccountNumber, account);
            if (result)
            {
                var transactionHistory = new TransactionHistory(account.AccountNumber,
                    account.AccountNumber, 2, amount, "successful deposit")
                {
                    Status = 1 //deposit suscess
                };
                saveTransactionHistory(transactionHistory); //save to database
                return transactionHistory;
            }
            else
            {
                Console.WriteLine("Deposit failed !!!");
                return null;
            }
        } //done

        //rut tien
        public TransactionHistory Withdraw(string accountNumber, double amount)
        {
            var account = AccountModel.FindById(accountNumber);
            if (account == null)
            {
                return null;
            }

            if (account.LockTransaction == 1 || account.Status is 0 or -1) //check khoa giao dich
            {
                Console.WriteLine("The account is not active, please contact the bank staff for processing !!!");
                return null;
            }

            if (account.Balance < amount)
            {
                Console.WriteLine("You don't have enough money to withdraw !!!");
                return null;
            }

            account.Balance -= amount;
            var result = AccountModel.Update(account.AccountNumber, account);
            if (result)
            {
                Console.WriteLine("successful Withdraw !!!");
                var transactionHistory = new TransactionHistory(account.AccountNumber,
                    account.AccountNumber, 1, amount, "successful withdraw")
                {
                    Status = 1 //withdraw suscess
                };
                saveTransactionHistory(transactionHistory);
                return transactionHistory;
            }
            else
            {
                Console.WriteLine("Withdraw failed !!!");
                return null;
            }
        } //done

        //chuyen tien
        public TransactionHistory Transfer(string senderAccountNumber, string receiverAccountNumber, double amount,
            string message)
        {
            var check = true;
            var accountSender = AccountModel.FindById(senderAccountNumber);
            if (accountSender == null)
            {
                Console.WriteLine("Sending account does not exist");
                check = false;
            }

            if (accountSender.LockTransaction == 1 || accountSender.Status is 0 or -1) //check khoa giao dich
            {
                Console.WriteLine("The Seding account is not active, please contact the bank staff for processing !!!");
                check = false;
            }

            var accountReceiver = AccountModel.FindById(receiverAccountNumber);
            if (accountReceiver == null)
            {
                Console.WriteLine("Receiver account does not exist");
                check = false;
            }
            else if (accountReceiver.LockTransaction == 1 || accountReceiver.Status is 0 or -1) //check khoa giao dich
            {
                Console.WriteLine("The Receiver account is not active");
                check = false;
            }

            if (accountSender.Balance < amount)
            {
                Console.WriteLine("You don't have enough money to withdraw !!!");
                check = false;
            }

            if (check == false)
            {
                return null;
            }

            accountSender.Balance -= amount;
            accountReceiver.Balance += amount;
            AccountModel.Update(accountReceiver.AccountNumber, accountReceiver);
            AccountModel.Update(accountSender.AccountNumber, accountSender);
            Console.WriteLine("Money transfer successful");

            var transactionHistory = new TransactionHistory(accountSender.AccountNumber,
                accountReceiver.AccountNumber, 3, amount, message)
            {
                Status = 1 //tranfer suscess
            };
            saveTransactionHistory(transactionHistory);
            return transactionHistory;
        } //done
    }
}