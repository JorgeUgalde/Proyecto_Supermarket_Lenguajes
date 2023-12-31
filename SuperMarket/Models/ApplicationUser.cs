﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Models
{
	public class ApplicationUser : IdentityUser
	{

		public int UserIdentification { get; set; }

		[Required]
		public string Name { get; set; }

		[Required] 
		public string StreetAddress { get; set; }
	}
}
