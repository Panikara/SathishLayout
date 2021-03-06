﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CondourApp.Models.Automation;
using System.Threading;
using CondourApp.Models.Logger;
using CondourApp.Models.DB;
using System.Web.Security;
using System.IO;

namespace CondourApp.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {

            //automation using webbrowser control 
            AutoResetEvent resultEvent = new AutoResetEvent(false);

            //getting linkedin details
            IELinkedin ieLinkedInBrowser = new IELinkedin(resultEvent);
            EventWaitHandle.WaitAll(new AutoResetEvent[] { resultEvent });
            string resultDetails = ieLinkedInBrowser.Details;



            ////Zauba Search
            //IEZauba objIEMyneta = new IEZauba();
            //string listMynetaData = objIEMyneta.GetData("VERINON TECHNOLOGY SOLUTIONS PRIVATE LIMITED");
            //ViewBag.html = listMynetaData;


            ////neta details
            //IEMyneta objIEMyneta = new IEMyneta();
            //List<string> listMynetaData = objIEMyneta.GetData("ysjagan");
            //ViewBag.html = listMynetaData;

            ////Gblobal Search
            //IEGlobal objIEMyneta = new IEGlobal();
            //string listMynetaData = objIEMyneta.GetData("suresh kareti");
            //ViewBag.html = listMynetaData;


            ////kanoon details
            //IEKanoon objIEKanoon = new IEKanoon();
            //List<string> listKanoonData = objIEKanoon.GetData("ysjagan");
            //ViewBag.html = listKanoonData;




            //AutoResetEvent resultEvent = new AutoResetEvent(false);
            //string resultDetails = string.Empty;

            ////browser = new IEBrowser(resultEvent);
            ////EventWaitHandle.WaitAll(new AutoResetEvent[] { resultEvent });
            ////string resultDetails = browser.Details;


            //Zaubacorp browser = new Zaubacorp(resultEvent);
            //EventWaitHandle.WaitAll(new AutoResetEvent[] { resultEvent });
            //resultDetails = browser.Details;
            //browser.Dispose();


            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection coll, string ReturnUrl = null)
        {
            try
            {
                UserDBOperations db = new UserDBOperations();

                Guid userId = db.IsValidUser(coll["userName"], coll["pwd"]);
                if (userId != Guid.Empty)//valid user
                {
                    SetUserId(userId, false);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        if (coll["userName"].ToLower() == "admin" && coll["pwd"].ToLower() == "admin")
                        {
                            return RedirectToAction("GetUsers", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("UserDetails", "Home", new { userName = coll["userName"] });
                        }

                    }

                }
                else
                {
                    @ViewBag.status = " Invalid Email/Phone Number or Password";
                }
            }
            catch (Exception ex)
            {
                Library.WriteLog("At Login UserName - " + coll["email-phone"], ex);
            }

            return View();
        }
        [Authorize]
        public ActionResult Home()
        {
            if (Roles.IsUserInRole("Admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (Roles.IsUserInRole("Maker"))
            {
                return RedirectToAction("Dashboard", "Maker");
            }
            else if (Roles.IsUserInRole("Checker"))
            {
                return RedirectToAction("Dashboard", "Checker");
            }
            return View();
        }
        public void SetUserId(Guid userId, bool rememberMe)
        {
            try
            {
                HttpCookie signinCookie = new HttpCookie("Condour");
                signinCookie.Value = userId.ToString();

                if (rememberMe)
                {
                    signinCookie.Expires = DateTime.Now.AddDays(5);
                }
                else
                {
                    signinCookie.Expires = DateTime.Now.AddDays(2);
                }

                UserDBOperations db = new UserDBOperations();
                UserInfo user = db.GetUser(userId);
                if (user != null)
                {

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                }
                this.ControllerContext.HttpContext.Response.Cookies.Add(signinCookie);
            }
            catch (Exception ex)
            {
                Library.WriteLog("At setuserid saving userid to cookie", ex);
            }
        }
        [Authorize]
        public ActionResult SignOut()
        {

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Condour"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Condour"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }


            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult LoginSuccess(string userName)
        {
            ViewBag.successMessage = "Login SuccessFully";
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserInfo userRegistration, HttpPostedFileBase postedFile, FormCollection coll)
        {
           
            SathishLayoutEntities1 entities = new SathishLayoutEntities1();

            string indexColl = coll["PlotColl.index"];
            string[] plotIndexColl = indexColl.Split(',');

            string path = Server.MapPath("~/UploadImages/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach(string index in plotIndexColl)
            {
                PlotDetailsInfo plotInfo = new PlotDetailsInfo();
                plotInfo.UserName = userRegistration.UserName;
                plotInfo.PlotNumber = coll["PlotColl[" + index + "]" + ".PlotNumber"];
                plotInfo.Area = coll["PlotColl[" + index + "]" + ".Area"];
                plotInfo.DocumentNumber = Convert.ToInt32(coll["PlotColl[" + index + "]" + ".DocumentNumber"]);
                plotInfo.YearOfRegistration = Convert.ToInt32(coll["PlotColl[" + index + "]" + ".YearOfRegistration"]);
                plotInfo.RegistrationOffice = coll["PlotColl[" + index + "]" + ".RegistrationOffice"];
                plotInfo.Nominee = coll["PlotColl[" + index + "]" + ".Nominee"];
                plotInfo.PlotDocumentPhoto = coll["PlotColl[" + index + "]" + ".PlotDocumentPhoto"];
                entities.PlotDetailsInfoes.Add(plotInfo);

            }

         //   if (Request.Files.Count > 0)
          //  {

                string fileName = System.IO.Path.GetFileName(postedFile.FileName);
                string filePath = "~/UploadImages/" + fileName;
                postedFile.SaveAs(path + postedFile.FileName);
                userRegistration.Attachments = filePath;
                userRegistration.UserId = Guid.NewGuid();
                userRegistration.Status = "In progress";
                userRegistration.PlotNumber = null;
                
                entities.UserInfoes.Add(userRegistration);

                entities.SaveChanges();

             
           // }


            return RedirectToAction("RegisterSuccess");
        }

        public ActionResult RegisterSuccess()
        {
            ViewBag.RegisterMessage = "Registration SuccessFully Completed";
            return View();
        }

        public ActionResult UserDetails(string userName)
        {
            SathishLayoutEntities1 dbContext = new SathishLayoutEntities1();
            UserData userData = new UserData();
            UserInfo user = dbContext.UserInfoes.Where(u => u.UserName == userName).FirstOrDefault();
            List<PlotDetailsInfo> plotsInfo = dbContext.PlotDetailsInfoes.Where(s => s.UserName == userName).ToList();
            userData.UserInfo = user;
            userData.PlotsInfo = plotsInfo;
            ViewBag.userName = userName;
            return View(userData);
           
        }
      

        public ActionResult NextClickGeneratepartialView()
        {

            return PartialView("~/Views/PlotDetails_PartialView.cshtml");
        }

        public ActionResult PartialViewDisplay(string userName)
        {
            SathishLayoutEntities1 dbContext = new SathishLayoutEntities1();
            //UserInfo singleUser = dbContext.UserInfoes.Where(u => u.UserName == userName).FirstOrDefault();
            List<PlotDetailsInfo> Singleplot = dbContext.PlotDetailsInfoes.Where(s => s.UserName ==userName).ToList();
            ViewBag.userName = userName;
            return View(Singleplot);
        }
    }
}