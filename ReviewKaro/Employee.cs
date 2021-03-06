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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.AssignedReviewDetails = new HashSet<AssignedReviewDetail>();
            this.AssignedReviewDetails1 = new HashSet<AssignedReviewDetail>();
            this.ReviewSubmissions = new HashSet<ReviewSubmission>();
            this.ReviewSubmissions1 = new HashSet<ReviewSubmission>();
        }
    
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Last_Name { get; set; }
        public System.DateTime DOB { get; set; }
        public int GenderId { get; set; }
        public string Designation { get; set; }
        public System.DateTime DOJ { get; set; }
        public string Department { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int EmployeeTypeId { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int OrganizationId { get; set; }
        public string Role { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignedReviewDetail> AssignedReviewDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignedReviewDetail> AssignedReviewDetails1 { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual State State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewSubmission> ReviewSubmissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewSubmission> ReviewSubmissions1 { get; set; }
    }
}
