using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using T069_AssignmentDapper.Data;
using T069_AssignmentDapper.Models;

namespace T069_AssignmentDapper.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserSalaryController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextDapper _dapper = new(config);

		// Create
		[HttpPost("AddUserSalary")]
		public IActionResult AddUserSalary(UserSalaryModel userSalary)
		{
			string sql = @$"
			INSERT INTO [TutorialAppSchema].[UserSalary] (
				[UserId],
				[Salary],
				[AvgSalary]
			) VALUES (
				'{userSalary.UserId}',
				'{userSalary.Salary.ToString("0.00", CultureInfo.InvariantCulture)}',
				'{userSalary.AvgSalary.ToString("0.00", CultureInfo.InvariantCulture)}'
			)";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Add user's salary.");
		}

		// Read - all
		[HttpGet("GetUsersSalary")]
		public IEnumerable<UserSalaryModel> GetUsersSalary()
		{
			string sql = @"
			SELECT
				[UserId],
				[Salary],
				[AvgSalary]
			FROM [TutorialAppSchema].[UserSalary]
			";

			IEnumerable<UserSalaryModel> usersSalary = _dapper.LoadData<UserSalaryModel>(sql);

			return usersSalary;
		}

		// Read - byId
		[HttpGet("GetUserSalary/{userId}")]
		public IEnumerable<UserSalaryModel> GetUserSalary(int userId)
		{
			string sql = @$"
			SELECT
				[UserId],
				[Salary],
				[AvgSalary]
			FROM [TutorialAppSchema].[UserSalary]
			WHERE [UserId] = {userId}
			";

			IEnumerable<UserSalaryModel> userSalarys = _dapper.LoadData<UserSalaryModel>(sql);

			return userSalarys;
		}

		// Update
		[HttpPut("EditUserSalary")]
		public IActionResult EditUserSalary(UserSalaryModel userSalary)
		{
			string sql = @$"
			UPDATE [TutorialAppSchema].[UserSalary] SET
				[Salary] = '{userSalary.Salary.ToString("0.00", CultureInfo.InvariantCulture)}',
				[AvgSalary] = '{userSalary.AvgSalary.ToString("0.00", CultureInfo.InvariantCulture)}'
			WHERE UserId = {userSalary.UserId}
			";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Update user's salary.");
		}

		// Delete
		[HttpDelete("DeleteUserSalarys/{userId}")]
		public IActionResult DeleteUserSalarys(int userId)
		{
			string sql = @$"
			DELETE
			FROM [TutorialAppSchema].[UserSalary]
			WHERE [UserId] = {userId}
			";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Delete user's salary.");
		}
	}
}
