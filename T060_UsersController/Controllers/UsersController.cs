using Microsoft.AspNetCore.Mvc;
using T060_UsersController.Data;
using T060_UsersController.Models;

namespace T060_UsersController.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextDapper _dapper = new(config);

		[HttpGet("GetUsers")]
		public IEnumerable<UserModel> GetUsers()
		{
			string sql = @"
			SELECT
				[UserId],
				[FirstName],
				[LastName],
				[Email],
				[Gender],
				[Active]
			FROM [TutorialAppSchema].[Users]
			";

			IEnumerable<UserModel> users = _dapper.LoadData<UserModel>(sql);

			return users;
		}

		[HttpGet("GetUser/{userId}")]
		public UserModel GetUser(int userId)
		{
			string sql = @$"
			SELECT
				[UserId],
				[FirstName],
				[LastName],
				[Email],
				[Gender],
				[Active]
			FROM [TutorialAppSchema].[Users]
			WHERE [UserId] = {userId}
			";

			UserModel user = _dapper.LoadDataSingle<UserModel>(sql);

			return user;
		}
	}
}
