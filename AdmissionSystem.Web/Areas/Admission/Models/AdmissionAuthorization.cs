using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdmissionSystem.Web.Areas.Admission.Models
{
	public class AdmissionAuthorization : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			HttpContext ctx = HttpContext.Current;

			string Name = Convert.ToString(HttpContext.Current.Session["Name"]);
			if (Name == "")
			{
				filterContext.Result = new RedirectResult("~/Account/Login");
				return;
			}
			base.OnActionExecuting(filterContext);
		}
	}
}