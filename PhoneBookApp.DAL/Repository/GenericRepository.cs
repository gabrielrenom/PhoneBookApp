using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;

        public DbSet<T> Entities
        {
            get => _entities;
            set => _entities = value;
        }

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
                throw new Exception("An error occurred while inserting the entity.", ex);
            }

            return entity;
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }

            return true;
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }

            return true;
        }
    }

}
