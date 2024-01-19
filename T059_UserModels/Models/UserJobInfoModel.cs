namespace T059_UserModels.Models
{
	public partial class UserJobInfoModel
	{
		public int UserId { get; set; }
		public string JobTitle { get; set; } = "";
		public string Department { get; set; } = "";
	}
}
