using System;
using System.Diagnostics;
using BankSystemAssignmentCSharp.Model;
using BankSystemAssignmentCSharp.Util;

namespace BankSystemAssignmentCSharp.Entity
{
    public class TransactionHistory
    {
        private long _createAt;
        private long _deleteAt;
        private long _updateAt;
        public string ID { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public int Type { get; set; } //(1) withdraw, deposit (2), transfer (3)
        public double Amount { get; set; }
        public string Message { get; set; }

        public long CreateAt
        {
            get => _createAt;
            set => _createAt = value;
        }

        public long UpdateAt
        {
            get => _updateAt;
            set => _updateAt = value;
        }

        public long DeleteAt
        {
            get => _deleteAt;
            set => _deleteAt = value;
        }

        public int Status { get; set; } // 1 successes, 2 process, 3 false

        //convert milisecond to string date time format
        public string StrCreateAt()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_createAt);
        }

        public string StrUpdateAt()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_updateAt);
        }

        public string StrDeleteAt()
        {
            return ConvertMilisecondToStringDateTime.ConvertToStringDate(_deleteAt);
        }

        public string strType()
        {
            if (Type == 1)
            {
                return "Withdaw";
            }

            if (Type == 2)
            {
                return "Deposit";
            }

            if (Type == 3)
            {
                return "Transfer";
            }
            else
            {
                return "Null";
            }
        }

        public string strStatus()
        {
            if (Status == 1)
            {
                return "Success";
            }

            if (Status == 2)
            {
                return "Process";
            }

            if (Status == 3)
            {
                return "False";
            }
            else
            {
                return "Null";
            }
        }
        //end convert block

        public TransactionHistory()
        {
        }

        public TransactionHistory(string senderAccountNumber, string receiverAccountNumber, int type, double amount,
            string message)
        {
            TransactionHistory check;
            do
            {
                ID = Guid.NewGuid().ToString();
                TransactionModel transactionModel = new TransactionModel();
                check = transactionModel.FinByID(ID);
            } while (check != null);

            SenderAccountNumber = senderAccountNumber;
            ReceiverAccountNumber = receiverAccountNumber;
            Type = type;
            Amount = amount;
            Message = message;
            CreateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            UpdateAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            DeleteAt = 0;
            Status = 2;
        }

        public override string ToString()
        {
            return
                $"{ID,40}{" |",2}{SenderAccountNumber,40}{" |",2}{ReceiverAccountNumber,40}{" |",2}{strType(),8}{" |",2}{Amount,10}{" |",2}{Message,28}{" |",2}{StrCreateAt(),13}{" |",2}{strStatus(),7}";
        }
    }
}