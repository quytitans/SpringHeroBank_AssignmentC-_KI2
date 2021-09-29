using System;
using System.Collections.Generic;
using System.Globalization;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Util
{
    public class ValidateInput
    {
        public static Dictionary<string, string> IsValidRegister(Account account)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (account.Username.Length < 5 || account.Username == "")
            {
                dictionary.Add("Username", "Username must be at least 5 characters");
            }

            if (account.Password.Length < 6)
            {
                dictionary.Add("Password", "Password must be at least 6 characters");
            }

            if (!account.Password.Equals(account.ConfirmPassword))
            {
                dictionary.Add("ConfirmPassword", "Password and Confirm-Password must be the same");
            }

            if (!isValidEmail(account.Email))
            {
                dictionary.Add("Email", "Invalid email");
            }

            if (account.FirstName.Length < 2 || account.FirstName == "")
            {
                dictionary.Add("FirstName", "FirstName must be at least 2 characters");
            }

            if (account.LastName.Length < 2 || account.LastName == "")
            {
                dictionary.Add("LastName", "LastName must be at least 2 characters");
            }

            if (account.IdentityNumber.Length < 10 || account.IdentityNumber == "")
            {
                dictionary.Add("IdentityNumber", "IdentityNumber must be at least 10 characters");
            }

            if (account.Phone.Length < 10 || account.Phone == "")
            {
                dictionary.Add("Phone", "Phone number must be at least 10 characters");
            }
            if (account.Gender == 99)
            {
                dictionary.Add("Gender", "Please select male or female to your gender");
            }
            if (account.Dob == 123)
            {
                dictionary.Add("Dob", "Date of birth must be used format (dd/MM/yyyy) example (30/01/1992)");
            }

            return dictionary;
        }
        
        public static Dictionary<string, string> IsValidRegisterAdmin(Admin account)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (account.Username.Length < 5 || account.Username == "")
            {
                dictionary.Add("Username", "Username must be at least 5 characters");
            }

            if (account.Password.Length < 6)
            {
                dictionary.Add("Password", "Password must be at least 6 characters");
            }

            if (!account.Password.Equals(account.ConfirmPassword))
            {
                dictionary.Add("ConfirmPassword", "Password and Confirm-Password must be the same");
            }

            if (account.FullName.Length < 2 || account.FullName == "")
            {
                dictionary.Add("FullName", "FullName must be at least 2 characters");
            }
            
            if (account.Phone.Length < 10 || account.Phone == "")
            {
                dictionary.Add("Phone", "Phone number must be at least 10 characters");
            }

            return dictionary;
        }
        public static Dictionary<string, string> IsValidLogin(string username, string password)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (username.Length < 5 || username == "")
            {
                dictionary.Add("Username", "Username must be at least 5 characters");
            }

            if (password.Length < 6)
            {
                dictionary.Add("Password", "Password must be at least 6 characters");
            }
            return dictionary;
        }
        public static bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool isDateTimeFormat(string value)
        {
            DateTime tempDate;
            bool validDate = DateTime.TryParseExact(value, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo,
                DateTimeStyles.None, out tempDate);
            if (validDate)
                return true;
            else
            {
                return false;
            }
        }
    }
}