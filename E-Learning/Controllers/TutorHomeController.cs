using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    public class TutorHomeController : Controller
    {
        // GET: TutorHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: TutorHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TutorHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TutorHome/Create
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

        // GET: TutorHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TutorHome/Edit/5
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

        // GET: TutorHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TutorHome/Delete/5
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
