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
        /*
         * - Yêu cầu người dùng nhập thông tin tài khoản
         * - Kiểm tra thông tin tài khoản: check thông tin input, check tồn tại
         * - Tạo muối, mã hóa password, khởi tạo 1 số thông tin tự động: ngày tạo, ngày update, status
         * - Lưu vào database thông qua model
         */
        Account Register();

        // Xem thông tin ngân hàng, có thể đọc từ file txt hoặc hard code
        void ShowBankInformation();

        /*
         * Đăng nhập vào hệ thống
         * - Yêu cầu người dùng nhập thông tin  username và password
         * - Kiểm tra sự tồn tại của tài khoản:....
         * - Trả về thông tin tài khoản nếu đăng nhập thành công
         */
        void LoginGeneral();
        void WithDraw(Account accountLogin); // thực hiện rút tiền
        void Deposit(Account accountLogin); // thực hiện gửi tiền
        void Transfer(Account accountLogin); // thực hiện chuyển tiền
        void CheckInformation(Account accountLogin); // kiem tra thong tin tai khoan chi tiet
        void CheckBalance(Account accountLogin); // kiem tra so du tai khoan hien tai
        void UpdateInformation(Account accountLogin);
        void ChangePassword(Account accountLogin);
        void LockAccount(Account accountLogin);
        void CheckTransactionHistory(Account accountLogin);
        void ShowAllUserAccount();
    }
}