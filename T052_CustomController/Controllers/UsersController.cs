using Microsoft.AspNetCore.Mvc;

namespace T052_CustomController.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		public UsersController()
		{

		}

		[HttpGet("GetUsers/{testValue}")]
		public string[] GetUsers(string testValue)
		{
			string[] responseArray = ["test1", "test2", testValue];
			return responseArray;
		}
	}
}
