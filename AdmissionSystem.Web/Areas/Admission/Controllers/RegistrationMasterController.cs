using Admission.Database;
using AdmissionSystem.Web.Areas.Admission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdmissionSystem.Web.Areas.Admission.Controllers
{
    public class RegistrationMasterController : Controller
    {
        Db_AdmissionEntities db = new Db_AdmissionEntities();
        // GET: Admission/RegistrationMaster
        public ActionResult Index()
        {
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            RegistrationEntryModel rm = new RegistrationEntryModel();
            List<RegistredStudentList> data = (from a in db.RegistrationMasters
                                               where a.SchoolCode == SchoolCode
                                               select new RegistredStudentList
                                               {
                                                   id = a.RegistrationId,
                                                   CourseName = a.CourseMaster.CourseName,
                                                   ClassName = a.ClassMaster.ClassName,
                                                   StudentName = a.StudentName,
                                                   FatherName = a.FatherName,
                                                   ModileNo = a.MobileNo,
                                                   Address = a.Address,
                                                   RegistrationNo = a.RegistrationNo,
                                               }).ToList();
            rm.RegistredStudentList = data;
            return View(rm);
        }

        public ActionResult Create()
		{
            RegistrationEntryModel model = new RegistrationEntryModel();
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName");
            ViewBag.ClassId = new SelectList(Enumerable.Empty<SelectListItem>());
            var Count = (from p in db.RegistrationAmountMasters select p).Count();
            string RegisNo = "S000" + (Count = 1);
            model.RegistrationNo = RegisNo;
            return View(model);
        }

        public ActionResult FillClass(int CourseId)
		{
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            var classes = new SelectList(db.ClassMasters.Where(c => c.CourseId == CourseId).ToList(), "ClassId", "ClassName");
            var Amount = db.RegistrationAmountMasters.Where(x => x.SchoolCode == SchoolCode && x.CourseId == CourseId).Select(x => x.RegistrationAmount).FirstOrDefault();
            var jsonData = new { classes, Amount };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
		}
    }
}