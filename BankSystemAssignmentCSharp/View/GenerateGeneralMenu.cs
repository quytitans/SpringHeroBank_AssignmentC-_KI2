using System;
using System.Runtime.InteropServices;
using BankSystemAssignmentCSharp.Controller;
using BankSystemAssignmentCSharp.Entity;
using BankSystemAssignmentCSharp.Util;

namespace BankSystemAssignmentCSharp.View
{
    public class GenerateGeneralMenu
    {
        public static void GenerateMenu()
        {
            UserController userController = new UserController();
            AdminController adminController = new AdminController();
            while (true)
            {
                Console.WriteLine("--------Well come to SHB bank--------");
                Console.WriteLine("please select: ");
                Console.WriteLine("1 - View SHB bank information");
                Console.WriteLine("2 - Customer incentive programs");
                Console.WriteLine("3 - Login");//done
                Console.WriteLine("4 - Register New Customer Account"); //done
                Console.WriteLine("5 - Register New Admin Account");//done
                Console.WriteLine("0 - Exit"); //done
                Console.WriteLine("--------------------------------------");
                var choice = getChoice();
                switch (@choice)
                {
                    case 1:
                        Console.WriteLine("View SHB bank information");
                        UserController userController1 = new UserController();
                        const string filePath = "../../Storage/SHBbank.txt";
                        userController1.ReadFileTXT(filePath);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Customer incentive programs");
                        UserController userController2 = new UserController();
                        const string filePath1 = "../../Storage/SHBnews.txt";
                        userController2.ReadFileTXT(filePath1);
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("--------------------LOGIN--------------------");
                        userController.LoginGeneral();
                        break;
                    case 4:
                        Console.WriteLine("Register New Customer Account");
                        var result = userController.Register();
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Register New Admin Account");
                        var result1 = adminController.CreateAdmin();
                        Console.ReadLine();
                        break;
                }

                if (choice != 0) continue;
                Console.WriteLine("Closed program, see you again !!!");
                break;
            }
        }

        public static void GenerateMenuAdminMode(Admin accountLogin)
        {
            while (true)
            {
                Console.WriteLine("--------Well come to SHB bank--------");
                Console.WriteLine("--------------ADMIN MODE-------------");
                Console.WriteLine($"Welcom Admin {accountLogin.FullName} comeback");
                Console.WriteLine("please select: ");
                Console.WriteLine("1 - Your detail information"); //done
                Console.WriteLine("2 - Change user account status"); //done
                Console.WriteLine("3 - Open/Lock user transaction"); //done
                Console.WriteLine("4 - Show list account (Active, Inactive/Waiting Approve, Deleted)"); // PHAN TRANG
                Console.WriteLine("5 - Find User info by ACCOUNT's NUMBER"); //done
                Console.WriteLine("6 - Find User info by USER's NAME"); //done
                Console.WriteLine("7 - Find User info by ACCOUNT's PHONE"); //done
                Console.WriteLine("8 - Check User's transaction history"); // PHAN TRANG
                Console.WriteLine("9 - Add new User Account"); //done
                Console.WriteLine("10 - Show users list"); // PHAN TRANG
                Console.WriteLine("11 - Show TransactionHistory list"); // PHAN TRANG
                Console.WriteLine("12 - Change Your Information"); //done
                Console.WriteLine("13 - Change Your Password"); //done
                Console.WriteLine("0 - Back");
                Console.WriteLine("--------------------------------------");
                var choice = getChoice();
                switch (@choice)
                {
                    case 1:
                        Console.WriteLine("Your detail information: ");
                        AdminController adminController = new AdminController();
                        adminController.CheckInformation(accountLogin);
                        Console.ReadLine();
                        break;
                    case 2:
                        AdminController adminController3 = new AdminController();
                        adminController3.ChangeUserStatus();
                        Console.ReadLine();
                        break;
                    case 3:
                        AdminController adminController16 = new AdminController();
                        adminController16.ChangeLockTransaction();
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("View list account by status");
                        AdminController adminController4 = new AdminController();
                        adminController4.ViewListUserByStatus();
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Find User info by ACCOUNT's NUMBER");
                        AdminController adminController5 = new AdminController();
                        adminController5.FindUserByAccountNumber();
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.WriteLine("Find User info by USER's NAME");
                        AdminController adminController6 = new AdminController();
                        adminController6.FindUserByName();
                        Console.ReadLine();
                        break;
                    case 7:
                        Console.WriteLine("Find User info by ACCOUNT's PHONE");
                        AdminController adminController7 = new AdminController();
                        adminController7.SearchUserByPhone();
                        Console.ReadLine();
                        break;
                    case 8:
                        Console.WriteLine("Check User's transaction history");
                        AdminController adminController8 = new AdminController();
                        adminController8.SearchUserByTransactionHistory();
                        Console.ReadLine();
                        break;
                    case 9:
                        Console.WriteLine("Add new User Account");
                        UserController userController = new UserController();
                        userController.Register();
                        Console.ReadLine();
                        break;
                    case 10:
                        Console.WriteLine("Show users list");
                        UserController userController1 = new UserController();
                        userController1.ShowAllUserAccount();
                        Console.ReadLine();
                        break;
                    case 11:
                        Console.WriteLine("Show TransactionHistory list");
                        AdminController adminController11 = new AdminController();
                        adminController11.ViewAllTransactionHistory();
                        Console.ReadLine();
                        break;
                    case 12:
                        Console.WriteLine("Change Your Information");
                        AdminController adminController12 = new AdminController();
                        adminController12.UpdateAdminAccount(accountLogin);
                        Console.ReadLine();
                        break;
                    
                    
                    case 13:
                        Console.WriteLine("Change Your Password");
                        AdminController adminController13 = new AdminController();
                        adminController13.ChangePassword(accountLogin);
                        Console.ReadLine();
                        break;
                }

                if (choice == 0)
                {
                    break;
                }
            }
        }

