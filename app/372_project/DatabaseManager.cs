using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using MySqlConnector;

namespace _372_project
{
    class DatabaseManager
    {
        private static MySqlConnection? connection;

        private static void setConnection()
        {
            connection = new MySqlConnection();
            connection.ConnectionString = "server=<domain>;uid=<username>;pwd=<pass>;database=<database_name>";
            connection.Open();
        }

        public static DataSet selectCommand(string command)
        {
            Debug.WriteLine(command);
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

            foreach (DataColumn attribute in dataSet.Tables[0].Columns)
            {
                string? name;
                if(Constants.ATTR_TO_NAME_DICT.TryGetValue(attribute.ColumnName, out name))
                {
                    attribute.ColumnName = name;
                }
            }

            return dataSet;
        }

        public static int insertCommand(string command)
        {
            Debug.WriteLine(command);
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);
            return cmd.ExecuteNonQuery();
        }

        public static int updateCommand(string command)
        {
            Debug.WriteLine(command);
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);
            return cmd.ExecuteNonQuery();
        }

        public static int deleteCommand(string command)
        {
            Debug.WriteLine(command);
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);
            return cmd.ExecuteNonQuery();
        }

        public static int commit()
        {
            Debug.WriteLine("COMMIT;");
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand("COMMIT;", connection);
            int rowCount = cmd.ExecuteNonQuery();
            closeConnection();
            return rowCount;
        }

        public static int rollback()
        {
            Debug.WriteLine("ROLLBACK;");
            if (!isConnectionReady())
                setConnection();

            MySqlCommand cmd = new MySqlCommand("ROLLBACK;", connection);
            int rowCount = cmd.ExecuteNonQuery();
            closeConnection();
            return rowCount;
        }

        private static void closeConnection()
        {
            if(isConnectionReady())
                connection.Close();
        }

        private static bool isConnectionReady()
        {
            return (connection != null && connection.State == System.Data.ConnectionState.Open);
        }
    }
}
