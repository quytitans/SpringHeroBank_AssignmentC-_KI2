using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using BankSystemAssignmentCSharp.Entity;
using BankSystemAssignmentCSharp.Model;
using BankSystemAssignmentCSharp.Util;
using BankSystemAssignmentCSharp.View;
using SpringHeroBank.utility;

namespace BankSystemAssignmentCSharp.Controller
{
    public class UserController : IUserController
    {
        AccountModel accountModel = new AccountModel();
        private AdminModel AdminModel = new AdminModel();
        private TransactionModel TransactionModel = new TransactionModel();

        public Account Register()
        {
            var check = true;
            var newAcc = new Account();
            do
            {
                Console.WriteLine("Wellcome to Spring Hero Bank - Registing new Acount");
                newAcc = GetUserInfo();
                var Errors = ValidateInput.IsValidRegister(newAcc);
                if (Errors.Count > 0)
                {
                    check = true;
                    ShowErrorMesage(Errors);
                }
                else if (Errors.Count == 0)
                {
                    check = false;
                }
            } while (check);

            var result = accountModel.Save(newAcc);
            if (result)
            {
                Console.WriteLine("saved new account success !!!");
                return newAcc;
            }
            else
            {
                Console.WriteLine("saving false, please try again !!!!");
                return null;
            }
        }

        public void ShowBankInformation()
        {
            throw new System.NotImplementedException();
        }

        //cho phep dang nhap chung ca admin va user
        public void LoginGeneral()
        {
            var checkLoop = true;
            var password = "";
            var userName = "";
            while (checkLoop)
            {
                Console.WriteLine("Enter username: ");
                userName = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
                var checkValid = ValidateInput.IsValidLogin(userName, password);
                if (checkValid.Count > 0)
                {
                    ShowErrorMesage(checkValid);
                }

                if (checkValid.Count == 0)
                {
                    checkLoop = false;
                }
            }

            var accountUser = accountModel.FindByUsername(userName);
            var accountAdmin = AdminModel.FindByUsername(userName);
            if (accountAdmin == null && accountUser == null)
            {
                Console.WriteLine("This Username is not exist, please try again");
            }

            //trien khai case user dang nhap
            if (accountUser != null)
            {
                if (accountUser.Status is 0 or -1)
                {
                    Console.WriteLine(
                        "Your acount is not active or deleted, please contact to banker staff to process");
                }
                else
                {
                    var checkPass = Hash.GenerateLordPassWord(password, accountUser.Salt)
                        .Equals(accountUser.PasswordHash);
                    if (checkPass)
                    {
                        Console.WriteLine("Login success !!!");
                        GenerateGeneralMenu.GenerateMenuUserMode(accountUser);
                    }
                    else
                    {
                        Console.WriteLine("Login false !!! Please try again !!!");
                    }
                }
            }

            //trien khai case admin dang nhap
            if (accountAdmin != null)
            {
                if (accountAdmin.Status is 0 or -1)
                {
                    Console.WriteLine(
                        "Your acount is not active or deleted, please contact to banker staff to process");
                }
                else
                {
                    var checkPass = Hash.GenerateLordPassWord(password, accountAdmin.Salt)
                        .Equals(accountAdmin.PasswordHash);
                    if (checkPass)
                    {
                        Console.WriteLine("Login success !!!");
                        GenerateGeneralMenu.GenerateMenuAdminMode(accountAdmin);
                    }
                    else
                    {
                        Console.WriteLine("Login false !!! Please try again !!!");
                    }
                }
            }
        }

