using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data;

namespace coding_tracker
{
    internal class CodingController
    {
        string connectionString = getConnectionString();
        internal static string getConnectionString()
        {
            String user = null;
            String password = null;
            String database = null;

            Console.WriteLine("--Database--");
            Console.WriteLine("User: ");

            user = Console.ReadLine();

            password = Utility.GetSecretInput("Password");

            Console.WriteLine("\nDatabase's name: ");

            database = Console.ReadLine();

            return $"server=127.0.0.1;uid={user};pwd={password};database={database}";
        }

        internal List<Coding> Get()
        {
            List<Coding> tableData = new List<Coding>();
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "SELECT * FROM coding";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(
                                new Coding
                                {
                                    Id = reader.GetInt32(0),
                                    Date = reader.GetString(1),
                                    Duration = reader.GetString(2)
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\nNo rows found.\n\n");
                        }
                    }
                }
                Console.WriteLine("\n\n");
            }

            TableVisualization.ShowTable(tableData);

            return tableData;
        }

        internal Coding GetById(int id)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText = $"SELECT * FROM coding Where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        Coding coding = new();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            coding.Id = reader.GetInt32(0);
                            coding.Date = reader.GetString(1);
                            coding.Duration = reader.GetString(2);
                        }

                        Console.WriteLine("\n\n");

                        return coding;
                    };
                }
            }
        }

        internal void Post(Coding coding)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"INSERT INTO coding (date, duration) VALUES ('{coding.Date}', '{coding.Duration}')";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }

        internal void Delete(int id)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"DELETE from coding WHERE Id = '{id}'";
                    tableCmd.ExecuteNonQuery();

                    Console.WriteLine($"\n\nRecord with Id {id} was deleted. \n\n");
                }
            }
        }

        internal void Update(Coding coding)
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = 
                    $@"UPDATE coding SET 
                          Date = '{coding.Date}', 
                          Duration = '{coding.Duration}' 
                       WHERE 
                          Id = {coding.Id}
                      ";

                    tableCmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"\n\nRecord with Id {coding.Id} was updated. \n\n");
        }
    }
}