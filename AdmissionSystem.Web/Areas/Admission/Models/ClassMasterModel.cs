using Admission.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdmissionSystem.Web.Areas.Admission.Models
{
	public class ClassMasterModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Enter Class Name")]
		public string ClassName { get; set; }

		public int CourseId { get; set; }
		public List<ClassList> ClassList { get; set; }
	}

	public class ClassList
	{
		public int Id { get; set; }
		public string ClassName { get; set; }
		public string CourseName { get; set; }
	}
}