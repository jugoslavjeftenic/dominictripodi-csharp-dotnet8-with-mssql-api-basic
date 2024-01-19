using Microsoft.AspNetCore.Mvc;
using T057_DatabaseConnection.Data;

namespace T057_DatabaseConnection.Controllers
{
	[ApiController]
	[Route("[controller]")]
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
