namespace PharmacyManagementSystem.Interfaces.Repositories
{
    public interface IProductRepository<K, T> where T : class
    {

        public Task<T> Add(T item);
        public Task<T> Delete(K key);
        public Task<T> Update(T item);
        public Task<T> Get(K key);
        public Task<IEnumerable<T>> Get();
        public Task<IEnumerable<T>> GetProductByCategory(K categoryId);

        public Task<IEnumerable<T>> GetProductsByPriceRange(K startPriceRange, K endPriceRange);

        public Task<IEnumerable<T>> GetAvailableProducts();

        public Task<IEnumerable< T>> GetProductByName(string name);
    }
}
