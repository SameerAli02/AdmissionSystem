using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdmissionSystem.Web.Areas.Admission.Models
{
	public class CourseMasterModel
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public List<Courses> CourseList { get; set; }
	}

	public class Courses
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
	}
}