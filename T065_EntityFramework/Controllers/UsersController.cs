using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using T065_EntityFramework.Data;
using T065_EntityFramework.Dtos;
using T065_EntityFramework.Models;

namespace T065_EntityFramework.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController(IConfiguration config) : ControllerBase
	{
		private readonly DataContextEF _ef = new(config);
		IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<UserToAddDto, UserModel>();
		}));

		[HttpGet("GetUsers")]
		public IEnumerable<UserModel> GetUsers()
		{
			return _ef.Users.ToList<UserModel>();
		}

		[HttpGet("GetUser/{userId}")]
		public UserModel GetUser(int userId)
		{
			return _ef.Users
				.Where(u => u.UserId == userId)
				.FirstOrDefault<UserModel>() ?? throw new Exception("Failed to Get User.");
		}

		[HttpPut("EditUser")]
		public IActionResult EditUser(UserModel user)
		{
			UserModel userToEdit = _ef.Users
				.Where(u => u.UserId == user.UserId)
				.FirstOrDefault<UserModel>() ?? throw new Exception("Failed to Get User during Update action.");

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

		[HttpPost("AddUser")]
		public IActionResult AddUser(UserToAddDto user)
		{
			//Without of AutoMapper
			//UserModel userToAdd = new()
			//{
			//	FirstName = user.FirstName,
			//	LastName = user.LastName,
			//	Email = user.Email,
			//	Gender = user.Gender,
			//	Active = user.Active
			//};

			//With AutoMapper
			UserModel userToAdd = _mapper.Map<UserModel>(user);

			_ef.Add(userToAdd);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Add User.");
		}

		[HttpDelete("DeleteUser/{userId}")]
		public IActionResult DeleteUser(int userId)
		{
			UserModel userToDelete = _ef.Users
				.Where(u => u.UserId == userId)
				.FirstOrDefault<UserModel>() ?? throw new Exception("Failed to Get User during Delete action.");

			_ef.Users.Remove(userToDelete);
			if (_ef.SaveChanges() > 0)
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User.");
		}
	}
}
