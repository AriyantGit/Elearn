using E_Learning.Filter;
using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    [Authorize]
    [SessionTimeout]
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
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public async Task<ActionResult> Index()
        {
            //obj.InsertModelasync(Student);
            
            var a = await obj.GetAllaync();
            var b = a.GetType();
            return View( a.ToList());
        }

        // GET: Tutor/Details/5
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public async Task<ActionResult> Details(int id)
        {
            
            return View(await obj.GetModelByIdasync(id));
        }

        // GET: Tutor/Create
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult TutorCourse()
        {
            if(TempData["Update"]!=null)
            {
                ViewBag.update = TempData["Update"];
            }
            int id =Convert.ToInt32(Session["TutorId"]);
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
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult CourseDetails(int id)
        {
            //var b = studentobj.GetAll().Where(x => x.CourseId == id).Select(x => new { Department = x.CourseId, EmployeesCount = x.StudentId }); 
           // Select(x => new { Department = x.Key, EmployeesCount = x.Count() });
            var a = studentobj.GetAll().Where(x => x.CourseId==id).ToList();
            return View(a);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        // POST: Tutor/Create
        [HttpPost]
        public  ActionResult Create(TutorDetails collection)
        {
            try
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                string FileExtension = file.ContentType;
                if ((FileExtension == "image/jpeg" || FileExtension == "image/jpg") && file.ContentLength <= 150000)
                {
                    var valid = obj.GetAll().Where(p => p.PhoneNo == collection.PhoneNo && p.Email == collection.Email).FirstOrDefault();
                    if (valid == null)
                    {
                        collection.Imageurl = ConvertToBytes(file);
                        collection.Disable = "No";
                        // TODO: Add insert logic here
                        collection.Role = "Tutor";
                          obj.InsertModel(collection);
                         obj.Save();


                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.user = "Email Id or Phone Number Already Register";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Mesage = "Please Give Any Image";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Tutor/Edit/5
        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult TutorCourseEdit(int id)
        {
            
            return View(courseobj.GetModelById(id));
        }
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Edit(int id)
        {
            return View(obj.GetModelById(id));
        }

        // POST: Tutor/Edit/5
        [HttpPost]
        public  ActionResult Edit(int id, TutorDetails data)
        {
            try
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                if(file!=null)
                {
                    string FileExtension = file.ContentType;
                    if ((FileExtension == "image/jpeg" || FileExtension == "image/jpg") && file.ContentLength <= 150000)
                    {
                        var td = obj.GetModelById(id);
                        td.Email = data.Email;
                        td.Fname = data.Fname;
                        td.Lname = data.Lname;
                        
                        td.PhoneNo = data.PhoneNo;
                        td.Imageurl = ConvertToBytes(file);
                        obj.UpdateModel(td);
                        obj.Save();
                        
                        // TODO: Add insert logic here

                       

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        var td = obj.GetModelById(id);
                        td.Email = data.Email;
                        td.Fname = data.Fname;
                        td.Lname = data.Lname;
                        
                        td.PhoneNo = data.PhoneNo;

                        obj.UpdateModel(td);
                        obj.Save();
                        return RedirectToAction("Index");
                    }
                }
               
                else
                {

                    // TODO: Add insert logic here

                    var td = obj.GetModelById(id);
                    td.Email = data.Email;
                    td.Fname = data.Fname;
                    td.Lname = data.Lname;
                    td.Password = data.Password;
                    td.PhoneNo = data.PhoneNo;
                   
                    obj.UpdateModel(td);
                    obj.Save();
                   


                        return RedirectToAction("Index");
                    }
                   
                
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Tutor/Delete/5
        [Authorize(Roles = "Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Delete(int id)
        {
            return View(obj.GetModelById(id));
        }

        // POST: Tutor/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [CustomAuthenticationFilter]
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
