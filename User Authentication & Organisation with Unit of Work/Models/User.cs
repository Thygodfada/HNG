using System.ComponentModel.DataAnnotations;

namespace User_Authentication___Organisation_with_Unit_of_Work.Models
{
    public class User
    {
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
		public List<UserOrganization> UserOrganizations { get; set; }
	}
}
