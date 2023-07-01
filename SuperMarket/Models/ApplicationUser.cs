using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int PhoneNumber { get; set; }

		[Required]
		public string Email { get; set; }

		[Required] 
		public string StreetAddress { get; set; }
	}
}
