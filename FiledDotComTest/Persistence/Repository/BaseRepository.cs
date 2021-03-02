using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using FiledDotComTest.Persistence.DbConnection;
using System;

namespace FiledDotComTest.Persistence.Repository
{
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : FiledDbContext
    {
        public readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Get(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return null;
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return null;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<TEntity> Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public async Task<IDbContextTransaction> InitiateTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
