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
    public class DepartmentRepository : GenericRepository<Departments>, IDepartmentRepository
    {
        #region Old Way
        //private readonly  MVCAppG04DbContext _dbContext;
        //public DepartmentRepository(MVCAppG04DbContext dbContext   )
        //{
        //    // dbContext = new MVCAppG04DbContext();
        //    //intalzation

        //    _dbContext = dbContext;
        //}
        //public int Add(Departments departments)
        //{
        //    _dbContext.departments.Add(departments);
        //  return   _dbContext.SaveChanges();//3dad row hasl leha affect
        //}

        //public int Delete(Departments departments)
        //{

        //    _dbContext.departments.Remove(departments);
        //    return _dbContext.SaveChanges();//3dad row hasl leha affect

        //}

        //public Departments Get(int id)
        //{
        ////var department= _dbContext.departments.Local.Where(d => d.Id == id).FirstOrDefault();
        ////if(department == null)
        ////    {
        ////        department = _dbContext.departments.Where(d => d.Id == id).FirstOrDefault();

        ////    }

        ////return department;
        //return _dbContext.departments.Find(id);//dahhh


        //}

        //public IEnumerable<Departments> GetAll()

        //   => _dbContext.departments.ToList();


        //public int Update(Departments departments)
        //{
        //    _dbContext.departments.Update(departments);
        //    return _dbContext.SaveChanges();
        //} 
        #endregion
        public DepartmentRepository(MVCAppG04DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Departments> SearchByDepartmentByAddress(string Name)
        
         =>   _dbContext.departments.Where(E => E.Name.ToLower().Contains(Name.ToLower()));
        





    }
}
