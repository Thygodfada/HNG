namespace User_Authentication___Organisation_with_Unit_of_Work.Models
{
	public class UserOrganization
	{
		public string UserId { get; set; }
		public User User { get; set; }
		public string OrgId { get; set; }
		public Organization Organization { get; set; }
	}
}
