//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B2B.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRegistration
    {
        public int ID { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string user_role { get; set; }
    }
}
