using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class

    {

        private protected readonly MVCAppG04DbContext _dbContext;
        public GenericRepository(MVCAppG04DbContext dbContext)
        {


            _dbContext = dbContext;
        }
        public async Task Add(T item)
        {
          await  _dbContext.Set<T>().AddAsync(item);
            //return _dbContext.SaveChanges();
        }

        public void Delete(T item)
        {

            _dbContext.Set<T>().Remove(item);
           // return _dbContext.SaveChanges();

        }

        public  async Task<T> Get(int id)
        

            => await _dbContext.Set<T>().FindAsync(id);


        

        public async Task <IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _dbContext.employees.Include(E => E.Departments).ToListAsync();
            }
            else

                return await _dbContext.Set<T>().ToListAsync();

        }
        public void Update(T item)
        {
            _dbContext.Set<T>().Update(item);
           // return _dbContext.SaveChanges();
        }


    }
}
