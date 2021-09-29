using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;
using BankSystemAssignmentCSharp.Model;
using BankSystemAssignmentCSharp.Util;
using SpringHeroBank.utility;

namespace BankSystemAssignmentCSharp.Controller
{
    public class AdminController : IAdminController
    {
        private AdminModel adminModel = new AdminModel();

        public Admin CreateAdmin()
        {
            var check = true;
            var newAcc = new Admin();
            do
            {
                Console.WriteLine("Wellcome to Spring Hero Bank - Registing new Admin Acount");
                newAcc = GetAdminInfo();
                var errors = ValidateInput.IsValidRegisterAdmin(newAcc);
                if (errors.Count > 0)
                {
                    check = true;
                    ShowErrorMessage(errors);
                }
                else if (errors.Count == 0)
                {
                    check = false;
                }
            } while (check);

            var result = adminModel.Save(newAcc);
            if (result)
            {
                Console.WriteLine("saved new account success !!!");
            }
            else
            {
                Console.WriteLine("saving false, please try again !!!!");
            }

            return newAcc;
        } //done

        public void CheckInformation(Admin accountLogin)
        {
            AdminModel adminModel = new AdminModel();
            var checkAccount = adminModel.FindById(accountLogin.Id);
            Console.WriteLine(
                $"{"Id",40}{"|",2}{"Username",15}{"|",2}{"FullName",15}{"|",2}{"Phone",12}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
            Console.WriteLine(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(checkAccount.ToString());
            Console.WriteLine(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public void UpdateAdminAccount(Admin accountLogin)
        {
            var check = true;
            var newAcc = new Admin();
            do
            {
                Console.WriteLine("Change your account's information: ");
                newAcc = GetAdminInfo();
                newAcc.Id = accountLogin.Id;
                newAcc.CreateAt = accountLogin.CreateAt;
                newAcc.DeleteAt = accountLogin.DeleteAt;
                newAcc.Status = accountLogin.Status;
                var Errors = ValidateInput.IsValidRegisterAdmin(newAcc);
                if (Errors.Count > 0)
                {
                    check = true;
                    ShowErrorMessage(Errors);
                }
                else if (Errors.Count == 0)
                {
                    check = false;
                }
            } while (check);

            var result = adminModel.Update(accountLogin.Id, newAcc);
            if (result)
            {
                Console.WriteLine("saved new information success !!!");
            }
            else
            {
                Console.WriteLine("saving false, please try again !!!!");
            }
        }

        public void ChangePassword(Admin accountLogin)
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
                    var result = adminModel.Update(Account1.Id, Account1);
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
        //done

        public void FindUserByAccountNumber()
        {
            AccountModel accountModel = new AccountModel();
            Console.WriteLine("Enter Accountnumber you want to find: ");
            var Accountnumber = Console.ReadLine();
            var result = accountModel.FindById(Accountnumber);
            if (result != null)
            {
                Console.WriteLine(
                    $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(result.ToString());
                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("This account number is not exist, please try again");
            }
        }

        public void FindUserByName()
        {
            AccountModel accountModel = new AccountModel();
            Console.WriteLine("Enter User's FirstName: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter User's LastName: ");
            var lastName = Console.ReadLine();
            var result = accountModel.FindByName(firstName, lastName);
            if (result != null)
            {
                Console.WriteLine(
                    $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var VARIABLE in result)
                {
                    Console.WriteLine(VARIABLE.ToString());
                }

                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }

            if (result == null)
            {
                Console.WriteLine("This user is not exist, please try again");
            }
        }
        //done

        public void FindUserByStatus(int status)
        {
            AccountModel accountModel = new AccountModel();
            var listRerult = accountModel.SearchByStatus(status);
            Console.WriteLine(
                $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var VARIABLE in listRerult)
            {
                Console.WriteLine(VARIABLE.ToString());
            }

            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        } //done

        public void ViewListUserByStatus()
        {
            Console.WriteLine("List account by status you want to see:");
            Console.WriteLine("1 - Active account");
            Console.WriteLine("2 - Inactive account");
            Console.WriteLine("3 - Deleted account");
            Console.WriteLine("Please select 1, 2 or 3 to view list account: ");
            var choise = Int32.Parse(Console.ReadLine());
            AdminController adminController4 = new AdminController();
            switch (choise)
            {
                case 1:
                    FindUserByStatus(1);
                    break;
                case 2:
                    FindUserByStatus(0);
                    break;
                case 3:
                    FindUserByStatus(-1);
                    break;
                default:
                    Console.WriteLine("Please choose from 1 to 3 option");
                    break;
            }
        } //done

        public void ChangeUserStatus()
        {
            Console.WriteLine("Enter Account number you want to change status: ");
            var activeAccountNumber1 = Console.ReadLine();
            AccountModel accountModel = new AccountModel();
            var checkAcc = accountModel.FindById(activeAccountNumber1);
            if (checkAcc != null)
            {
                string strStatus = "null";
                if (checkAcc.Status == 1)
                {
                    strStatus = "active";
                }

                if (checkAcc.Status == 0)
                {
                    strStatus = "inactive";
                }

                if (checkAcc.Status == -1)
                {
                    strStatus = "deleted";
                }

                Console.WriteLine($"Current status is {strStatus}");
                Console.WriteLine("Choose 1 (active), 0 (inactive), or -1 (delete) for new status: ");
                var newStatus = Int32.Parse(Console.ReadLine());
                var result = adminModel.ChangeAccStatus(activeAccountNumber1, newStatus);
                if (result)
                {
                    Console.WriteLine("Account status change success!!!");
                }
                else
                {
                    Console.WriteLine("Change status false, please try again!!!");
                }
            }
            else
            {
                Console.WriteLine("This Accountnumber is not exist, please try again!!!");
            }
        } //done

        public void SearchUserByPhone()
        {
            AccountModel accountModel = new AccountModel();
            Console.WriteLine("Enter Phone number you want to find: ");
            var phone = Console.ReadLine();
            var result = accountModel.SearchByPhone(phone);
            if (result != null)
            {
                Console.WriteLine(
                    $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var VARIABLE in result)
                {
                    Console.WriteLine(VARIABLE.ToString());
                }

                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("This phone number is not exist, please try again");
            }
        } //done

        public void SearchUserByIndentityNumber()
        {
            AccountModel accountModel = new AccountModel();
            Console.WriteLine("Enter IndentityNumber you want to find: ");
            var IndentityNumber = Console.ReadLine();
            var result = accountModel.SearchByIndentityNumber(IndentityNumber);
            if (result != null)
            {
                Console.WriteLine(
                    $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var VARIABLE in result)
                {
                    Console.WriteLine(VARIABLE.ToString());
                }

                Console.WriteLine(
                    "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("This IndentityNumber is not exist, please try again");
            }
        } //done

        public void SearchUserByTransactionHistory()
        {
            TransactionModel transactionModel = new TransactionModel();
            Console.WriteLine("Enter account number you want to check:");
            var accountCheck = Console.ReadLine();
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

            var listResult =
                transactionModel.FindTransactionHistoryByAccountNumber(accountCheck, startTime, endTime);
            if (listResult != null)
            {
                Console.WriteLine($"The transaction history from {startTime} to {endTime}: ");
                Console.WriteLine(
                    $"{"ID",40}{" |",2}{"SenderAccountNumber",40}{" |",2}{"ReceiverAccountNumber",40}{" |",2}{"Type",8}{" |",2}{"Amount",10}{" |",2}{"Message",28}{" |",2}{"CreateAt",13}{" |",2}{"Status",7}");
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var VARIABLE in listResult)
                {
                    Console.WriteLine(VARIABLE.ToString());
                }

                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }

        public void ViewAllTransactionHistory()
        {
            TransactionModel transactionModel = new TransactionModel();
            var result = transactionModel.FindAllTransactionHistory();
            if (result == null)
            {
                Console.WriteLine("TransactionHistory list is empty");
            }
            else
            {
                Console.WriteLine(
                    $"{"ID",40}{" |",2}{"SenderAccountNumber",40}{" |",2}{"ReceiverAccountNumber",40}{" |",2}{"Type",8}{" |",2}{"Amount",10}{" |",2}{"Message",28}{" |",2}{"CreateAt",13}{" |",2}{"Status",7}");
                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var VARIABLE in result)
                {
                    Console.WriteLine(VARIABLE.ToString());
                }

                Console.WriteLine(
                    "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }

        private Admin GetAdminInfo()
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter confirmpassword: ");
            string confirmPassword = Console.ReadLine();
            Console.WriteLine("Enter fullname: ");
            string fullName = Console.ReadLine();
            Console.WriteLine("Enter phone: ");
            string phone = Console.ReadLine();
            var newAccountAdmin = new Admin(username, password, confirmPassword, fullName, phone);
            return newAccountAdmin;
        } //get new origin accountAdmin

        private void ShowErrorMessage(Dictionary<string, string> Errors)
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