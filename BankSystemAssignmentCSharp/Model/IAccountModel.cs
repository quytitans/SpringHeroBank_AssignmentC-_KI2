using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Model
{
    public interface IAccountModel
    {
        bool Save(Account account);
        bool Update(string id, Account updateAccount);
        bool Delete(string id);
        Account FindById(string id);
        List<Account> FindByName(string firstName, string lastName);

        Account FindByUsername(string username);

        List<Account> FindAll(int offset, int limit);
        int CountAllAccount();
        
        List<Account> SearchByPhone(string keyword);
        
        List<Account> SearchByIndentityNumber(string keyword);
        
        List<Account> SearchByStatus(int status, int offset, int limit);
        int CountUserByStatus(int status);
       
    }
}