using System;
using System.Globalization;

namespace BankSystemAssignmentCSharp.Util
{
    public class ConvertMilisecondToStringDateTime
    {
        public static string ConvertToStringDate(long millisecondsInputValue)
        {
            string format = "dd/MM/yyyy";
            if (millisecondsInputValue ==0 )
            {
                return "Null";
            }
            var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            var time = posixTime.AddMilliseconds(millisecondsInputValue);
            return time.ToShortDateString();
        }
    }
}