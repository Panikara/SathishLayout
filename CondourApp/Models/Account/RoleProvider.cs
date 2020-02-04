using CondourApp.Models.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CondourApp.Models.DB;
namespace CustomMembership.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roleColl = new string[1];
            try
            {
                UserDBOperations db = new UserDBOperations();
                roleColl = db.GetRoelsForUser(username);
            }
            catch (Exception ex)
            {
                Library.WriteLog("At get roles role provider", ex);
                return new string[] { };
            }
                return roleColl;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool isUserInRole = false;
            try
            {
                UserDBOperations db = new UserDBOperations();
                isUserInRole = db.IsUserInRole(username,roleName);
            }
            catch (Exception ex)
            {
                Library.WriteLog("At IsUserInRole role provider", ex);
                return false;
            }
            return isUserInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}