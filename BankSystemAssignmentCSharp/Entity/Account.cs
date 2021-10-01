using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;
using BankSystemAssignmentCSharp.Model;
using BankSystemAssignmentCSharp.Util;
using Hash = SpringHeroBank.utility.Hash;

namespace BankSystemAssignmentCSharp.Entity
{
    public class Account
    {
        private long _createAt;
        private long _updateAt;
        private long _deleteAt;
        private long _dob;
        public string AccountNumber { get; set; }
        public int Type { get; set; }
        public double Balance { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int LockTransaction { get; set; } //cho phep khoa giao dich
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long Dob
        {
            get => _dob;
            set => _dob = value;
        }

        public int Gender { get; set; } //0 is male, 1 is female
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }

        public long CreateAt
        {
            get => _createAt;
            set => _createAt = value;
        }

        public long UpdateAt
        {
            get => _updateAt;
            set => _updateAt = value;
        }

        public long DeleteAt
        {
            get => _deleteAt;
            set => _deleteAt = value;
        }

        public int Status { get; set; }

        //convert milisecond to string date time format
        public string strCreateAt()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_createAt);
        }

        public string strUpdateAt()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_updateAt);
        }

        public string strDeleteAt()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_deleteAt);
        }

        public string strDoB()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_dob);
        }

        public string strLockTransaction()
        {
            if (LockTransaction == 1)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }

        public string strStatus()
        {
            if (Status == 1)
            {
                return "Active";
            }

            if (Status == 0)
            {
                return "Inactive";
            }

            if (Status == -1)
            {
                return "Deleted";
            }
            else
            {
                return "Null";
            }
        }

        //end convert block
        public Account(string username, string password, string confirmpassword
            , string firstName, string lastName, long dob,
            int gender, string email, string identityNumber, string phone)
        {
            Account result;
            do
            {
                AccountNumber = Guid.NewGuid().ToString();
                AccountModel accountModel = new AccountModel();
                result = accountModel.FindById(AccountNumber);
            } while (result != null);

            Salt = Hash.RandomString(6);
            Type = 1; //defaulf 1 la tai khoan ca nhan 2 la tai khoan doanh nghiep
            Balance = 0;
            Username = username;
            Password = password;
            ConfirmPassword = confirmpassword;
            PasswordHash = ProcessHashPassword(password, Salt);
            LockTransaction = 1; //khóa giao dịch trong thời điển tạo 1 : true, 2: false
            FirstName = firstName;
            LastName = lastName;
            Dob = dob;
            Gender = gender;
            Email = email;
            IdentityNumber = identityNumber;
            Phone = phone;
            CreateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            UpdateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            DeleteAt = 0;
            Status = 0; //1 là active, 2 là khoa, -1 là deleted, 0 : chua kich hoat
        }

        public Account()
        {
        }

        public override string ToString()
        {
            return
                $"{AccountNumber,40}{"|",2}{Balance,15}{"|",2}{Username,15}{"|",2}{strLockTransaction(),5}{"|",2}{FirstName,10}{"|",2}{LastName,10}{"|",2}{IdentityNumber,18}{"|",2}{Phone,17}{"|",2}{strCreateAt(),15}{"|",2}{strUpdateAt(),15}{"|",2}{strDeleteAt(),15}{"|",2}{strStatus(),8}";
        }

        private string ProcessHashPassword(string pass, string salt1)
        {
            string _hashPassword = Hash.GenerateLordPassWord(pass, salt1);
            return _hashPassword;
        }
    }
}