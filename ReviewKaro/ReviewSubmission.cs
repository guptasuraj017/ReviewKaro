//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReviewKaro
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReviewSubmission
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public byte Rating { get; set; }
        public string Feedback { get; set; }
        public Nullable<int> OrganizationId { get; set; }
        public long ReviewId { get; set; }
        public long ReviewerId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Review Review { get; set; }
        public virtual Employee Employee1 { get; set; }
    }
}
