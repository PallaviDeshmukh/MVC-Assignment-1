//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Birthday.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactBirthdate
    {
        public int ID { get; set; }
        public Nullable<int> ContactID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> LatActivityDate { get; set; }
    
        public virtual Birthday Birthday { get; set; }
    }
}
