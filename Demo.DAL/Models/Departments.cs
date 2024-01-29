using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Departments
    {
        public int Id { get; set; }
        //key  by conventional
        [Required(ErrorMessage ="Code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        //string hythwal el anvarchar.max
        //required or not 
        //law anta .net 5 not law 6 requied ? law auzo optional
        //allow null
        public string Name { get; set; }
        public DateTime DateofCreation { get; set; }
       
        //navgatiin many
        public ICollection<Employee> Employees { get; set; }=new List<Employee>();  
    }
}
