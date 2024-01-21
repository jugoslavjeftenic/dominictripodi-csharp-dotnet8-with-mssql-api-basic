namespace T069_AssignmentDapper.Models
{
	public class UserModel
	{
		public int UserId { get; set; }
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string Email { get; set; } = "";
		public string Gender { get; set; } = "";
		public bool Active { get; set; }
	}
}
