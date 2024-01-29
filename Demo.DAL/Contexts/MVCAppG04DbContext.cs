using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAppG04DbContext:IdentityDbContext<ApplicationUser>
    {
        public MVCAppG04DbContext(DbContextOptions<MVCAppG04DbContext> options):base(options) { 
        }

       
            
       
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server = .;Database= secondvideomvc; Trusted_Connection= true");

        public DbSet<Departments> departments {  get; set; }
        public DbSet<Employee> employees { get; set; }
        //public DbSet<IdentityUser>  Users{ get; set; }
        //public DbSet<IdentityRole> Roles { get; set; }


    }
}
