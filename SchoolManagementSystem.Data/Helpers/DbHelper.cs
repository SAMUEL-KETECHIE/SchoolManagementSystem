using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SchoolManagementSystem.Data.Helpers
{
    public class DbHelper:IDisposable
    {
        //public static DbHelper Instance = new DbHelper();

        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbHelper()
        {

        }

        public NpgsqlCommand CreateCommand(string commandName, string connectionStr = "")
        {
            connectionStr =
                _configuration.GetConnectionString(
                    string.IsNullOrWhiteSpace(connectionStr) ? "SchoolData" : connectionStr);
            var connection = new NpgsqlConnection(connectionStr);
            var command = new NpgsqlCommand(commandName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            return command;
        }

        public IDbConnection CreateConnection(string connectionStr = "")
        {
            connectionStr =
                _configuration.GetConnectionString(
                    string.IsNullOrWhiteSpace(connectionStr) ? "SchoolData" : connectionStr);
            _connection = new NpgsqlConnection(connectionStr);
            return _connection;
        }
        public void Dispose()
        {
            if (!(_connection is null) && _connection.State is ConnectionState.Open)
            {
                _connection.Close();
            }

            _connection?.Dispose();
        }
    }
}
