using E_Learning.Filter;
using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    [Authorize]   
    [SessionTimeout]
    public class TopicListController : Controller
    {
        _IAllRepository<TopicList> obj;
        _IAllRepository<Course> courseobj;
        _IAllRepository<TopicLike> tlikeeobj;
        _IAllRepository<StudentCourseRegistration> sturegobj;
        DbModel db;
        public TopicListController()
        {
            this.db = new DbModel();
            this.obj = new AllRepository<TopicList>();
            this.courseobj = new AllRepository<Course>();
            this.tlikeeobj = new AllRepository<TopicLike>();
            this.sturegobj = new AllRepository<StudentCourseRegistration>();
        }

        // GET: TopicList
        [Authorize(Roles ="Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index(string addquestion)
        {
           // Session["TutorId"] = 1;
            var courselist = courseobj.GetAll().Where(x => x.TutorDetail.Id ==  Convert.ToInt32(Session["TutorId"])).GroupBy(x => x.Cname.ToUpper()).Select(x => x.FirstOrDefault());
            //.GroupBy(x => x.Cname ).Select(x => x.FirstOrDefault())
            //Convert.ToInt32(Session["TutorId"])
            ViewBag.droplist = courselist.Distinct();
            if (addquestion != null)
            {
                TempData["addquestion"] = true;
                ViewBag.addquestion = "true";

            }
                
            return View(); 
        }
        public JsonResult GetLevel(string level)
        {
            
            db.Configuration.ProxyCreationEnabled = false;
            var tutorid = Convert.ToInt32(Session["TutorId"]);
            var levellist = db.Courses.Where(x => x.Cname == level && x.TutorDetail.Id == tutorid).ToList();
            //var levellist = courseobj.GetAll().Where(x => x.Cname==level && x.TutorDetail.Id== 1).ToList();
            return Json(levellist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index(Course course)
        {
            if (course != null)
            {
                var coursedetails = courseobj.GetAll().Where(x => x.Cname == (course.Cname) && x.Clevel == course.Clevel && x.TutorDetail.Id == Convert.ToInt32(Session["TutorId"])).FirstOrDefault();
                Session["Courseid"] = coursedetails.Id;
                Session["CourseName"] = coursedetails.Cname + "(" + coursedetails.Clevel + ")";
                return RedirectToAction("TutorTopic", "TopicList");
            }
            else
                return View();
            
        }
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult TutorTopic()
        {
            if(TempData["fileupload"] != null)
            {
                ViewBag.fileuploadmessage = TempData["fileupload"];
            }
            var a = obj.GetAll().Where(x => x.CourseId == Convert.ToInt32(Session["Courseid"]));
            return View(a);
        }
       
        public void Tlike(int id)
        {
            var b = id;
            
            var a = new TopicLike();
            a.StudentId = Convert.ToInt32(Session["StudentId"]); 
            a.TopicId = id;
            tlikeeobj.InsertModel(a);
            tlikeeobj.Save();
        }
        
        public void TDislike(int id)
        {
            var b = id;
            var a = new TopicLike();
           var ba = tlikeeobj.GetAll().Where(x => x.TopicId == id && x.StudentId == Convert.ToInt32(Session["StudentId"])).FirstOrDefault();

            tlikeeobj.DeleteModel(ba.Id);
            tlikeeobj.Save();
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult TopicList(int? i,int? courseid)
        {
            var stuid = Convert.ToInt32(Session["StudentId"]) ;//come from session
            var coursedetails = courseid;
            ViewBag.data = tlikeeobj.GetAll().Where(x=> x.StudentId == stuid).Select(x=>x.TopicId).ToList();
           
            ViewBag.courseNameLevel = Session["CourseName"];
            
            if (i==null)
            {
                var a= obj.GetAll().Where(x => x.CourseId == coursedetails).FirstOrDefault();
                if(a!=null)
                {
                    if (a == null)
                        a.Views = 0;
                    else
                        a.Views += 1;
                    obj.UpdateModel(a);
                    obj.Save();
                    ViewBag.videourl = obj.GetAll().Where(x => x.CourseId == coursedetails).Select(x => x.VideoPath).FirstOrDefault();
                    ViewBag.topicid = obj.GetAll().Where(x => x.CourseId == coursedetails).Select(x => x.Id).FirstOrDefault();
                    var stucoursedetls = sturegobj.GetAll().Where(x => x.CourseId == coursedetails && x.StudentId == Convert.ToInt32(Session["Studentid"])).FirstOrDefault();
                    stucoursedetls.TopicListId = ViewBag.topicid;
                    sturegobj.UpdateModel(stucoursedetls);
                    sturegobj.Save();
                }
                else
                {
                    ViewBag.topic = "No data found";
                    return View();
                }
               // a.Views =a==null?0:a.Views+ 1;
                
            }
            else
            {
                var a= obj.GetAll().Where(x => x.Id == i).FirstOrDefault();
                a.Views += 1;
                obj.UpdateModel(a);
                obj.Save();
                ViewBag.videourl = obj.GetAll().Where(x => x.Id == i).Select(x => x.VideoPath).FirstOrDefault();
                ViewBag.topicid = obj.GetAll().Where(x => x.Id == i).Select(x => x.Id).FirstOrDefault();
                
                coursedetails = obj.GetAll().Where(x => x.Id == i).Select(x => x.CourseId).FirstOrDefault();
                var stucoursedetls = sturegobj.GetAll().Where(x => x.CourseId == coursedetails && x.StudentId==Convert.ToInt32(Session["Studentid"])).FirstOrDefault();
                stucoursedetls.TopicListId = i;
                sturegobj.UpdateModel(stucoursedetls);
                sturegobj.Save();
            }
            
            

            // var lists = courseobj.GetAll().GroupBy(x => x.Cname).Select(x => x.FirstOrDefault());
            //ViewBag.droplist = lists;
           
            //ViewBag.videourl = @"/Uploads/ASP.NET MVC Web Development - Udemy.mp4";
                //@"/Image/(3)%20Hashing%20-%20YouTube.MP4";
            return View(obj.GetAll().Where(x => x.CourseId == coursedetails).ToList());
        }
        public ActionResult AllTopic()
        {
            
            return View(obj.GetAll().Where(x=>x.CourseId == Convert.ToInt32(TempData["Courseid"])).ToList());
        }
        public ActionResult StuAllTopic(int id)
        {
            
            return View(obj.GetAll().Where(x => x.CourseId == id).ToList());
        }
        // GET: TopicList/Details/5
        public ActionResult Details(int id)
        {
            return View(obj.GetModelById(id));
        }
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult TutortopicDetails(int id)
        {
            ViewBag.Like =Convert.ToInt32(tlikeeobj.GetAll().Where(x => x.TopicId == id).Count());
            return View(obj.GetModelById(id));
        }
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        // GET: TopicList/Create
        public ActionResult Create()
        {
            ViewBag.courseNameLevel = Session["CourseName"];
            return View();
        }

        // POST: TopicList/Create
        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Create(TopicList collection)
        {
            try
            {
                
                // TODO: Add insert logic here
                HttpPostedFileBase filevideo = Request.Files["customFile"];
                string FileExtension = filevideo.ContentType;
                
                if (FileExtension== "video/mp4"  )
                {
                    int pos = FileExtension.IndexOf('/') + 1;
                    string exten = FileExtension.Substring(pos);
                    var filename = DateTime.Now.ToString("yyyyMMddTHHmmss.") + exten;
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //filevideo.FileName = "gdd";// DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");
                    //var a = DateTime.Now.ToString("yyyy-MM-dd’THH:mm:ss");
                    filevideo.SaveAs(path+filename);
                    HttpPostedFileBase filepdf = Request.Files["pdffile"];
                    
                    ViewBag.Message = "File uploaded successfully.";
                    //remove ~ from videopath for future use
                    collection.VideoPath = "/Uploads/" + filename;
                    if (filepdf.ContentLength > 0)
                    {
                        string FileExtensionpdf = filepdf.ContentType;
                        byte[] pdfeBytes = null;
                        BinaryReader reader = new BinaryReader(filepdf.InputStream);
                        pdfeBytes = reader.ReadBytes((int)filepdf.ContentLength);
                        collection.PdfContent = pdfeBytes;

                    }
                    collection.CourseId = Convert.ToInt32(Session["Courseid"]);
                    collection.Views = 0;
                    obj.InsertModel(collection);
                    obj.Save();
                    TempData["fileupload"] = "File Upload Successfully done";
                    return RedirectToAction("TutorTopic");
                }else
                {
                    return View();
                }
               
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: TopicList/Edit/5
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Edit(int id)
        {
            var a =obj.GetModelById(id);
            return View(a);
        }

        // POST: TopicList/Edit/5
        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Edit(int id, TopicList collection)
        {
            try
            {


                //var t = obj.GetModelById(id);
                //int pos = t.VideoPath.LastIndexOf('/') + 1;
                //string exten = t.VideoPath.Substring(pos);
                //var filename = exten;

                // TODO: Add insert logic here
                HttpPostedFileBase filevideo = Request.Files["customFile"];
                string FileExtension = filevideo.ContentType;
                if(filevideo.ContentLength>0)
                {
                    if (FileExtension == "video/mp4")
                    {
                        var topicdetails = obj.GetModelById(id);
                        int pos = topicdetails.VideoPath.LastIndexOf('/') + 1;
                        string exten = topicdetails.VideoPath.Substring(pos);
                        var filename = exten;
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        //filevideo.FileName = "gdd";// DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");
                        //var a = DateTime.Now.ToString("yyyy-MM-dd’THH:mm:ss");
                        filevideo.SaveAs(path + filename);
                        HttpPostedFileBase filepdf = Request.Files["pdffile"];

                        ViewBag.Message = "File uploaded successfully.";
                        //remove ~ from videopath for future use
                        //collection.VideoPath = "/Uploads/" + filename;
                        if (filepdf.ContentLength > 0)
                        {
                            string FileExtensionpdf = filepdf.ContentType;
                            byte[] pdfeBytes = null;
                            BinaryReader reader = new BinaryReader(filepdf.InputStream);
                            pdfeBytes = reader.ReadBytes((int)filepdf.ContentLength);
                            topicdetails.PdfContent = pdfeBytes;

                        }
                        topicdetails.TopicName = collection.TopicName;
                        topicdetails.Description = collection.Description;
                        obj.UpdateModel(topicdetails);
                        obj.Save();
                        TempData["fileupload"] = "File Upload Successfully done";
                        return RedirectToAction("TutorTopic");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    var topicdetails = obj.GetModelById(id);
                    
                    HttpPostedFileBase filepdf = Request.Files["pdffile"];

                    ViewBag.Message = "File uploaded successfully.";
                    //remove ~ from videopath for future use
                    //collection.VideoPath = "/Uploads/" + filename;
                    if (filepdf.ContentLength > 0)
                    {
                        string FileExtensionpdf = filepdf.ContentType;
                        byte[] pdfeBytes = null;
                        BinaryReader reader = new BinaryReader(filepdf.InputStream);
                        pdfeBytes = reader.ReadBytes((int)filepdf.ContentLength);
                        topicdetails.PdfContent = pdfeBytes;

                    }
                    topicdetails.TopicName = collection.TopicName;
                    topicdetails.Description = collection.Description;
                   // collection.CourseId = Convert.ToInt32(Session["Courseid"]);
                    //collection.Views = 0;
                    obj.UpdateModel(topicdetails);
                    obj.Save();
                    TempData["fileupload"] = "File Updated Successfully done";
                    return RedirectToAction("TutorTopic");

                }
               
                

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: TopicList/Delete/5
        [Authorize(Roles = "Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TopicList/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [CustomAuthenticationFilter]
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
