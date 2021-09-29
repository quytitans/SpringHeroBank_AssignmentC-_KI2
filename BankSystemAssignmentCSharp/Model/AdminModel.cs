using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;
using MySql.Data.MySqlClient;
using QuanLySinhVienVoiCSharp.Util;

namespace BankSystemAssignmentCSharp.Model
{
    public class AdminModel : IAdminModel
    {
        public bool Save(Admin account)
        {
            var checkExist = FindByUsername(account.Username);
            if (checkExist != null)
            {
                Console.WriteLine("Username already exist !!! Please try again !!!");
                return false;
            }

            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        $"insert into adminlistdata (Id, Username, Salt, PasswordHash,FullName,Phone, CreateAt, UpdateAt, DeleteAt, Status) " +
                        $"values ('{account.Id}', '{account.Username}', '{account.Salt}', '{account.PasswordHash}', '{account.FullName}', {account.Phone}, {account.CreateAt}, {account.UpdateAt}, {account.DeleteAt}, {account.Status})",
                        cnn);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        } //done

        public bool Update(string id, Admin updateAccount)
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
                    $"UPDATE adminlistdata SET " +
                    $"Id = '{updateAccount.Id}'," +
                    $"Username = '{updateAccount.Username}'," +
                    $"Salt = '{updateAccount.Salt}'," +
                    $"PasswordHash = '{updateAccount.PasswordHash}'," +
                    $"FullName = '{updateAccount.FullName}'," +
                    $"Phone = '{updateAccount.Phone}'," +
                    $"CreateAt = {updateAccount.CreateAt}," +
                    $"UpdateAt = {updateAccount.UpdateAt}," +
                    $"DeleteAt = {updateAccount.DeleteAt}," +
                    $"Status = {updateAccount.Status} " +
                    $"WHERE Id = '{id}';";
                MySqlCommand mySqlCommand = new MySqlCommand(querry, cnn);
                var result = mySqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        } //done

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
        } //done

        public Admin FindById(string id)
        {
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                Admin admin = new Admin();
                cnn.Open();
                var strQuerry = $"select * from adminlistdata where Id  = '{id}'";
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
                        admin.Id = result.GetString("Id");
                        admin.Username = result.GetString("Username");
                        admin.Salt = result.GetString("Salt");
                        admin.PasswordHash = result.GetString("PasswordHash");
                        admin.FullName = result.GetString("FullName");
                        admin.Phone = result.GetString("Phone");
                        admin.CreateAt = result.GetInt64("CreateAt");
                        admin.UpdateAt = result.GetInt64("UpdateAt");
                        admin.DeleteAt = result.GetInt64("DeleteAt");
                        admin.Status = result.GetInt32("Status");
                        return admin;
                    }
                }
            }

            return null;
        } //done

        public Admin FindByUsername(string Username1)
        {
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                Admin admin = new Admin();
                cnn.Open();
                var strQuerry = $"select * from adminlistdata where Username  = '{Username1}'";
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
                        admin.Id = result.GetString("Id");
                        admin.Username = result.GetString("Username");
                        admin.Salt = result.GetString("Salt");
                        admin.PasswordHash = result.GetString("PasswordHash");
                        admin.FullName = result.GetString("FullName");
                        admin.Phone = result.GetString("Phone");
                        admin.CreateAt = result.GetInt64("CreateAt");
                        admin.UpdateAt = result.GetInt64("UpdateAt");
                        admin.DeleteAt = result.GetInt64("DeleteAt");
                        admin.Status = result.GetInt32("Status");
                        return admin;
                    }
                }
            }

            return null;
        } // done

        public List<Admin> FindAll()
        {
            List<Admin> listAcc = new List<Admin>();
            using (var cnn = ConnectionHelperCSharp.GetConnection())
            {
                cnn.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("select * from adminlistdata", cnn);
                var result = mySqlCommand.ExecuteReader();
                if (result == null)
                {
                    return null;
                }

                while (result.Read())
                {
                    Admin admin = new Admin();
                    admin.Id = result.GetString("Id");
                    admin.Username = result.GetString("Username");
                    admin.Salt = result.GetString("Salt");
                    admin.PasswordHash = result.GetString("PasswordHash");
                    admin.FullName = result.GetString("FullName");
                    admin.Phone = result.GetString("Phone");
                    admin.CreateAt = result.GetInt64("CreateAt");
                    admin.UpdateAt = result.GetInt64("UpdateAt");
                    admin.DeleteAt = result.GetInt64("DeleteAt");
                    admin.Status = result.GetInt32("Status");
                    listAcc.Add(admin);
                }
            }

            return listAcc;
        } //done

        //tuong tac voi tai khoan nguoi dung, thay doi trang thai
        private AccountModel accountModel = new AccountModel();

        //thay doi cho phep/khong cho phep giao dich
        public bool ChangeAccLockTransaction(string AccountNumber, int newLockTransaction)
        {
            var result = accountModel.FindById(AccountNumber);
            if (result == null)
            {
                return false;
            }
            else
            {
                result.LockTransaction = newLockTransaction;
                var result2 = accountModel.Update(result.AccountNumber, result);
                if (result2)
                {
                    return true;
                }
            }

            return false;
        } //done

        public bool ChangeAccStatus(string AccountNumber, int newStatus)
        {
            var result = accountModel.FindById(AccountNumber);
            if (result == null)
            {
                return false;
            }
            else
            {
                result.Status = newStatus;
                var result2 = accountModel.Update(result.AccountNumber, result);
                if (result2)
                {
                    return true;
                }
            }

            return false;
        } //done
    }
}