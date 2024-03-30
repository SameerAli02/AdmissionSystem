using Admission.Database;
using AdmissionSystem.Web.Areas.Admission.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdmissionSystem.Web.Areas.Admission.Controllers
{
    public class ClassMasterController : Controller
    {
        Db_AdmissionEntities db = new Db_AdmissionEntities();
        // GET: Admission/ClassMaster
        public ActionResult Index()
        {
            ClassMasterModel model = new ClassMasterModel();
            var classList = (from p in db.ClassMasters
                             select new ClassList
                             {
                                 Id = p.ClassId,
                                 ClassName = p.ClassName,
                                 CourseName = p.CourseMaster.CourseName
                             }).ToList();
            model.ClassList = classList;
            return View(model);
        }

        public ActionResult Create()
        {
            try
            {
                string schoolCode = Convert.ToString(Session["SchoolCode"]);

                var courses = db.CourseMasters.Where(p => p.SchoolCode == schoolCode).ToList();

                ViewBag.CourseId = new SelectList(courses, "CourseId", "CourseName");

                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Create(ClassMasterModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var checkClassName = db.ClassMasters.Where(x => x.ClassName.ToUpper() == model.ClassName.ToUpper().Trim()).Count();
                    if (checkClassName == 0)
                    {
                        ClassMaster classM = new ClassMaster();
                        classM.ClassName = model.ClassName;
                        classM.CourseId = model.CourseId;
                        db.ClassMasters.Add(classM);
                        db.SaveChanges();
                        TempData["message"] = "Data Saved Successfully";
                    }
                    else
                    {
                        TempData["message"] = "Class name already exists.";
                    }
                }
                else
                {
                    TempData["message"] = "Model is not valid.";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ClassId)
        {
            try
            {
                // Fetch the class details
                var @class = db.ClassMasters.Where(x => x.ClassId == ClassId).FirstOrDefault();
                if (@class == null)
                {
                    TempData["message"] = "Class not found";
                    return RedirectToAction("Index");
                }

                // Populate the model with class details
                ClassMasterModel model = new ClassMasterModel();
                model.Id = @class.ClassId;
                model.ClassName = @class.ClassName;
                model.CourseId = @class.CourseId; // Assuming CourseId is a property in your model

                // Fetch courses relevant to the class
                string schoolCode = Convert.ToString(Session["SchoolCode"]);
                var courses = db.CourseMasters.Where(p => p.SchoolCode == schoolCode).ToList();

                // Populate ViewBag with a list of courses
                ViewBag.CourseId = new SelectList(courses, "CourseId", "CourseName", model.CourseId);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(ClassMasterModel model)
        {
            if (ModelState.IsValid)
            {
                string schoolCode = Convert.ToString(Session["SchoolCode"]);
                ClassMaster classMaster = db.ClassMasters.Find(model.Id);
                classMaster.ClassName = model.ClassName;
                classMaster.CourseId = model.CourseId;
                db.Entry(classMaster).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Data Updated Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ClassId)
        {
            if (ModelState.IsValid)
            {
                ClassMaster classMaster = db.ClassMasters.Find(ClassId);
                db.Entry(classMaster).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                TempData["message"] = "Data Deleted Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }
    }
}
