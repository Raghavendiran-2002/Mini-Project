using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Exceptions.General;
using PharmacyManagementSystem.Interfaces.Repositories;

namespace PharmacyManagementSystem.Models.Repositories
{
    public abstract class BaseRepository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly DBPharmacyContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(DBPharmacyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();
        }

        public async virtual Task<T> Add(T item)
        {
            _entities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async virtual Task<T> Delete(K key)
        {
            var obj = await Get(key);                        
            _entities.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;           
        }

        public async virtual Task<T> Get(K key)
        {
            var obj = await _entities.FindAsync(key);            
            return obj;
        }

        public async virtual Task<IEnumerable<T>> Get()
        {
            return await _entities.ToListAsync();
        }

        public async virtual Task<T> Update(T item)
        {
            if (item == null)        
            _entities.Update(item);
            await _context.SaveChangesAsync();
            return item;

        }
    }
}
