using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    public class TutorController : Controller
    {
        _IAllRepository<TutorDetails> obj;
        _IAllRepository<Course> courseobj;
        _IAllRepository<StudentCourseRegistration> studentobj;
        DbModel db;
        public TutorController()
        {
            
            this.obj = new AllRepository<TutorDetails>();
            this.courseobj = new AllRepository<Course>();
            this.studentobj = new AllRepository<StudentCourseRegistration>();
        }
        // GET: Tutor
        public async Task<ActionResult> Index()
        {
            //obj.InsertModelasync(Student);
            
            var a = await obj.GetAllaync();
            var b = a.GetType();
            return View( a.Where(x=>x.Id==1));
        }

        // GET: Tutor/Details/5
        
        public async Task<ActionResult> Details(int id)
        {
            
            return View(await obj.GetModelByIdasync(id));
        }

        // GET: Tutor/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult TutorCourse()
        {
            int id = 1;
            return View(courseobj.GetAll().Where(x=>x.TutorId==id).ToList());
        }
        
        public  async Task<int> totalstudent(int id)
        {
            this.db = new DbModel();
            var a = await db.StudentCourseRegistrations.Where(x=>x.CourseId==id).CountAsync();

            //var a=await studentobj.GetAll().Where(x => x.CourseId == id).ToListAsync();
            return  a;
        }
        [HttpPost]
        public ActionResult CourseDetails(int id)
        {
            //var b = studentobj.GetAll().Where(x => x.CourseId == id).Select(x => new { Department = x.CourseId, EmployeesCount = x.StudentId }); 
           // Select(x => new { Department = x.Key, EmployeesCount = x.Count() });
            var a = studentobj.GetAll().Where(x => x.CourseId==id).ToList();
            return View(a);
        }
        // POST: Tutor/Create
        [HttpPost]
        public async Task<ActionResult> Create(TutorDetails collection)
        {
            try
            {
                // TODO: Add insert logic here
                collection.Password = "123";
                 await obj.InsertModelasync(collection);
                //await obj.SaveAsync();


                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Tutor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(obj.GetModelById(id));
        }

        // POST: Tutor/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, TutorDetails collection)
        {
            try
            {
                // TODO: Add update logic here
                collection.Password = "123";
              await obj.UpdateAsyn(collection);
               //obj.Save();
                //obj.Save();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Tutor/Delete/5
        public ActionResult Delete(int id)
        {
            return View(obj.GetModelById(id));
        }

        // POST: Tutor/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, TutorDetails collection)
        {
            try
            {
                // TODO: Add delete logic here
               await obj.DeleteAsyn(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
