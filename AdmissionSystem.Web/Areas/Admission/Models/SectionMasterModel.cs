using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmissionSystem.Web.Areas.Admission.Models
{
	public class SectionMasterModel
	{
		[Required(ErrorMessage = "Select Course")]
		public int CourseId { get; set; }
		[Required(ErrorMessage = "Select Class")]
		public int ClassId { get; set; }
		public int SectionId { get; set; }
		[Required(ErrorMessage = "Enter Section Name")]
		public string SectionName { get; set; }
		public List<SectionList> SectionList { get; set; }
	}

	public class SectionList
	{
		public int SectionId { get; set; }
		public string SectionName { get; set; }
		public string ClassName { get; set; }
		public string CourseName { get; set; }
	}
}