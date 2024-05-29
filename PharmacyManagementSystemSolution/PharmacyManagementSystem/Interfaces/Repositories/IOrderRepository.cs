using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;

namespace PharmacyManagementSystem.Interfaces.Repositories
{
    public interface IOrderRepository<K, T> where T : class
    {
        public Task<T> Add(T item);
        public Task<T> Delete(K key);
        public Task<T> Update(T item);
        public Task<T> Get(K key);
        public Task<IEnumerable<T>> Get();
        public Task<T> CancelOrder(K categoryId);
    }
}
