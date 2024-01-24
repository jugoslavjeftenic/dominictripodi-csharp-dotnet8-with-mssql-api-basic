using Microsoft.AspNetCore.Mvc;
using T070_AssignmentEntityFramework.Data;
using T070_AssignmentEntityFramework.Models;

namespace T070_AssignmentEntityFramework.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserSalaryController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextEF _ef = new(config);

		// Create
		[HttpPost("AddUserSalary")]
		public IActionResult AddUserSalary(UserSalaryModel userSalary)
		{
			UserSalaryModel userSalaryToAdd = new()
			{
				UserId = userSalary.UserId,
				Salary = userSalary.Salary,
				AvgSalary = userSalary.AvgSalary
			};

			_ef.Add(userSalaryToAdd);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Add User's Salary.");
		}

		// Read - All
		[HttpGet("GetUsersSalary")]
		public IEnumerable<UserSalaryModel> GetUsersSalary()
		{
			return _ef.UserSalary.ToList();
		}

		// Read - ById
		[HttpGet("GetUserSalary/{userId}")]
		public UserSalaryModel GetUserSalary(int userId)
		{
			return _ef.UserSalary
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User's Salary with user ID: {userId}.");
		}

		// Update
		[HttpPut("EditUserSalary")]
		public IActionResult EditUserSalary(UserSalaryModel userSalary)
		{
			UserSalaryModel userSalaryToEdit = _ef.UserSalary
				.Where(u => u.UserId == userSalary.UserId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User's Salary during Update action.");

			userSalaryToEdit.UserId = userSalary.UserId;
			userSalaryToEdit.Salary = userSalary.Salary;
			userSalaryToEdit.AvgSalary = userSalary.AvgSalary;

			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Update User's Salary.");
		}

		// Delete
		[HttpDelete("DeleteUserSalary/{userId}")]
		public IActionResult DeleteUserSalary(int userId)
		{
			UserSalaryModel userSalaryToEdit = _ef.UserSalary
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception("Failed to Get User's Salary during Delete action.");

			_ef.UserSalary.Remove(userSalaryToEdit);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User's Salary.");
		}
	}
}
