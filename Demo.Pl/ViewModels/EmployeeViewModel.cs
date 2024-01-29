using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Pl.ViewModels
{
    
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "MaxLength of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "MinLength of Name is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 50, ErrorMessage = "Age Range from 22 to 50 ONly!!!!")]
        [Required(ErrorMessage =" Age Range from 22 to 50 ONly!!!!")]
        public int Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
          ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
       // [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool ISACTIVE { get; set; }
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        
        public string ImageName { get; set; }
        [Required(ErrorMessage = " image Required")]
        public IFormFile Image { get; set; }
        // public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Departments")]
        public int? DepartmentsId { get; set; }
        //w2at eli atcreate fe
        //NAVIGATION PROPERTY[ONE]
        public Departments Departments { get; set; }
        
    }
}
