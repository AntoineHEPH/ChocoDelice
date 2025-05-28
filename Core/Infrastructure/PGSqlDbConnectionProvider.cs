using System.Data;
using Npgsql;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Infrastructure
{
    internal class PGSqlDbConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;
        public PGSqlDbConnectionProvider(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }
        public async Task<IDbConnection> CreateConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
