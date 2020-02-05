//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CondourApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class UserInfo
    {
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> AadharNumber { get; set; }
        public Nullable<long> PrimaryContactNumber { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string Venture { get; set; }
        public Nullable<int> PlotNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public Nullable<long> AltContactNumber { get; set; }
        public string Nominees { get; set; }
        public string Comments { get; set; }
        public string Attachments { get; set; }
    
        public virtual Role Role { get; set; }

        public HttpPostedFileBase postedFile { get; set; }
    }
}
