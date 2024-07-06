namespace User_Authentication___Organisation_with_Unit_of_Work.Models
{
	public class Organization
	{
		public string OrgId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<UserOrganization> UserOrganizations { get; set; }
	}
}
