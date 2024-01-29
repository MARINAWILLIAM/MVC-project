using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly MVCAppG04DbContext _dbContext;

        public IEmployeeRepository employeeRepository { get; set; }
        public IDepartmentRepository departmentRepository { get; set; }
        public UnitOfWork(MVCAppG04DbContext dbContext)
        {
            employeeRepository=new EmployeeRepository(dbContext);
            departmentRepository=new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        
          =>  await _dbContext.SaveChangesAsync();
       
        public void Dispose() {
            _dbContext.Dispose();
        }
    }
}
