using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Controller
{
    public interface IAdminController
    {
        Admin CreateAdmin();
        void CheckInformation(Admin accountLogin);
        void UpdateAdminAccount(Admin accountLogin);
        void ChangePassword(Admin accountLogin);
        void FindUserByAccountNumber();
        void FindUserByName();
        void FindUserByStatus(int status);
        void ViewListUserByStatus();
        void ChangeUserStatus();
        public void ChangeLockTransaction();
        void SearchUserByPhone();
        void SearchUserByIndentityNumber();
        void SearchUserByTransactionHistory();
        void ViewAllTransactionHistory();
    }
}