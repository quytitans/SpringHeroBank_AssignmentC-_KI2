using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Util
{
    public static class ExtendMethodGeneral
    {
        public static void Display(this List<TransactionHistory> listTransaction)
        {
            Console.WriteLine(
                $"{"ID",40}{" |",2}{"SenderAccountNumber",40}{" |",2}{"ReceiverAccountNumber",40}{" |",2}{"Type",8}{" |",2}{"Amount",10}{" |",2}{"Message",28}{" |",2}{"CreateAt",13}{" |",2}{"Status",7}");
            Console.WriteLine(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var VARIABLE in listTransaction)
            {
                Console.WriteLine(VARIABLE.ToString());
            }

            Console.WriteLine(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public static void Display(this List<Account> listAccount)
        {
            Console.WriteLine(
                $"{"AccountNumber",40}{"|",2}{"Balance",15}{"|",2}{"Username",15}{"|",2}{"Lock",5}{"|",2}{"FirstName",10}{"|",2}{"LastName",10}{"|",2}{"IdentityNumber",18}{"|",2}{"Phone",17}{"|",2}{"CreateAt",15}{"|",2}{"UpdateAt",15}{"|",2}{"DeleteAt",15}{"|",2}{"Status",5}");
            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var VARIABLE in listAccount)
            {
                Console.WriteLine(VARIABLE.ToString());
            }

            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
    }
}