using E_Learning.Filter;
using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class CourseAddController : Controller
    {
        _IAllRepository<Course> obj;
        public CourseAddController()
        {
            this.obj = new AllRepository<Course>();
        }
        // GET: CourseAdd
        [CustomAuthenticationFilter]
        [Authorize(Roles ="Tutor,Admin")]
        
        public ActionResult Index()
        {
            return View(obj.GetAll().Where(x=>x.TutorId == Convert.ToInt32(Session["TutorId"])).ToList());
        }

        // GET: CourseAdd/Details/5
        public ActionResult Details(int id)
        {
            return View("Details","TopicList",id);
        }

        // GET: CourseAdd/Create
        [CustomAuthenticationFilter]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseAdd/Create
        [HttpPost]
        [CustomAuthenticationFilter]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Create(Course collection)
        {
            try
            {

                // TODO: Add insert logic here
                collection.TutorId =  Convert.ToInt32(Session["TutorId"]);
                obj.InsertModel(collection);
                obj.Save();
                return RedirectToAction("Tutorcourse","Tutor");
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
        [CustomAuthenticationFilter]
        [Authorize(Roles = "Tutor,Admin")]
        // GET: CourseAdd/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseAdd/Edit/5
        [HttpPost]
        [CustomAuthenticationFilter]
        [Authorize(Roles = "Tutor,Admin")]
        public ActionResult Edit(int id, Course c)
        {
            try
            {
                // TODO: Add update logic here
                var coursedtls = obj.GetModelById(id);
                if (coursedtls != null)
                {
                    coursedtls.Cname = c.Cname;
                    coursedtls.Clevel = c.Clevel;
                    coursedtls.Fee = c.Fee;
                    obj.UpdateModel(coursedtls);
                    obj.Save();
                    TempData["Update"] = "Course Update Sucessfully done";
                    return RedirectToAction("TutorCourse","Tutor");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseAdd/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseAdd/Delete/5
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
