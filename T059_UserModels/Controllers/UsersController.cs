using Microsoft.AspNetCore.Mvc;
using T059_UserModels.Data;

namespace T059_UserModels.Controllers
{
	public class UsersController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextDapper _dapper = new(config);

		[HttpGet("TestConnection")]
		public DateTime TestConnection()
		{
			return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
		}
		public string[] GetUsers(string testValue)
		{
			string[] responseArray = ["test1", "test2", testValue];
			return responseArray;
		}
	}
}
