using Microsoft.AspNetCore.Mvc;
using T069_AssignmentDapper.Data;
using T069_AssignmentDapper.Models;

namespace T069_AssignmentDapper.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserJobInfoController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextDapper _dapper = new(config);

		// Create
		[HttpPost("AddUserJobInfo")]
		public IActionResult AddUserJobInfo(UserJobInfoModel userJobInfo)
		{
			string sql = @$"
			INSERT INTO [TutorialAppSchema].[UserJobInfo] (
				[UserId],
				[JobTitle],
				[Department]
			) VALUES (
				'{userJobInfo.UserId}',
				'{userJobInfo.JobTitle}',
				'{userJobInfo.Department}'
			)";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Add user's jobs info.");
		}

		// Read - all
		[HttpGet("GetUsersJobInfo")]
		public IEnumerable<UserJobInfoModel> GetUsersJobInfo()
		{
			string sql = @"
			SELECT
				[UserId],
				[JobTitle],
				[Department]
			FROM [TutorialAppSchema].[UserJobInfo]
			";

			IEnumerable<UserJobInfoModel> usersJobInfo = _dapper.LoadData<UserJobInfoModel>(sql);

			return usersJobInfo;
		}

		// Read - byId
		[HttpGet("GetUserJobsInfo/{userId}")]
		public IEnumerable<UserJobInfoModel> GetUserJobsInfo(int userId)
		{
			string sql = @$"
			SELECT
				[UserId],
				[JobTitle],
				[Department]
			FROM [TutorialAppSchema].[UserJobInfo]
			WHERE [UserId] = {userId}
			";

			IEnumerable<UserJobInfoModel> userJobsInfo = _dapper.LoadData<UserJobInfoModel>(sql);

			return userJobsInfo;
		}

		// Update
		[HttpPut("EditUserJobInfo")]
		public IActionResult EditUserJobInfo(UserJobInfoModel userJobInfo)
		{
			string sql = @$"
			UPDATE [TutorialAppSchema].[UserJobInfo] SET
				[JobTitle] = '{userJobInfo.JobTitle}',
				[Department] = '{userJobInfo.Department}'
			WHERE UserId = {userJobInfo.UserId}
			";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Update user's jobs info.");
		}

		// Delete
		[HttpDelete("DeleteUserJobsInfo/{userId}")]
		public IActionResult DeleteUserJobsInfo(int userId)
		{
			string sql = @$"
			DELETE
			FROM [TutorialAppSchema].[UserJobInfo]
			WHERE [UserId] = {userId}
			";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Delete user's jobs info.");
		}
	}
}
