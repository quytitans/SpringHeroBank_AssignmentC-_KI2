using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace QuanLySinhVienVoiCSharp.Util
{
    public class ConnectionHelperCSharp
    {
        private static MySqlConnection cnn;
        private static readonly string Server = "127.0.0.1";
        private static readonly string Uid = "root";
        private static readonly string Pwd = "";
        private static readonly string Database = "banksystem_asignment_csharp";

        public static MySqlConnection GetConnection()
        {
            string myConnectionString = $"server={Server};uid={Uid};pwd={Pwd};database={Database};SslMode=none";
            if (cnn == null)
            {
                try
                {
                    cnn = new MySqlConnection(myConnectionString);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return cnn;
        }
    }
}