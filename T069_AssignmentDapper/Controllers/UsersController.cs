﻿using Microsoft.AspNetCore.Mvc;
using T069_AssignmentDapper.Data;
using T069_AssignmentDapper.Dtos;
using T069_AssignmentDapper.Models;

namespace T069_AssignmentDapper.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextDapper _dapper = new(config);

		// Create
		[HttpPost("AddUser")]
		public IActionResult AddUser(UserToAddDto user)
		{
			string sql = @$"
			INSERT INTO [TutorialAppSchema].[Users] (
				[FirstName],
				[LastName],
				[Email],
				[Gender],
				[Active]
			) VALUES (
				'{user.FirstName}',
				'{user.LastName}',
				'{user.Email}',
				'{user.Gender}',
				'{user.Active}'
			)";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Add user");
		}

		// Read - all
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

		// Read - byId
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

		// Update
		[HttpPut("EditUser")]
		public IActionResult EditUser(UserModel user)
		{
			string sql = @$"
			UPDATE [TutorialAppSchema].[Users] SET
				[FirstName] = '{user.FirstName}',
				[LastName] = '{user.LastName}',
				[Email] = '{user.Email}',
				[Gender] = '{user.Gender}',
				[Active] = '{user.Active}'
			WHERE UserId = {user.UserId}
			";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Update user");
		}

		// Delete
		[HttpDelete("DeleteUser/{userId}")]
		public IActionResult DeleteUser(int userId)
		{
			string sql = @$"
			DELETE
			FROM [TutorialAppSchema].[Users]
			WHERE [UserId] = {userId}
			";

			if (_dapper.ExecuteSql(sql))
			{
				return Ok();
			}

			throw new Exception("Failed to Delete user");
		}
	}
}
