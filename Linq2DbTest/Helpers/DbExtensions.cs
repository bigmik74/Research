using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LinqToDB.Data
{
    public static class DbExtensions
    {
        public static bool IsDatabaseExists(this DataConnection dc)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(dc.ConnectionString);
            string databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "";
            string connectionString = builder.ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }

        public static void CreateDatabase(this DataConnection dc)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(dc.ConnectionString);
            string databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "";
            string connectionString = builder.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"CREATE DATABASE {databaseName}";
                command.ExecuteNonQuery();
            }
        }
    }
}
