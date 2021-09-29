using System;
using System.Collections.Generic;
using BankSystemAssignmentCSharp.Entity;

namespace BankSystemAssignmentCSharp.Model
{
    public interface ITransactionModel
    {
        bool saveTransactionHistory (TransactionHistory transactionHistory);
        // sao kê
        List<TransactionHistory> FindTransactionHistoryByAccountNumber(
            string accountNumber,
            string startTime,
            string endTime);
        List<TransactionHistory> FindAllTransactionHistory();

        // thực hiện gửi tiền
        TransactionHistory Deposit(string accountNumber, double amount);

        // thực hiện rút tiền
        TransactionHistory Withdraw(string accountNumber, double amount);

        // thực hiện chuyển tiền
        TransactionHistory Transfer(
            string senderAccountNumber,
            string receiverAccountNumber,
            double amount,
            string message);
    }
}