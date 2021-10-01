using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Model
{
    public interface ITransactionModel
    {
        bool saveTransactionHistory(TransactionHistory transactionHistory);

        // sao kê
        List<TransactionHistory> FindTransactionHistoryByAccountNumber(
            string accountNumber,
            string startTime,
            string endTime,
            int offset,
            int limit);

        TransactionHistory FinByID(string ID);
        List<TransactionHistory> FindAllTransactionHistory(int offset, int limit);
        int CountNumberOffTransactionHistory();
        int CountNumberOffTransactionHistory(string accountNumber, string startTime, string endTime);


        // thực hiện gửi tiền
        TransactionHistory Deposit(string accountNumber, double amount);
        TransactionHistory Withdraw(string accountNumber, double amount);

        TransactionHistory Transfer(
            string senderAccountNumber,
            string receiverAccountNumber,
            double amount,
            string message);
    }
}