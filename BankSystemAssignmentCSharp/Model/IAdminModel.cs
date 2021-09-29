using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Model
{
    public interface IAdminModel
    {
        bool Save(Admin account);
        bool Update(string id, Admin updateAccount);
        bool Delete(string id);
        Admin FindById(string id);
        Admin FindByUsername(string id);
        List<Admin> FindAll();
        bool ChangeAccLockTransaction(string usernameNumber, int newLockTransaction);
        bool ChangeAccStatus(string usernameNumber, int newStatus);
    }
}