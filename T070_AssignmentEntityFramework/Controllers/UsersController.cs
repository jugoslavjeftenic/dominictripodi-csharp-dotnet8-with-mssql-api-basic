using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using T070_AssignmentEntityFramework.Data;
using T070_AssignmentEntityFramework.Dtos;
using T070_AssignmentEntityFramework.Models;

namespace T070_AssignmentEntityFramework.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextEF _ef = new(config);
		private readonly Mapper _mapper = new(new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<UserToAddDto, UserModel>();
		}));

		// Create
		[HttpPost("AddUser")]
		public IActionResult AddUser(UserToAddDto user)
		{
			UserModel userToAdd = _mapper.Map<UserModel>(user);

			_ef.Add(userToAdd);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Add User.");
		}

		// Read - All
		[HttpGet("GetUsers")]
		public IEnumerable<UserModel> GetUsers()
		{
			return _ef.Users.ToList();
		}

		// Read - ById
		[HttpGet("GetUser/{userId}")]
		public UserModel GetUser(int userId)
		{
			return _ef.Users
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User with ID: {userId}.");
		}

		// Update
		[HttpPut("EditUser")]
		public IActionResult EditUser(UserModel user)
		{
			UserModel userToEdit = _ef.Users
				.Where(u => u.UserId == user.UserId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User during Update action.");

			userToEdit.FirstName = user.FirstName;
			userToEdit.LastName = user.LastName;
			userToEdit.Email = user.Email;
			userToEdit.Gender = user.Gender;
			userToEdit.Active = user.Active;

			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Update User.");
		}

		// Delete
		[HttpDelete("DeleteUser/{userId}")]
		public IActionResult DeleteUser(int userId)
		{
			UserModel userToDelete = _ef.Users
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception("Failed to Get User during Delete action.");

			_ef.Users.Remove(userToDelete);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User.");
		}
	}
}
