using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace T057_DatabaseConnection.Data
{
	public class DataContextDapper(IConfiguration config)
	{
		private readonly IConfiguration _config = config;

		public IEnumerable<T> LoadData<T>(string sql)
		{
			IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			return dbConnection.Query<T>(sql);
		}

		public T LoadDataSingle<T>(string sql)
		{
			IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			return dbConnection.QuerySingle<T>(sql);
		}

		public bool ExecuteSql(string sql)
		{
			IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			return dbConnection.Execute(sql) > 0;
		}

		public int ExecuteSqlWithRowCount(string sql)
		{
			IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			return dbConnection.Execute(sql);
		}
	}
}
