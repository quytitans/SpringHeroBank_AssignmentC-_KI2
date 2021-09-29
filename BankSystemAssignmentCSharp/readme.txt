### Chức năng
- Khách hàng có thể đăng ký tài khoản hệ thống
- Khách hàng có thể đăng nhập hệ thống
- Khách hàng có thể xem thông tin giới thiệu về ngân hàng
- Khách hàng có thể gửi phản hồi, góp ý cho ngân hàng
- Khách hàng (sau khi đăng nhập) có thể gửi tiền vào ngân hàng
- Khách hàng (sau khi đăng nhập) có thể rút tiền khỏi ngân hàng
- Khách hàng (sau khi đăng nhập) có thể xem thông tin tài khoản
- Khách hàng (sau khi đăng nhập) có thể chỉnh sửa thông tin tài khoản
- Khách hàng (sau khi đăng nhập) có thể khóa/mở tài khoản
- Khách hàng (sau khi đăng nhập) có thể thực hiện chuyển tiền trong ngân hàng
- Khách hàng (sau khi đăng nhập) có thể tra cứu lịch sử giao dịch trong tài khoản
- (Option) Khách hàng (sau khi đăng nhập) có thể đóng tài khoản
- Admin có thể đăng nhập hệ thống với quyền admin
- Admin có thể duyệt/từ chối các thông tin tài khoản đăng ký
- Admin có thể tạo sửa xóa các thông tin tài khoản admin khác
- Admin có thể chuyển trạng thái tài khoản của khách hàng (khóa, mở)
- Admin có thể tìm kiếm thông tin tài khoản khách hàng dưa theo: số tài khoản 
- Admin có thể tìm kiếm thông tin tài khoản khách hàng dưa theo: số điện thoại 
- Admin có thể tìm kiếm thông tin tài khoản khách hàng dưa theo: số cmnd, cccd 
- Admin có thể kiểm tra lịch sử giao dịch dựa theo số tài khoản

### Usecase
- Role: Guest, User, Admin

###Entity
- Admin:
    - Id (string): số tài khoản
    - Username (string): tên đăng nhập
    - PasswordHash (string): password đã mã hóa
    - Salt (string): muối
    - FullName (string): tên
    - Phone (string): số điện thoại
    - CreateAt (Datetime): ngày mở tài khoản
    - UpdateAt (Datetime): ngày update tài khoản
    - DeleteAt (Datetime): ngày xóa tài khoản
    - Status (int): trạng thái tài khoản, //1 là active, 2 là khóa, -1 là deleted, 0 : chuakichhoat
    
- Account (có thể tách ra bằng 1 bảng khác chứa thông tin các nhân):
    - AccountNumber (string): số tài khoản
    - Type (int): cá nhân doanh nghiệp
    - Balance (double): số dư tài khoản
    - Username (string): tài khoản ebanking
    - PasswordHash (string): password đã mã hóa
    - Salt (string): muối
    - LockTransaction (bool): 1 khóa 0  không khóa   vẫn được đăng nhập
    
    - FirstName (string): Tên
    - LastName (string): Họ
    - Dob (Datetime): ngày sinh
    - Gender (int): giới tính
    - Email (string): hòm thư điện tử
    - IdentityNumber (string): căn cước hoặc cmd
    - Phone (string): số điện thoại
    
    - CreateAt (Datetime): ngày mở tài khoản
    - UpdateAt (Datetime): ngày update tài khoản
    - DeleteAt (Datetime): ngày xóa tài khoản
    - Status (int): trạng thái tài khoản, 1 là active, 0 là khóa, -1 là deleted
    
- TransactionHistory
    - Id (string): mã giao dịch
    - SenderAccountNumber (string FK from Account): ai gửi đến
    - ReceiverAccountNumber (string FK from Account): ai nhận tiền
    - Type (int): withdraw (1), deposit (2), transfer (3)
    - Amount (double): số tiền giao dịch
    - Message (string): nội dung giao dịch
    
    - CreateAt (Datetime): ngày mở tài khoản
    - UpdateAt (Datetime): ngày update tài khoản
    - DeleteAt (Datetime): ngày xóa tài khoản
    - Status (int): trạng thái giao dịch 1 là thành công, 2 là đang xử lý, 3 là thất bại
    - (Optinal, có thể thay thế bằng file txt) BankInfomation