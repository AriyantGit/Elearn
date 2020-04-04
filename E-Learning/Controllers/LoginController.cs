using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    public class LoginController : Controller
    {
        _IAllRepository<TutorDetails> tutorobj;
        public LoginController()
        {
            this.tutorobj = new AllRepository<TutorDetails>();
        }
        // GET: Login
        public ActionResult TutorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TutorLogin(TutorDetails tutorDetails)
        {
            var tutorlogindetials=tutorobj.GetAll().Where(x=>x.Email.Equals(tutorDetails.Email) && x.Password.Equals(tutorDetails.Password)).FirstOrDefault();
            if(tutorlogindetials!=null)
            {
                Session["TutorId"] = tutorlogindetials.Id;
                Session["TutorName"] = tutorlogindetials.Fname+" "+tutorlogindetials.Lname;
                return RedirectToAction("Index", "TutorHome");
            }
            ViewBag.message = "User Email or Password is incorrect";
            return View();
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
