using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
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
            if(item == null) throw new ItemCannotBeNull($"{nameof(item)} cannot be null");
            _entities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async virtual Task<T> Delete(K key)
        {
            var obj = await Get(key);
            if(obj != null)
            {
                _entities.Remove(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            throw new ItemCannotBeNull($"{key} cannot be null");
        }

        public async virtual Task<T> Get(K key)
        {
            var obj = await _entities.FindAsync(key);
            if(obj == null)
            {
                throw new ItemCannotBeNull($"{key} cannot be null");
            }
            return obj;
        }

        public async virtual Task<IEnumerable<T>> Get()
        {
            return await _entities.ToListAsync();
        }

        public async virtual Task<T> Update(T item)
        {
            if (item == null)
            {
                throw new ItemCannotBeNull($"{nameof(item)} cannot be null");
            }
            _entities.Update(item);
            await _context.SaveChangesAsync();
            return item;

        }
    }
}
