using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using Data.Birthday.Repository;
using Data.Birthday.RepositoryInterfaces;
using PagedList;
using System.Data;
using WebMatrix.WebData;
using MvcApplication1.Mailers;
using System.Text;



namespace MvcApplication1.Controllers
{
    
    [Authorize]
    public class BirthdayController : Controller
    {
        BirthdayInterface _birthdayRepository = null;

        //public BirthdayController(BirthdayInterface birthdayRepository)
        //{
        // _birthdayRepository = birthdayRepository;
        //}

        public BirthdayController()
        {
          
           _birthdayRepository= new BirthdayRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Email()
        {
            return View();
        }

        //
        // GET: /Birthday/

        public JsonResult  GetBirthdates(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            UsersContext db = new UsersContext();
                   // from u in db.UserProfiles where  select u
                     UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity .Name .ToLower());
                    // Check if user already exists
                      var Users = _birthdayRepository.GetBirthDates(user.UserId).Select(
                             a => new
                             {
                                 a.UserID,
                                 a.UserName,
                                 a.Password,
                                 a.Birthdate
                             }

                             );
                    
                         int pageSize = 5;
                         int pageNumber = (page ?? 1);
                         ViewBag.CurrentFilter = searchString;
                         int totalRecords = Users.Count();
                         var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
                         sortOrder = "DESC";
                         currentFilter = string.Empty;
                         searchString = string.Empty;
                         if (sortOrder.ToUpper() == "DESC")
                         {
                             Users = Users.OrderByDescending(s => s.UserName);
                             // Users = Users.Skip(pageNumber * pageSize).Take(pageSize);
                             Users = Users.Take(pageSize);
                         }
                         else
                         {
                             Users = Users.OrderBy(s => s.UserName);
                             Users = Users.Skip(pageNumber * pageSize).Take(pageSize);
                         }
                     
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = Users
                };
                var currentBirthDates = from b in Users
                                        where (b.Birthdate >= DateTime.Now.Date && b.Birthdate <= (DateTime.Now.Date.AddDays(7.00)))
                                                                                   select b;
                var mailer = new UserMailer();
                var msg = mailer.BirthdayReminder();
                StringBuilder body =new StringBuilder ();
                body.Append("<html>");
                foreach (var item in currentBirthDates)
                {
                    
                    body.Append( "<p>" + item.UserName + "       </p><p>  " + item.Birthdate + "</p>");
                   
                }
                 body.Append("</html>");
                 msg.Body = body.ToString ();
                msg.Send();
                return Json(jsonData, JsonRequestBehavior.AllowGet);
              
                //return View(Users);
        }

        
        //public ActionResult Edit(int id)
        //{
        //    BirthdayModels model =null;
        //    if (ModelState.IsValid)
        //    {
        //        var user = _birthdayRepository.GetUserByID(id);
        //       model = new BirthdayModels();
        //        model.Birthdate = user.Birthdate;
        //        model.Password = user.Password;
        //        model.UserName = user.UserName;
        //        TempData["id"] = id;
        //    }
        //    return View(model);
        //}

        public string Edit(Domain.Birthday.Entities.Birthday  birthday)
        {
            string msg="";
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Birthday.Entities.Birthday day = new Domain.Birthday.Entities.Birthday();
                    if (ModelState.IsValid)
                    {
                        day.UserID = birthday.UserID;
                        day.UserName = birthday.UserName;
                        day.Password = birthday.Password;
                        day.Birthdate = birthday.Birthdate;
                        _birthdayRepository.UpdateUser(day);
                        msg = "Updated Successfully";
                    }
                  
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public ActionResult Details(int id)
        {
            BirthdayModels model=null;
            if(ModelState .IsValid )
            {
            var user = _birthdayRepository.GetUserByID(id);
            model = new BirthdayModels();
            model.Birthdate = user.Birthdate;
            model.Password = user.Password;
            model.UserName = user.UserName;
            model.UserID = user.UserID;
            }
            return View(model);
        }

        public ActionResult  Update(FormCollection form)
        {
            Domain .Birthday .Entities .Birthday day=new Domain.Birthday.Entities.Birthday ();
            if (ModelState.IsValid)
            {
                day.UserID = Convert.ToInt32(TempData["id"]);
                day.UserName = form["UserName"];
                day.Password = form["Password"];
                day.Birthdate = Convert.ToDateTime(form["Birthdate"]);
                _birthdayRepository.UpdateUser(day);
            }
           return  RedirectToAction("Index","Birthday");
            
        }

        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost ]
        //public ActionResult Create(FormCollection form)
        //{

        //    Domain.Birthday.Entities.Birthday day = new Domain.Birthday.Entities.Birthday();
        //    if (ModelState.IsValid)
        //    {
        //        day.UserName = form["UserName"];
        //        day.Password = form["Password"];
        //        day.Birthdate = Convert.ToDateTime(form["Birthdate"]);
        //        _birthdayRepository.InsertUser(day);
        //    }
        //    return RedirectToAction("Index", "Birthday");

        //}

        [HttpPost]
        public string Create([Bind(Exclude = "UserID")] Domain.Birthday.Entities.Birthday birthday)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {

                    birthday.IsActive = true;
                    birthday.LatActivityDate = DateTime.Now.Date;
                    birthday =  _birthdayRepository.InsertUser(birthday);
                    Domain.Birthday.Entities.ContactBirthdate contact = new Domain.Birthday.Entities.ContactBirthdate();
                    contact.UserID = birthday.UserID;
                    contact.IsActive = true;
                    contact.LatActivityDate = DateTime.Now.Date;
                    UsersContext db = new UsersContext();
                   // from u in db.UserProfiles where  select u
                     UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity .Name.ToLower());
                    // Check if user already exists
                     if (user != null)
                     {
                         contact.ContactID = user.UserId;
                     }
                    _birthdayRepository.InserContactBirthdate(contact);
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        //public ActionResult Delete(int id)
        //{

        //    _birthdayRepository.DeleteUser(id);
        //    return RedirectToAction("Index", "Birthday");

        //}

        public string  Delete(int id)
        {

            _birthdayRepository.DeleteUser(id);
            return "Deleted successfully";
        }

    }
}
