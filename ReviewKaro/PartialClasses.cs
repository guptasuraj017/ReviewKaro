using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReviewKaro.Models;
using System.ComponentModel.DataAnnotations;
namespace ReviewKaro
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
    }
    [MetadataType(typeof(OrganizationMetadata))]
    public partial class Organization
    {

    }

    [MetadataType(typeof(ReviewMetadata))]
    public partial class Review    
    {

    }


}