using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Controller
{
    /*
 * Trong các dự án lớn thường sẽ bổ sung thêm tầng Service
 * Các business phức tạp sẽ được xử lý tại tầng business
 * Các controller chỉ đơn giản làm validate và điều hướng dữ liệu
 * Trong bài toán này chúng ta bỏ qua tầng service và xử lý tất cả trong controller
 */
    public interface IUserController
    {
        Account Register();
        void ShowBankInformation();
        void LoginGeneral();
        void WithDraw(Account accountLogin);
        void Deposit(Account accountLogin);
        void Transfer(Account accountLogin);
        void CheckInformation(Account accountLogin);
        void CheckBalance(Account accountLogin);
        void UpdateInformation(Account accountLogin);
        void ChangePassword(Account accountLogin);
        void LockAccount(Account accountLogin);
        void CheckTransactionHistory(Account accountLogin);
        void ShowAllUserAccount();
        void ReadFileTXT(string link);
    }
}