using System;
using BankSystemAssignmentCSharp.Util;
using SpringHeroBank.utility;

namespace BankSystemAssignmentCSharp.Entity
{
    public class Admin
    {
        private long _createAt;
        private long _updateAt;
        private long _deleteAt;
        public string Id { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
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
        public string strStatus()
        {
            if (Status ==1)
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

        public Admin()
        {
        }

        public Admin(string username, string password, string confirmPassword,
            string fullName, string phone)
        {
            Id = Guid.NewGuid().ToString(); //xu ly them khi co ham find admin by id
            Username = username;
            Salt = Hash.RandomString(6);
            Password = password;
            ConfirmPassword = confirmPassword;
            PasswordHash = ProcessHashPassword(password, Salt);
            FullName = fullName;
            Phone = phone;
            CreateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            UpdateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            DeleteAt = 0; //default = 0 cho tai khoan con dang hoạt dong
            Status = 0; //1 là active, 2 là khóa, -1 là deleted, 0 : chuakichhoat
        }


        public override string ToString()
        {
            return
                $"{Id,40}{ "|",2}{Username,15}{ "|",2}{FullName,15}{ "|",2}{Phone,12}{ "|",2}{strCreateAt(),15}{ "|",2}{strUpdateAt(),15}{ "|",2}{strDeleteAt(),15}{ "|",2}{strStatus(),8}";
        }

        //ma hoa password
        private string ProcessHashPassword(string pass, string salt1)
        {
            string _hashPassword = Hash.GenerateLordPassWord(pass, salt1);
            return _hashPassword;
        }
    }
}