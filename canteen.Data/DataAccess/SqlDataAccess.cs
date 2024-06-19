using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace canteen.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection CreateConnection(string connectionId = "conn")
        {
            var connectionString = _config.GetConnectionString(connectionId);
            return new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "conn")
        {
            using IDbConnection connection = CreateConnection(connectionId);
            try
            {
                return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                // Handle and log the exception appropriately.
                throw new ApplicationException("Error executing GetData.", ex);
            }
        }

        public async Task SaveData<T>(string spName, T parameters, string connectionId = "conn")
        {
            using IDbConnection connection = CreateConnection(connectionId);
            try
            {
                await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                // Handle and log the exception appropriately.
                throw new ApplicationException("Error executing SaveData.", ex);
            }
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionId = "conn")
        {
            using IDbConnection connection = CreateConnection(connectionId);
            try
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
            catch (Exception ex)
            {
                // Handle and log the exception appropriately.
                throw new ApplicationException("Error executing LoadData.", ex);
            }
        }

        Task<T> ISqlDataAccess.LoadData<T>(string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        // Other methods for data access...

        // You can similarly add methods for updating and deleting records.
    }
}