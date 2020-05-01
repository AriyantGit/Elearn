using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Learning.Controllers
{
    public class LoginController : Controller
    {
        _IAllRepository<TutorDetails> tutorobj;
        _IAllRepository<Student> stuobj;
        public LoginController()
        {
            this.tutorobj = new AllRepository<TutorDetails>();
            this.stuobj = new AllRepository<Student>();
        }
        // GET: Login
        public ActionResult TutorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TutorLogin(TutorDetails tutorDetails)
        {
            //Session.Abandon();
            var tutorlogindetials=tutorobj.GetAll().Where(x=>x.Email.Equals(tutorDetails.Email) && x.Password.Equals(tutorDetails.Password)).FirstOrDefault();
            if(tutorlogindetials!=null)
            {
                if(tutorlogindetials.Disable.ToUpper().Equals("NO"))
                {
                    FormsAuthentication.SetAuthCookie(tutorlogindetials.Email,false);
                    this.Session["TutorId"] = tutorlogindetials.Id;
                    this.Session["userId"] = tutorlogindetials.Id;
                    this.Session["TutorName"] = tutorlogindetials.Fname + " " + tutorlogindetials.Lname;
                    if(tutorlogindetials.Imageurl!=null)
                    {
                        var base64 = Convert.ToBase64String(tutorlogindetials.Imageurl);
                        var imgsrc = string.Format("data:image/jpeg;base64,{0}", base64);
                        this.Session["image"] = imgsrc;
                    }
                   

                    return RedirectToAction("Index", "TutorHome");
                }
                else
                {
                    ViewBag.message = "User Account Disable by Admin";
                    return View();

                }
               
            }
            ViewBag.message = "User Email or Password is incorrect";
            return View();
        }
        public ActionResult stulogin()
        {
            return View();
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        
        }
        public ActionResult tutorsignup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult tutorsignup(TutorDetails collection)
        {
            try
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                string FileExtension = file.ContentType;
                if ((FileExtension == "image/jpeg" || FileExtension == "image/jpg") && file.ContentLength <= 150000)
                {
                    var valid = tutorobj.GetAll().Where(p => p.PhoneNo == collection.PhoneNo && p.Email == collection.Email).FirstOrDefault();
                    if (valid == null)
                    {
                        collection.Imageurl = ConvertToBytes(file);
                        collection.Disable = "No";
                        // TODO: Add insert logic here
                        collection.Role = "Tutor";
                        tutorobj.InsertModel(collection);
                        tutorobj.Save();


                        return RedirectToAction("tutorlogin");
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
        public ActionResult stusignup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult stusignup([Bind(Include = "StudentId,FName,LName,Email,Mobile,Image,Password,DateCreated,UserModified")] Student student)
        {

            try
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                string FileExtension = file.ContentType;
                if ((FileExtension == "image/jpeg" || FileExtension == "image/jpg") && file.ContentLength <= 150000 && student.Password.Length>5)
                {
                    var valid = stuobj.GetAll().Where(p => p.Mobile == student.Mobile && p.Email == student.Email).FirstOrDefault();
                    if (valid == null)
                    {
                        student.Image = ConvertToBytes(file);
                        student.Role = "Student";
                        student.Disable = "No";

                        stuobj.InsertModel(student);
                        stuobj.Save();

                        return RedirectToAction("StudentLogin");
                    }
                    else
                    {
                        ViewBag.user = "Email Id or Phone Number Already Register";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Please Give All Information";
                    return View();
                }
                
            }
            catch( Exception ex)
            {
                return View(ex);

            }

            
        }
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentLogin(Student stu)
        {
            //Session.Abandon();
            var stulogindetials = stuobj.GetAll().Where(x => x.Email.Equals(stu.Email) && x.Password.Equals(stu.Password)).FirstOrDefault();
            if (stulogindetials != null)
            {
                if (stulogindetials.Disable.ToUpper().Equals("NO"))
                {
                    FormsAuthentication.SetAuthCookie(stulogindetials.Email, false);

                    this.Session["StudentId"] = stulogindetials.StudentId;
                    this.Session["UserID"] = stulogindetials.StudentId;
                    this.Session["StuName"] = stulogindetials.FName + " " + stulogindetials.LName;
                    if (stulogindetials.Image != null)
                    {
                        var base64 = Convert.ToBase64String(stulogindetials.Image);
                        var imgsrc = string.Format("data:image/jpeg;base64,{0}", base64);
                        this.Session["image"] = imgsrc;
                    }


                    return RedirectToAction("Index", "Stuhome");
                }
                else
                {
                    ViewBag.message = "User Account Disable by Admin";
                    return View();

                }

            }
            ViewBag.message = "User Email or Password is incorrect";
            return View();
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult tutorLogout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("TutorLogin");
        }
        public ActionResult studentLogout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("studentLogin");
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
