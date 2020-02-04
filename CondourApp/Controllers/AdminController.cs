﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CondourApp.Models.DB;
using CondourApp.Models.Edmx;


namespace CondourApp.Controllers
{
    public class AdminController : Controller
    {
        AdminDBOperations objDb = new AdminDBOperations();
        // GET: Admin
        public ActionResult AddUser()
        {
            List<Role> lstRoles = objDb.GetallRoles();
            ViewBag.GetAllUserDetails = objDb.GetAllUserDetails();
            ViewBag.Roles = new SelectList(lstRoles, "RoleId", "Role1");
            return View();
        }

        // GET: Email
        public ActionResult EmailSettings()
        {
            

           var banksSelectList = objDb.GetAllBanks().Select(x => new SelectListItem() { Text =x.BankName,Value= x.BankId.ToString()}).ToList();         
            return View(banksSelectList);
        }

        // GET: Report
        public ActionResult ReportSettings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserInfoes user)
        {
            
            objDb.Adduser(user);
           
            return View();
        }
        public List<User> GetAllUSerDetails(int id)
        {
            List<User> lstUser = objDb.GetAllUserDetails();
            return (lstUser);
        }
        [HttpGet]
        public PartialViewResult EditUser(string id)
        {
            
            Guid guid = new Guid(id);
            User userDetails = objDb.EditUser(guid);
            List<Role> lstRoles = objDb.GetallRoles();
            ViewBag.Roles = new SelectList(lstRoles, "RoleId", "Role1");
           return PartialView ("_FillUser",userDetails);
        }
        [HttpPost]
        public void EditUser(User user)
        {
            objDb.UpdateEditUSer(user);
            List<Role> lstRoles = objDb.GetallRoles();
            ViewBag.Roles = new SelectList(lstRoles, "RoleId", "Role1");

        }
        public ActionResult GetAllApplicationDetails()
        {
            List<Application> lstapplidata = objDb.GetAllApplicationnData();
            return View(lstapplidata);
        }
        //For Application User Details Edit
        public ActionResult Editapplication(int id)
        {

            //Guid guid = new Guid(id);
            Application userDetails = objDb.EditAppliactionData(id);
            List<Role> lstRoles = objDb.GetallRoles();
            CommonDBOperations objCommDbOper = new CommonDBOperations();
            ViewBag.Banks = new SelectList(objCommDbOper.GetAllBanks(), "BankId", "BankName");
            ViewBag.Locations = new SelectList(objCommDbOper.GetAllLocations(), "LocationId", "Location1");
            ViewBag.Branches = new SelectList(objCommDbOper.GetAllBranches(), "BranchId", "BranchName");
            ViewBag.Products = new SelectList(objCommDbOper.GetAllProducts(), "ProductId", "Product1");
            ViewBag.Roles = new SelectList(lstRoles, "RoleId", "Role1");
            return View(userDetails);
        }
        //For Conform Edited Application User Details
        [HttpPost]
        public ActionResult EditaApplicationUser(Application appuser)
        {
            objDb.SaveEditApplicationUser(appuser);
            return RedirectToAction("GetAllApplicationDetails");
        }

        public ActionResult DeleteApplicationUser(int id)
        {
            objDb.DeleteApplicationUser(id);
            return RedirectToAction("GetAllApplicationDetails");
        }
        public ActionResult DetailsApplicationUser(int id)
        {
            Application userDetails = objDb.EditAppliactionData(id);
            return View(userDetails);
        }
    }
}