        public static void GenerateMenuUserMode(Account accountLogin)
        {
            while (true)
            {
                Console.WriteLine("--------Well come to SHB bank--------");
                Console.WriteLine("--------------USER MODE-------------");
                Console.WriteLine($"Welcome user {accountLogin.FirstName} {accountLogin.LastName} comeback");
                Console.WriteLine("please select: ");
                Console.WriteLine("1 - Show account information"); //done
                Console.WriteLine("2 - Show Balance"); //done 
                Console.WriteLine("3 - Deposit"); //done
                Console.WriteLine("4 - Withdraw"); //done
                Console.WriteLine("5 - Transfer"); //done
                Console.WriteLine("6 - Check transaction history"); // PHAN TRANG
                Console.WriteLine("7 - Lock/Unlock account transaction"); //done
                Console.WriteLine("8 - Edit account's information"); //done
                Console.WriteLine("9 - Change password"); //done
                Console.WriteLine("0 - Back"); //done
                Console.WriteLine("--------------------------------------");
                var choice = getChoice();
                switch (@choice)
                {
                    case 1:
                        Console.WriteLine("Detail information:");
                        UserController userController4 = new UserController();
                        userController4.CheckInformation(accountLogin);
                        Console.ReadLine();
                        break;
                    case 2:
                        UserController userController5 = new UserController();
                        userController5.CheckBalance(accountLogin);
                        Console.ReadLine();
                        break;
                    case 3:
                        UserController userController = new UserController();
                        userController.Deposit(accountLogin);
                        Console.ReadLine();
                        break;
                    case 4:
                        UserController userController1 = new UserController();
                        userController1.WithDraw(accountLogin);
                        Console.ReadLine();
                        break;
                    case 5:
                        UserController userController2 = new UserController();
                        userController2.Transfer(accountLogin);
                        Console.ReadLine();
                        break;
                    case 6:
                        UserController userController6 = new UserController();
                        userController6.CheckTransactionHistory(accountLogin);
                        Console.ReadLine();
                        break;
                    case 7:
                        UserController userController7 = new UserController();
                        userController7.LockAccount(accountLogin);
                        Console.ReadLine();
                        break;
                    case 8:
                        UserController userController8 = new UserController();
                        userController8.UpdateInformation(accountLogin);
                        Console.ReadLine();
                        break;
                    case 9:
                        UserController userController9 = new UserController();
                        userController9.ChangePassword(accountLogin);
                        Console.ReadLine();
                        break;
                }

                if (choice == 0)
                {
                    break;
                }
            }
        }

        public static int getChoice()
        {
            string strChoice = "abc";
            int choice = 100;
            while (!CheckintergerValue.IsIntergerValue(strChoice))
            {
                Console.WriteLine("Enter your choice: ");
                strChoice = Console.ReadLine();
                    
            }
            if (CheckintergerValue.IsIntergerValue(strChoice))
            {
                return choice = Int32.Parse(strChoice);  
            }
            return default;
        }
    }
}