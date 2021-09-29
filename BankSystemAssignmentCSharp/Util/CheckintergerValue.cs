using System;

namespace BankSystemAssignmentCSharp.Util
{
    public class CheckintergerValue
    {
        public static bool IsIntergerValue(string checkValue)
        {
            int i;
            var result = int.TryParse(checkValue, out i);
            return result;
        }
    }
}