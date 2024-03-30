using AdmissionSystem.Web.Areas.Admission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdmissionSystem.Web.Areas.Admission.Controllers
{
    [AdmissionAuthorization]
    public class DashboardController : Controller
    {
        // GET: Admission/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}