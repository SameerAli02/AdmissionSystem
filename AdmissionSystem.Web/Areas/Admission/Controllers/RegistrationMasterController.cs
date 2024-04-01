﻿using Admission.Database;
using AdmissionSystem.Web.Areas.Admission.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
											   where a.SchoolCode == SchoolCode && a.Active == false
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

		[HttpPost]
		public ActionResult Create(RegistrationEntryModel obj)
		{
			try
			{
				if (ModelState.IsValid)
				{
					RegistrationMaster regist = new RegistrationMaster();
					string SchoolCode = Convert.ToString(Session["SchoolCode"]);
					regist.StudentName = obj.StudentName;
					regist.FatherName = obj.FatherName;
					regist.CourseId = obj.CourseId;
					regist.ClassId = obj.ClassId;
					regist.MobileNo = obj.MobileNo;
					regist.Address = obj.Address;
					regist.RegistrationAmount = obj.RegistrationAmount;
					regist.SchoolCode = SchoolCode;
					regist.RegistrationDate = DateTime.Now;
					regist.RegistrationNo = obj.RegistrationNo;
					db.RegistrationMasters.Add(regist);
					db.SaveChanges();
					TempData["message"] = "Student Registered Successfully";
					return RedirectToAction("Index");
				}
				else
				{
					TempData["message"] = "Please Enter Valid Data";
					string SchoolCode = Convert.ToString(Session["SchoolCode"]);
					ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName");
					ViewBag.ClassId = new SelectList(Enumerable.Empty<SelectListItem>());
					return View(obj);
				}
			}
			catch (Exception ex)
			{
				TempData["message"] = ex.Message;
				return RedirectToAction("Index");
			}
		}

		public ActionResult Edit(int id)
		{
			if (id == 0)
			{
				TempData["message"] = "Some Error Occured.";
				return RedirectToAction("Index");
			}
			RegistrationEntryModel obj = new RegistrationEntryModel();
			RegistrationMaster rm = db.RegistrationMasters.Find(id);
			string SchoolCode = Convert.ToString(Session["SchoolCode"]);
			ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName", rm.CourseId);
			ViewBag.ClassId = new SelectList(db.ClassMasters.Where(c => c.CourseId == rm.CourseId).ToList(), "ClassId", "ClassName", rm.ClassId);
			obj.id = id;
			obj.StudentName = rm.StudentName;
			obj.FatherName = rm.FatherName;
			obj.MobileNo = rm.MobileNo;
			obj.Address = rm.Address;
			obj.RegistrationAmount = rm.RegistrationAmount??0;
			obj.RegistrationNo = rm.RegistrationNo;
			return View(obj);
		}

		[HttpPost]
		public ActionResult Edit(RegistrationEntryModel obj)
		{
			try
			{
				if (ModelState.IsValid)
				{
					RegistrationMaster regist = db.RegistrationMasters.Find(obj.id);
					string SchoolCode = Convert.ToString(Session["SchoolCode"]);
					regist.StudentName = obj.StudentName;
					regist.FatherName = obj.FatherName;
					regist.CourseId = obj.CourseId;
					regist.ClassId = obj.ClassId;
					regist.MobileNo = obj.MobileNo;
					regist.Address = obj.Address;
					regist.RegistrationAmount = obj.RegistrationAmount;
					regist.SchoolCode = SchoolCode;
					regist.RegistrationDate = DateTime.Now;
					regist.RegistrationNo = obj.RegistrationNo;
					db.Entry(regist).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					TempData["message"] = "Student Registered Successfully";
					return RedirectToAction("Index");
				}
				else
				{
					TempData["message"] = "Please Enter Valid Data";
					string SchoolCode = Convert.ToString(Session["SchoolCode"]);
					ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName");
					ViewBag.ClassId = new SelectList(Enumerable.Empty<SelectListItem>());
					return View(obj);
				}
			}
			catch (Exception ex)
			{
				TempData["message"] = "Error: " + ex.Message; 
				return RedirectToAction("Index");
			}
		}

		public ActionResult Delete(int id)
		{
			try
			{
				RegistrationMaster regist = db.RegistrationMasters.Find(id);
				if (regist == null)
				{
					TempData["message"] = "Some Error Ouccured.Try Again!!";
					return RedirectToAction("Index");
				}
				else
				{
					regist.Active = true;
					db.Entry(regist).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					TempData["message"] = "Registration Cancelled Successfully!!";
				}
			}
			catch (Exception ex)
			{
				TempData["message"] = "Some Error Occured. Try Again.This Registration No. is related to some other Enteries";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
	}
}