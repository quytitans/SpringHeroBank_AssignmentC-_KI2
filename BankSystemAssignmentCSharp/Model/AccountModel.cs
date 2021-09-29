using System;
using System.Collections.Generic;
using System.Linq;
using BankSystemAssignmentCSharp.Entity;
using MySql.Data.MySqlClient;
using QuanLySinhVienVoiCSharp.Util;

namespace BankSystemAssignmentCSharp.Model
{
    public class AccountModel : IAccountModel
    {
        public bool Save(Account account)
        {
            var checkExist = FindByUsername(account.Username);
            if (checkExist != null)
            {
                Console.WriteLine("Username already exist !!! Please try again !!!");
                return false;
            }

            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                try
                {
                    cnn.Open();
                    MySqlCommand cmd =
                        new MySqlCommand(
                            $"insert into bankdatabase (AccountNumber, Type, Balance, Username, PasswordHash, Salt, LockTransaction, FirstName, LastName, Dob, Gender, Email, IdentityNumber, Phone, CreateAt, UpdateAt, DeleteAt, Status) " +
                            $"values ('{account.AccountNumber}', {account.Type}, {account.Balance}, '{account.Username}', '{account.PasswordHash}', '{account.Salt}', {account.LockTransaction}, '{account.FirstName}', '{account.LastName}', " +
                            $"{account.Dob}, {account.Gender}, '{account.Email}', '{account.IdentityNumber}', '{account.Phone}', {account.CreateAt}, {account.UpdateAt}, {account.DeleteAt}, {account.Status})",
                            cnn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return false;
        } //Done

        public bool Update(string id, Account updateAccount)
        {
            var checkExist = FindById(id);
            if (checkExist == null)
            {
                Console.WriteLine("This ID is not exist, please try again !!!");
                return false;
            }

            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                var querry =
                    $"UPDATE bankdatabase SET " +
                    $"AccountNumber  = '{updateAccount.AccountNumber}', " +
                    $"Type = {updateAccount.Type}, " +
                    $"Balance = {updateAccount.Balance}," +
                    $"Username = '{updateAccount.Username}'," +
                    $"PasswordHash = '{updateAccount.PasswordHash}'," +
                    $"Salt = '{updateAccount.Salt}'," +
                    $"LockTransaction = {updateAccount.LockTransaction}," +
                    $"FirstName = '{updateAccount.FirstName}'," +
                    $"LastName = '{updateAccount.LastName}'," +
                    $"Dob = {updateAccount.Dob}," +
                    $"Gender = {updateAccount.Gender}," +
                    $"Email = '{updateAccount.Email}'," +
                    $"IdentityNumber = '{updateAccount.IdentityNumber}'," +
                    $"Phone = '{updateAccount.Phone}'," +
                    $"CreateAt = {updateAccount.CreateAt}," +
                    $"UpdateAt = {updateAccount.UpdateAt}," +
                    $"DeleteAt = {updateAccount.DeleteAt}," +
                    $"Status = {updateAccount.Status} " +
                    $"WHERE AccountNumber = '{id}';";
                MySqlCommand mySqlCommand = new MySqlCommand(querry, cnn);
                var result = mySqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        } //Done

        public bool Delete(string id)
        {
            var checkExist = FindById(id);
            if (checkExist == null)
            {
                Console.WriteLine("This ID is not exist, please try again !!!");
                return false;
            }
            else
            {
                //thay doi status sang deleted, khoi tao thoi gian xoa account
                checkExist.Status = -1;
                checkExist.DeleteAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }

            if (Update(id, checkExist))
            {
                Console.WriteLine("Deleted this account !!!!");
                return true;
            }

            return false;
        } //Done

        public Account FindById(string id)
        {
            try
            {
                using (var cnn = ConnectionHelperCSharp.GetConnection())
                {
                    Account account = new Account();
                    cnn.Open();
                    string strQuerry = $"select * from bankdatabase where AccountNumber  = '{id}'";
                    MySqlCommand mySqlCommand = new MySqlCommand(strQuerry, cnn);
                    var result = mySqlCommand.ExecuteReader();
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (result.Read())
                        {
                            account.AccountNumber = result.GetString("AccountNumber");
                            account.Type = result.GetInt32("Type");
                            account.Balance = result.GetDouble("Balance");
                            account.Username = result.GetString("Username");
                            account.PasswordHash = result.GetString("PasswordHash");
                            account.Salt = result.GetString("Salt");
                            account.LockTransaction = result.GetInt32("LockTransaction");
                            account.FirstName = result.GetString("FirstName");
                            account.LastName = result.GetString("LastName");
                            account.Dob = result.GetInt64("Dob");
                            account.Gender = result.GetInt32("Gender");
                            account.Email = result.GetString("Email");
                            account.IdentityNumber = result.GetString("IdentityNumber");
                            account.Phone = result.GetString("Phone");
                            account.CreateAt = result.GetInt64("CreateAt");
                            account.UpdateAt = result.GetInt64("UpdateAt");
                            account.DeleteAt = result.GetInt64("DeleteAt");
                            account.Status = result.GetInt32("Status");
                            return account;
                        }
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public List<Account> FindByName(string firstName, string lastName)
        {
            List<Account> ListResult = new List<Account>();
            try
            {
                using (var cnn = ConnectionHelperCSharp.GetConnection())
                {
                    Account account = new Account();
                    cnn.Open();
                    string strQuerry =
                        $"select * from bankdatabase where FirstName  = '{firstName}' and LastName = '{lastName}'";
                    MySqlCommand mySqlCommand = new MySqlCommand(strQuerry, cnn);
                    var result = mySqlCommand.ExecuteReader();
                    if (!result.Read())
                    {
                        return null;
                    }
                    {
                        while (result.Read())
                        {
                            account.AccountNumber = result.GetString("AccountNumber");
                            account.Type = result.GetInt32("Type");
                            account.Balance = result.GetDouble("Balance");
                            account.Username = result.GetString("Username");
                            account.PasswordHash = result.GetString("PasswordHash");
                            account.Salt = result.GetString("Salt");
                            account.LockTransaction = result.GetInt32("LockTransaction");
                            account.FirstName = result.GetString("FirstName");
                            account.LastName = result.GetString("LastName");
                            account.Dob = result.GetInt64("Dob");
                            account.Gender = result.GetInt32("Gender");
                            account.Email = result.GetString("Email");
                            account.IdentityNumber = result.GetString("IdentityNumber");
                            account.Phone = result.GetString("Phone");
                            account.CreateAt = result.GetInt64("CreateAt");
                            account.UpdateAt = result.GetInt64("UpdateAt");
                            account.DeleteAt = result.GetInt64("DeleteAt");
                            account.Status = result.GetInt32("Status");
                            ListResult.Add(account);
                        }

                        return ListResult;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("This user is not exist, please try again");
                return null;
            }

            return null;
        }
        //done

        public Account FindByUsername(string username)
        {
            try
            {
                using (var cnn = ConnectionHelperCSharp.GetConnection())
                {
                    Account account = new Account();
                    cnn.Open();
                    string strQuerry = $"select * from bankdatabase where Username = '{username}'";
                    MySqlCommand mySqlCommand = new MySqlCommand(strQuerry, cnn);
                    var result = mySqlCommand.ExecuteReader();
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (result.Read())
                        {
                            account.AccountNumber = result.GetString("AccountNumber");
                            account.Type = result.GetInt32("Type");
                            account.Balance = result.GetDouble("Balance");
                            account.Username = result.GetString("Username");
                            account.PasswordHash = result.GetString("PasswordHash");
                            account.Salt = result.GetString("Salt");
                            account.LockTransaction = result.GetInt32("LockTransaction");
                            account.FirstName = result.GetString("FirstName");
                            account.LastName = result.GetString("LastName");
                            account.Dob = result.GetInt64("Dob");
                            account.Gender = result.GetInt32("Gender");
                            account.Email = result.GetString("Email");
                            account.IdentityNumber = result.GetString("IdentityNumber");
                            account.Phone = result.GetString("Phone");
                            account.CreateAt = result.GetInt64("CreateAt");
                            account.UpdateAt = result.GetInt64("UpdateAt");
                            account.DeleteAt = result.GetInt64("DeleteAt");
                            account.Status = result.GetInt32("Status");
                            return account;
                        }
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        } //done

        public List<Account> FindAll()
        {
            List<Account> listAcc = new List<Account>();
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("select * from bankdatabase", cnn);
                var result = mySqlCommand.ExecuteReader();
                if (result == null)
                {
                    return null;
                }

                while (result.Read())
                {
                    Account account = new Account();
                    account.AccountNumber = result.GetString("AccountNumber");
                    account.Type = result.GetInt32("Type");
                    account.Balance = result.GetDouble("Balance");
                    account.Username = result.GetString("Username");
                    account.PasswordHash = result.GetString("PasswordHash");
                    account.Salt = result.GetString("Salt");
                    account.LockTransaction = result.GetInt32("LockTransaction");
                    account.FirstName = result.GetString("FirstName");
                    account.LastName = result.GetString("LastName");
                    account.Dob = result.GetInt64("Dob");
                    account.Gender = result.GetInt32("Gender");
                    account.Email = result.GetString("Email");
                    account.IdentityNumber = result.GetString("IdentityNumber");
                    account.Phone = result.GetString("Phone");
                    account.CreateAt = result.GetInt64("CreateAt");
                    account.UpdateAt = result.GetInt64("UpdateAt");
                    account.DeleteAt = result.GetInt64("DeleteAt");
                    account.Status = result.GetInt32("Status");
                    listAcc.Add(account);
                }
            }

            return listAcc;
        } //Done

        public List<Account> SearchByPhone(string keyword)
        {
            List<Account> lisAcc = new List<Account>();
            try
            {
                using (var cnn = ConnectionHelperCSharp.GetConnection())
                {
                    cnn.Open();
                    string strQuerry = $"select * from bankdatabase where Phone = '{keyword}'";
                    MySqlCommand mySqlCommand = new MySqlCommand(strQuerry, cnn);
                    var result = mySqlCommand.ExecuteReader();
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        while (result.Read())
                        {
                            Account account = new Account();
                            account.AccountNumber = result.GetString("AccountNumber");
                            account.Type = result.GetInt32("Type");
                            account.Balance = result.GetDouble("Balance");
                            account.Username = result.GetString("Username");
                            account.PasswordHash = result.GetString("PasswordHash");
                            account.Salt = result.GetString("Salt");
                            account.LockTransaction = result.GetInt32("LockTransaction");
                            account.FirstName = result.GetString("FirstName");
                            account.LastName = result.GetString("LastName");
                            account.Dob = result.GetInt64("Dob");
                            account.Gender = result.GetInt32("Gender");
                            account.Email = result.GetString("Email");
                            account.IdentityNumber = result.GetString("IdentityNumber");
                            account.Phone = result.GetString("Phone");
                            account.CreateAt = result.GetInt64("CreateAt");
                            account.UpdateAt = result.GetInt64("UpdateAt");
                            account.DeleteAt = result.GetInt64("DeleteAt");
                            account.Status = result.GetInt32("Status");
                            lisAcc.Add(account);
                        }

                        return lisAcc;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        } //Done

        public List<Account> SearchByIndentityNumber(string keyword)
        {
            List<Account> lisAcc = new List<Account>();
            try
            {
                using (var cnn = ConnectionHelperCSharp.GetConnection())
                {
                    cnn.Open();
                    string strQuerry = $"select * from bankdatabase where IdentityNumber = '{keyword}'";
                    MySqlCommand mySqlCommand = new MySqlCommand(strQuerry, cnn);
                    var result = mySqlCommand.ExecuteReader();
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        while (result.Read())
                        {
                            Account account = new Account();
                            account.AccountNumber = result.GetString("AccountNumber");
                            account.Type = result.GetInt32("Type");
                            account.Balance = result.GetDouble("Balance");
                            account.Username = result.GetString("Username");
                            account.PasswordHash = result.GetString("PasswordHash");
                            account.Salt = result.GetString("Salt");
                            account.LockTransaction = result.GetInt32("LockTransaction");
                            account.FirstName = result.GetString("FirstName");
                            account.LastName = result.GetString("LastName");
                            account.Dob = result.GetInt64("Dob");
                            account.Gender = result.GetInt32("Gender");
                            account.Email = result.GetString("Email");
                            account.IdentityNumber = result.GetString("IdentityNumber");
                            account.Phone = result.GetString("Phone");
                            account.CreateAt = result.GetInt64("CreateAt");
                            account.UpdateAt = result.GetInt64("UpdateAt");
                            account.DeleteAt = result.GetInt64("DeleteAt");
                            account.Status = result.GetInt32("Status");
                            lisAcc.Add(account);
                        }

                        return lisAcc;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        } //Done

        public List<Account> SearchByStatus(int status)
        {
            List<Account> lisAcc = new List<Account>();
            try
            {
                using (var cnn = ConnectionHelperCSharp.GetConnection())
                {
                    cnn.Open();
                    string strQuerry = $"select * from bankdatabase where Status = {status}";
                    MySqlCommand mySqlCommand = new MySqlCommand(strQuerry, cnn);
                    var result = mySqlCommand.ExecuteReader();
                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        while (result.Read())
                        {
                            Account account = new Account();
                            account.AccountNumber = result.GetString("AccountNumber");
                            account.Type = result.GetInt32("Type");
                            account.Balance = result.GetDouble("Balance");
                            account.Username = result.GetString("Username");
                            account.PasswordHash = result.GetString("PasswordHash");
                            account.Salt = result.GetString("Salt");
                            account.LockTransaction = result.GetInt32("LockTransaction");
                            account.FirstName = result.GetString("FirstName");
                            account.LastName = result.GetString("LastName");
                            account.Dob = result.GetInt64("Dob");
                            account.Gender = result.GetInt32("Gender");
                            account.Email = result.GetString("Email");
                            account.IdentityNumber = result.GetString("IdentityNumber");
                            account.Phone = result.GetString("Phone");
                            account.CreateAt = result.GetInt64("CreateAt");
                            account.UpdateAt = result.GetInt64("UpdateAt");
                            account.DeleteAt = result.GetInt64("DeleteAt");
                            account.Status = result.GetInt32("Status");
                            lisAcc.Add(account);
                        }

                        return lisAcc;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        } //Done
    }
}