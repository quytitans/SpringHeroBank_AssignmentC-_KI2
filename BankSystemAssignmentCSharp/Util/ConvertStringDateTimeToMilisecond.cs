using System;

namespace BankSystemAssignmentCSharp.Util
{
    public class ConvertStringDateTimeToMilisecond
    {
        public static long ToMiliSecond(string strDate)
        {
            DateTime dateTime = default;
            string format = "dd/MM/yyyy";
            try 
            {
                dateTime = DateTime.ParseExact(strDate, format, null );
            }
            catch (FormatException e) 
            {
                Console.WriteLine(e.Message);
            }
            long mili = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
            
            return mili;
        }
    }
}