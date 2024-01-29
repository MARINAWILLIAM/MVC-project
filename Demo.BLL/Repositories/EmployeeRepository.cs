using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
       

        public EmployeeRepository(MVCAppG04DbContext dbContext):base(dbContext)
        {
          
        }

        public IQueryable<Employee> SearchEmployeeByAddress(string Name)
        
         =>   _dbContext.employees.Where(E=>E.Name.ToLower().Contains(Name.ToLower()));
        



        #region Old Way
        //private readonly MVCAppG04DbContext _dbContext;
        //public EmployeeRepository(MVCAppG04DbContext dbContext)
        //{


        //    _dbContext = dbContext;
        //}
        //public int Add(Employee employee)
        //{
        //    _dbContext.employees.Add(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{

        //    _dbContext.employees.Remove(employee);
        //    return _dbContext.SaveChanges();

        //}

        //public Employee Get(int id)
        //{

        //    return _dbContext.employees.Find(id);


        //}

        //public IEnumerable<Employee> GetAll()

        //   => _dbContext.employees.ToList();


        //public int Update(Employee employee)
        //{
        //    _dbContext.employees.Update(employee);
        //    return _dbContext.SaveChanges();
        //} 
        #endregion

    }
}



