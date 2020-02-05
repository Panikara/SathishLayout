using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace CondourApp.Models.DB
{
    public class AdminDBOperations
    {
        //public List<Bank> GetAllBanks()
        //{        
        //    using(ConsultancyEntities objEntity = new ConsultancyEntities())
        //    {
        //       return  objEntity.Banks.ToList();
        //    }
        //}
        //public  void Adduser(User user)
        //{
        //    try
        //    {
        //        using (ConsultancyEntities db = new ConsultancyEntities())
        //        {
                    
        //            user.UserId = Guid.NewGuid();

        //            db.Users.Add(user);
        //            db.SaveChanges();
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
        //public List<Role> GetallRoles()
        //{
        //    //try
        //    //{
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        return db.Roles.ToList();
        //    }
        //   // }
        //    //catch(Exception ex)
        //    //{
        //    //    //for display error
        //    //}

        //}
        //public List<User> GetAllUserDetails()
        //{
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        List<User> lstUSer = db.Users.ToList();
        //        return (lstUSer);
        //    }
        //}
        //public User EditUser(Guid id)
        //{
        //   // Guid editedGuid = new Guid(id);
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        return db.Users.Find(id);
        //    }
        //}
        //public void UpdateEditUSer(User user)
        //{
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        //User updateuser=db.Users.Find()
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //       // return db.Users
        //    }
        //}
        //public List<Application> GetAllApplicationnData()
        //{
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        List<Application> lstAppUserData = db.Applications.ToList();
        //        return (lstAppUserData);
        //    }
        //}
        //public Application EditAppliactionData(int id)
        //{
        //    // Guid editedGuid = new Guid(id);
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        return db.Applications.Find(id);
        //    }
        //}
        //public void SaveEditApplicationUser(Application appuser)
        //{
        //    using (ConsultancyEntities db = new ConsultancyEntities())
        //    {
        //        db.Entry(appuser).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //}
        public void DeleteApplicationUser(int id)
        {
            using (SathishLayoutEntities db = new SathishLayoutEntities())
            {
            //   Application userapp= db.Applications.Find(id);
              //  db.Applications.Remove(userapp);
               // db.SaveChanges();
            }
        }


    }

}