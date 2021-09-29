using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Model
{
    public interface IAccountModel
    {
        bool Save(Account account); // đăng ký tài khoản
        bool Update(string id, Account updateAccount); // thay đổi tài khoản, lock, active,...
        bool Delete(string id); // đóng tài khoản
        Account FindById(string id); // tìm tài khoản theo số tài khoản
        List<Account> FindByName(string firstName, string lastName); // tìm tài khoản theo số tài khoản

        Account FindByUsername(string username); // tìm tài khoản theo số tài khoản

        List<Account> FindAll(); // lấy danh sách kèm phân trang

        // lọc theo số điện thoại, có thể nhập 1 phần điện thoại để search
        List<Account> SearchByPhone(string keyword);

        // lọc theo cmnd, cccd
        List<Account> SearchByIndentityNumber(string keyword);

        //loc theo Status
        List<Account> SearchByStatus(int status);
       
    }
}