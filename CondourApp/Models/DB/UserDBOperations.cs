using CondourApp.Models.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CondourApp;
namespace CondourApp.Models.DB
{
    public class UserDBOperations
    {
        public Guid IsValidUser(string userName, string pwd)
        {
            UserInfo user = new UserInfo();
            try
            {
                using (SathishLayoutEntities db = new SathishLayoutEntities())
                {
                    
      user = db.UserInfoes.Where(u => u.UserName.ToLower() == userName.ToLower()).Where(u => u.Password == pwd).FirstOrDefault();
                   
                }
            }
            catch (Exception ex)
            {
                Library.WriteLog("At IsValidUser UserName- " + userName, ex);
                return Guid.Empty;
            }

            if (user != null)
            {
                return user.UserId;
            }
            else
            {
                return Guid.Empty;
            }

        }

        public UserInfo GetUser(Guid id)
        {
            try
            {
                using (SathishLayoutEntities db = new SathishLayoutEntities())
                {
                    UserInfo user = db.UserInfoes.Find(id);
                    if (user != null)
                        return user;
                }
            }
            catch (Exception ex)
            {
                Library.WriteLog("At Get User", ex);

            }

            return null;

        }

        public string[] GetRoelsForUser(string userName)
        {
            string[] roleColl = new string[1];
            try
            {
                using (SathishLayoutEntities db = new SathishLayoutEntities())
                {

        UserInfo   user = db.UserInfoes.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
                    roleColl[0] = user.Role.Role1;
                }
            }
            catch (Exception ex)
            {
                Library.WriteLog("At IsValidUser UserName- " + userName, ex);
                return roleColl;
            }

            return roleColl;

        }

        public bool IsUserInRole(string userName,string role)
        {
            bool isUserInRole = false;
            try
            {
                using (SathishLayoutEntities db = new SathishLayoutEntities())
                {

      UserInfo user = db.UserInfoes.Where(u => u.UserName.ToLower() == userName.ToLower()).
                        Where(u=>u.Role.Role1==role).FirstOrDefault();
                    if (user != null)
                    {
                        isUserInRole = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Library.WriteLog("At IsUserInRole UserName- " + userName, ex);
                return false;
            }

            return isUserInRole;

        }
    }
}