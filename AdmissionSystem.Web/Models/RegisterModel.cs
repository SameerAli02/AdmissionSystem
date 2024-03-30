using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmissionSystem.Web.Models
{
	public class RegisterModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string ContactNo { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[Compare("Password", ErrorMessage ="Password must be same")]
		public string ConfirmPassword { get; set; }
	}
}