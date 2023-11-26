using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace _372_project
{
    class DatabaseManager
    {
        private static MySqlConnection connection;

        private static void setConnection()
        {
            connection = new MySqlConnection();
            connection.ConnectionString = "server=hasantuna.com;uid=hasan;pwd=31721Kzh5;database=okul_veri_tabani";
            connection.Open();
        }

        public static DataSet selectCommand(string command)
        {
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            return dataSet;
        }

        private static void closeConnection()
        {
            if(isConnectionReady())
                connection.Close();
        }

        private static bool isConnectionReady()
        {
            return connection != null && connection.State == System.Data.ConnectionState.Open;
        }
    }
}
