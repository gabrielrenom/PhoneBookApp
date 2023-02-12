using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.DAL.Repository
{

    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _entities.AsEnumerable();
        }
        
        public async Task<T> GetById(int id)
        {
            return _entities.Find(id);
        }

        public async Task<T> Insert(T entity)
        {
            try
            {
                _entities.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return entity;
        }

        public async Task<bool> Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
