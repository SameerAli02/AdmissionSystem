using Admission.Database;
using AdmissionSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AdmissionSystem.Web.Controllers
{
	public class AccountController : Controller
	{
		Db_AdmissionEntities db = new Db_AdmissionEntities();
		// GET: Account
		public ActionResult Login()
		{
			if (Request.IsAuthenticated)
			{
				string cookieName = FormsAuthentication.FormsCookieName; //find cookie name
				HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //get the cookie by it's name
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //decrypt it
				string UserName = ticket.Name; //you have the username!
				Session["Name"] = UserName;
				return RedirectToAction("Index", "Dashboard", new { area="Admission"});
			}
			LoginModel lm = new LoginModel();
			return View(lm);
		}

		[HttpPost]
		public ActionResult Login(LoginModel lm)
		{
			try
			{
				Request.Cookies.Clear();
				if (ModelState.IsValid)
				{
					string Password = encryption(lm.UserPassword);
					var check = (from p in db.Tb_Login where p.UserName == lm.UserName && p.UserPassword == Password && p.Active == true select p).Count();
					if (check > 0)
					{
						DateTime expiryDate = DateTime.Now.AddDays(30);

						//create a new froms auth ticket
						FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, lm.UserName, DateTime.Now, expiryDate, lm.isRemember, string.Empty);

						//encrypt the ticket
						string encryptedTicket = FormsAuthentication.Encrypt(ticket);

						//create a new authentication cookie - and set its expiration data
						HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
						authenticationCookie.Expires = ticket.Expiration;

						//add the cookie to the response.
						Response.Cookies.Add(authenticationCookie);
						Session["Name"] = lm.UserName;
						TempData["Message"] = "Login Successfully!";
						return RedirectToAction("Index", "Dashboard", new { area = "Admission" });
					}
					else
					{
						TempData["Message"] = "UnAuthorized User";
						return View(lm);
					}
				}
				else
				{
					return View(lm);
				}
			}
			catch (Exception ex)
			{
				TempData["Message"] = ex.Message;
				return View(lm);
			}
		}

		public ActionResult Register()
		{
			RegisterModel rm = new RegisterModel();
			return View(rm);
		}

		[HttpPost]
		public ActionResult Register(RegisterModel rm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Tb_Register tr = new Tb_Register();
					tr.Name = rm.Name;
					tr.Email = rm.Email;
					tr.ContactNo = rm.ContactNo;
					db.Tb_Register.Add(tr);

					string password = encryption(rm.Password);
					Tb_Login tl = new Tb_Login();
					tl.Id = GetNextId();
					tl.UserName = rm.Name;
					tl.UserPassword = password;
					tl.Active = true;
					db.Tb_Login.Add(tl);
					db.SaveChanges();
					Session["Name"] = rm.Name;
					TempData["Message"] = "User Register Successfully!";
					return RedirectToAction("Index", "Dashboard", new { area = "Admission" });
				}
				else
				{
					return View(rm);
				}
			}
			catch (Exception ex)
			{
				TempData["Message"] = ex.Message;
				return View(rm);
			}
		}
		private int GetNextId()
		{
			int currentMaxId = db.Tb_Login.Max(t => (int?)t.Id) ?? 0;
			return currentMaxId + 1;
		}
		public static string encryption(String password)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] encrypt;
			UTF8Encoding encode = new UTF8Encoding();
			encrypt = md5.ComputeHash(encode.GetBytes(password));
			StringBuilder encryptdata = new StringBuilder();
			for(int i = 0; i < encrypt.Length; i++)
			{
				encryptdata.Append(encrypt[i].ToString());
			}
			return encryptdata.ToString();
		}
	}
}