        public void WithDraw(Account accountLogin)
        {
            Console.WriteLine("Enter the amount you want to withdraw, then press enter: ");
            var amount = Double.Parse(Console.ReadLine());
            var result = TransactionModel.Withdraw(accountLogin.AccountNumber, amount);
            if (result != null)
            {
                Console.WriteLine("Withdraw success !!!");
                Console.WriteLine(
                    $"{"ID",40}{" |",2}{"SenderAccountNumber",40}{" |",2}{"ReceiverAccountNumber",40}{" |",2}{"Type",8}{" |",2}{"Amount",10}{" |",2}{"Message",28}{" |",2}{"CreateAt",13}{" |",2}{"Status",7}");
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(result.ToString());
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }

        public void Deposit(Account accountLogin)
        {
            Console.WriteLine("Enter the amount you want to deposit into the bank, then press enter: ");
            var amount = Double.Parse(Console.ReadLine());
            var result = TransactionModel.Deposit(accountLogin.AccountNumber, amount);
            if (result != null)
            {
                Console.WriteLine("Deposit success !!!");
                Console.WriteLine(
                    $"{"ID",40}{" |",2}{"SenderAccountNumber",40}{" |",2}{"ReceiverAccountNumber",40}{" |",2}{"Type",8}{" |",2}{"Amount",10}{" |",2}{"Message",28}{" |",2}{"CreateAt",13}{" |",2}{"Status",7}");
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(result.ToString());
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }

        public void Transfer(Account accountLogin)
        {
            Console.WriteLine("Enter Receiver Account number: ");
            var receiverAcc = Console.ReadLine();
            Console.WriteLine("Enter the amount you want to transfer: ");
            var amount = Double.Parse(Console.ReadLine());
            Console.WriteLine("Enter transfer message: ");
            var message = Console.ReadLine();
            var result = TransactionModel.Transfer(accountLogin.AccountNumber, receiverAcc, amount, message);
            if (result != null)
            {
                Console.WriteLine("Transfer success !!!");
                Console.WriteLine(
                    $"{"ID",40}{" |",2}{"SenderAccountNumber",40}{" |",2}{"ReceiverAccountNumber",40}{" |",2}{"Type",8}{" |",2}{"Amount",10}{" |",2}{"Message",28}{" |",2}{"CreateAt",13}{" |",2}{"Status",7}");
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(result.ToString());
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }

        public void CheckInformation(Account accountLogin)
        {
            var updateAcc = accountModel.FindById(accountLogin.AccountNumber);
            Console.WriteLine(
                $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(updateAcc.ToString());
            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public void CheckBalance(Account accountLogin)
        {
            var updateAcc = accountModel.FindById(accountLogin.AccountNumber);
            Console.WriteLine($"Your balance is: {updateAcc.Balance}");
        }

        public void UpdateInformation(Account accountLogin)
        {
            var check = true;
            var newAcc = new Account();
            do
            {
                Console.WriteLine("Change your account's information: ");
                newAcc = GetUserInfo();
                newAcc.AccountNumber = accountLogin.AccountNumber;
                newAcc.Type = accountLogin.Type;
                newAcc.LockTransaction = accountLogin.LockTransaction;
                newAcc.Balance = accountLogin.Balance;
                newAcc.CreateAt = accountLogin.CreateAt;
                newAcc.DeleteAt = accountLogin.CreateAt;
                newAcc.Status = accountLogin.Status;
                var Errors = ValidateInput.IsValidRegister(newAcc);
                if (Errors.Count > 0)
                {
                    check = true;
                    ShowErrorMesage(Errors);
                }
                else if (Errors.Count == 0)
                {
                    check = false;
                }
            } while (check);

            var result = accountModel.Update(accountLogin.AccountNumber, newAcc);
            if (result)
            {
                Console.WriteLine("saved new information success !!!");
            }
            else
            {
                Console.WriteLine("saving false, please try again !!!!");
            }
        }

        public void ChangePassword(Account accountLogin)
        {
            var Account1 = accountLogin;
            Console.WriteLine("Enter current password: ");
            var newPass = "";
            var currentPass = Console.ReadLine();
            var currentPassHash = Hash.GenerateLordPassWord(currentPass, accountLogin.Salt);
            if (currentPassHash.Equals(accountLogin.PasswordHash))
            {
                Console.WriteLine("Enter new password: ");
                newPass = Console.ReadLine();
                Console.WriteLine("Confirm new password: ");
                var confirmNewPass = Console.ReadLine();
                if (!newPass.Equals(confirmNewPass))
                {
                    Console.WriteLine("New password and the confirm must be the same, please try again");
                }
                else
                {
                    Account1.Password = newPass;
                    Account1.Salt = Hash.RandomString(5);
                    Account1.PasswordHash = Hash.GenerateLordPassWord(newPass, Account1.Salt);
                    Account1.UpdateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    var result = accountModel.Update(Account1.AccountNumber, Account1);
                    if (result)
                    {
                        Console.WriteLine("Change password complete !!!");
                    }
                    else
                    {
                        Console.WriteLine("Change password false !!!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Current password is not true, please try again !!!");
            }
        }

        public void LockAccount(Account accountLogin)
        {
            var updatedAcc = accountModel.FindById(accountLogin.AccountNumber);
            if (updatedAcc.LockTransaction == 0)
            {
                Console.WriteLine("Your account is OPEN transaction, do you want to lock it ? yes/no :");
                var choice = Console.ReadLine();
                if (choice.Equals("yes") || choice.Equals("Yes") || choice.Equals("YES"))
                {
                    var result = AdminModel.ChangeAccLockTransaction(updatedAcc.AccountNumber, 1);
                    if (result)
                    {
                        Console.WriteLine("Locked transaction success !!!");
                    }
                    else
                    {
                        Console.WriteLine("Lock transaction false !!!");
                    }
                }

                if (choice.Equals("No") || choice.Equals("no") || choice.Equals("NO"))
                {
                    Console.WriteLine("Canceled !!!");
                }
            }

            if (updatedAcc.LockTransaction == 1)
            {
                Console.WriteLine("Your account is LOCKING transaction, do you want to open it ?");
                var choice = Console.ReadLine();
                if (choice.Equals("yes") || choice.Equals("Yes") || choice.Equals("YES"))
                {
                    var result = AdminModel.ChangeAccLockTransaction(updatedAcc.AccountNumber, 0);
                    if (result)
                    {
                        Console.WriteLine("Open transaction success !!!");
                    }
                    else
                    {
                        Console.WriteLine("Open transaction false !!!");
                    }
                }

                if (choice.Equals("No") || choice.Equals("no") || choice.Equals("NO"))
                {
                    Console.WriteLine("Canceled !!!");
                }
            }
        }

        public void CheckTransactionHistory(Account accountLogin)
        {
            Console.WriteLine("Enter the time period to check: ");
            string startTime;
            string endTime;
            var loop1 = true;
            do
            {
                Console.WriteLine("Start date (dd/MM/yyy): ");
                startTime = Console.ReadLine();
                var check1 = ValidateInput.isDateTimeFormat(startTime);
                Console.WriteLine("End date (dd/MM/yyy): ");
                endTime = Console.ReadLine();
                var check2 = ValidateInput.isDateTimeFormat(endTime);
                if (check1 == true && check2 == true)
                {
                    loop1 = false;
                }
                else
                {
                    loop1 = true;
                    Console.WriteLine("Please enter datetime format // example: 30/01/1992");
                }
            } while (loop1);

            //end step 1
            TransactionModel transactionModel = new TransactionModel();
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            var offset = 0;
            var limit = 10;
            do
            {
                var page = offset / limit + 1;
                var totalPage = 0;
                if (transactionModel.CountNumberOffTransactionHistory() % limit == 0)
                {
                    totalPage = transactionModel.CountNumberOffTransactionHistory() / limit;
                }
                else
                {
                    totalPage = transactionModel.CountNumberOffTransactionHistory() / limit + 1;
                }

                var result =
                    TransactionModel.FindTransactionHistoryByAccountNumber(accountLogin.AccountNumber, startTime,
                        endTime, offset, limit);
                if (result != null)
                {
                    Console.WriteLine($"Your transaction history from {startTime} to {endTime}: ");
                    result.Display();
                    Console.WriteLine($"<<Back-- Page: {page}/{totalPage} --Next>>");
                    Console.WriteLine("Press DownArrow to escape");
                }

                if (result == null)
                {
                    Console.WriteLine("Account list is empty");
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (page < totalPage)
                        {
                            offset += limit;
                        }

                        break;

                    case ConsoleKey.LeftArrow:
                        if (page > 1)
                        {
                            offset -= limit;
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        Console.WriteLine("Close view, enter for more selection");
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                        }

                        break;
                }
            } while (!Console.KeyAvailable && key.Key != ConsoleKey.DownArrow);
        }

        public void ShowAllUserAccount()
        {
            AccountModel accountModel = new AccountModel();
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            var offset = 0;
            var limit = 5;
            do
            {
                var page = offset / limit + 1;
                var totalPage = 0;
                if (accountModel.CountAllAccount() % limit == 0)
                {
                    totalPage = accountModel.CountAllAccount() / limit;
                }
                else
                {
                    totalPage = accountModel.CountAllAccount() / limit + 1;
                }

                var result = accountModel.FindAll(offset, limit);

                if (result == null)
                {
                    Console.WriteLine("Account list is empty");
                }
                else
                {
                    result.Display();
                }

                Console.WriteLine($"<<Back-- Page: {page}/{totalPage} --Next>>");
                Console.WriteLine("Press DownArrow to escape");
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (page < totalPage)
                        {
                            offset += limit;
                        }

                        break;

                    case ConsoleKey.LeftArrow:
                        if (page > 1)
                        {
                            offset -= limit;
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        Console.WriteLine("Close view, enter for more selection");
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                        }

                        break;
                }
            } while (!Console.KeyAvailable && key.Key != ConsoleKey.DownArrow);
        }

        public void ReadFileTXT(string link)
        {
            Console.OutputEncoding = Encoding.Unicode;
            string[] lines;

            if (File.Exists(link))
            {
                lines = File.ReadAllLines(link);
                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine("{0}", lines[i]);
                }

                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }

        private Account GetUserInfo()
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter confirmpassword: ");
            string confirmpassword = Console.ReadLine();
            Console.WriteLine("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name: ");
            string lastName = Console.ReadLine();

            //check input Dob must be datetime string format, correct continue, incorrect dob = 123
            Console.WriteLine("Enter your date of birth (example: 30/01/1992): ");
            var strDate = Console.ReadLine();
            var check = ValidateInput.isDateTimeFormat(strDate);
            long dob = 123;
            if (check)
            {
                dob = ConvertStringDateTimeToMilisecond.ToMiliSecond(strDate);
            }
            else
            {
                dob = 123;
            }

            //enter gender
            Console.WriteLine("Enter your gender male/female: ");
            string gender1 = Console.ReadLine();
            int gender = 99;
            if (gender1.Equals("male") || gender1.Equals("Male"))
            {
                gender = 1;
            }
            else if (gender1.Equals("female") || gender1.Equals("Female"))
            {
                gender = 0;
            }

            //continue
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter identitynumber: ");
            string identityNumber = Console.ReadLine();
            Console.WriteLine("Enter phone: ");
            string phone = Console.ReadLine();
            var newAccount = new Account(username, password, confirmpassword, firstName, lastName, dob, gender, email,
                identityNumber, phone);
            return newAccount;
        } //get new origin account

        private void ShowErrorMesage(Dictionary<string, string> Errors)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("ATTENTION !!! ERROR MESSAGES:");

            foreach (var mError in Errors)
            {
                Console.WriteLine("- " + mError.Value);
            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Please try again !!!");
            Console.WriteLine("-------------------------------");
            Console.ReadLine();
        } //show login and register error validate
    }
}