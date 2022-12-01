using System;
using MySql.Data;

namespace coding_tracker
{
    internal class DatabaseManager
    {
        internal void CreateTable(string connectionString)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding (
                        Id INTEGER PRIMARY KEY AUTO_INCREMENT, 
                        Date TEXT, 
                        Duration TEXT
                    )";

                tableCmd.ExecuteNonQuery();

                }
            }
        }
    }
}