using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        //[MinLength(5, ErrorMessage = "MinLength of Name is 5 Chars")]
        public string Name { get; set; }
      
     
        public int? Age { get; set; }
       // [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
         // ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool ISACTIVE { get; set; }
        //[DataType(DataType.EmailAddress)]
      
        public string Email { get; set; }
      
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }=DateTime.Now;
        public string ImageName { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentsId { get; set; }
        //w2at eli atcreate fe
        //NAVIGATION PROPERTY[ONE]
        public Departments Departments { get; set; }


    }
}
