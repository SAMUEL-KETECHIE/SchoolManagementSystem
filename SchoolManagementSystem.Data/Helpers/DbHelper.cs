using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SchoolManagementSystem.Data.Helpers
{
    public class DbHelper
    {
        private readonly IConfiguration _configuration;
        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}
