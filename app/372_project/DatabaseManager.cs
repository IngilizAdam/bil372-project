using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace _372_project
{
    class DatabaseManager
    {
        private static MySqlConnection connection;

        public static MySqlConnection getConnection()
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=hasantuna.com;uid=hasan;pwd=31721Kzh5;database=odev2";
            connection.Open();
            return connection;
        }



        public static void closeConnection()
        {
            if(isConnectionReady())
                connection.Close();
        }

        public static bool isConnectionReady()
        {
            return connection != null && connection.State == System.Data.ConnectionState.Open;
        }
    }
}
