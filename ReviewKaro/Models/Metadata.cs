using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewKaro.Models
{
    public class EmployeeMetadata
    {
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string FirstName;

        [Required]
        public string Last_Name;

        [Required]
        public System.DateTime DOB;

        [Required]
        public int GenderId;

        [Required]
        public string Designation;

        [Required]
        public System.DateTime DOJ;
      

        [Required]
        public string Department;

        [Required]
        public int CountryId;

        [Required]
        public int StateId;

        [Required]
        public int CityId;

        [Required]
        public int EmployeeTypeId;

        [Required]
        public long PhoneNumber;

        [Required]
        public string Email;

        [Required]
        [Remote("IsUserNameAvailable", "Employees",ErrorMessage ="This username already exists")]
        public string Username { get; set; }

        [Required]
        public string Password;
    }

    public class OrganizationMetadata
    {
        [Required]
        public string Name;

        [Required]
        public long Phone_Number;

        [Required]
        public string Email;

        [Required]
        [Remote("IsUserNameAvailable", "Home")]
        public string UserId { get; set; }

        [Required]
        public string Password;

        [Required]
        public int CountryId;

        [Required]
        public int StateId;

        [Required]
        public int CityId;

        [Required]
        public bool Status;
    }
    public class ReviewMetadata {

        [Required]
        public string Agenda { get; set; }

        [Required]
        public System.DateTime ReviewCycleStartDate { get; set; }

        [Required]

        public System.DateTime ReviewCycleEndDate { get; set; }

        [Required]

        public byte MinRate { get; set; }

        [Required]

        public byte MaxRate { get; set; }
    }
}