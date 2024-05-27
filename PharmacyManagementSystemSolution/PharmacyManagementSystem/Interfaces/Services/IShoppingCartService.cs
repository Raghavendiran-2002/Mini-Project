using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCart> AddItemToCart(AddShoppingCartItemDTO addShoppingCartDTO);
        public Task<IEnumerable< ShoppingCart>> GetAllCartByUserId(int userId);

        public Task<ShoppingCart> UpdateItemInCart(UpdateItemInCartDTO updateItemInCartDTO);
        public Task<ShoppingCart> RemoveItemFromCart(int cartId);
    }
}
