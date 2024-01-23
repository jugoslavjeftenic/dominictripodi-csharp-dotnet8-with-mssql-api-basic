using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using T070_AssignmentEntityFramework.Data;
using T070_AssignmentEntityFramework.Dtos;
using T070_AssignmentEntityFramework.Models;

namespace T070_AssignmentEntityFramework.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserJobInfoController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextEF _ef = new(config);

		// Create
		[HttpPost("AddUserJobInfo")]
		public IActionResult AddUserJobInfo(UserJobInfoModel userJobInfo)
		{
			UserJobInfoModel userJobInfoToAdd = new()
			{
				UserId = userJobInfo.UserId,
				JobTitle = userJobInfo.JobTitle,
				Department = userJobInfo.Department
			};

			_ef.Add(userJobInfoToAdd);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Add User's Job Info.");
		}

		// Read - All
		[HttpGet("GetUsersJobInfos")]
		public IEnumerable<UserJobInfoModel> GetUsersJobInfos()
		{
			return _ef.UserJobInfo.ToList();
		}

		// Read - ById
		[HttpGet("GetUserJobInfo/{userId}")]
		public UserJobInfoModel GetUserJobInfo(int userId)
		{
			return _ef.UserJobInfo
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User's Job Info with user ID: {userId}.");
		}

		// Update
		[HttpPut("EditUserJobInfo")]
		public IActionResult EditUserJobInfo(UserJobInfoModel userJobInfo)
		{
			UserJobInfoModel userJobInfoToEdit = _ef.UserJobInfo
				.Where(u => u.UserId == userJobInfo.UserId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User's Job Info during Update action.");

			userJobInfoToEdit.UserId = userJobInfo.UserId;
			userJobInfoToEdit.JobTitle = userJobInfo.JobTitle;
			userJobInfoToEdit.Department = userJobInfo.Department;

			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Update User's Job Info.");
		}

		// Delete
		[HttpDelete("DeleteUserJobInfo/{userId}")]
		public IActionResult DeleteUserJobInfo(int userId)
		{
			UserJobInfoModel userJobInfoToDelete = _ef.UserJobInfo
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception("Failed to Get User's Job Info during Delete action.");

			_ef.UserJobInfo.Remove(userJobInfoToDelete);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User's Job Info.");
		}
	}
}
