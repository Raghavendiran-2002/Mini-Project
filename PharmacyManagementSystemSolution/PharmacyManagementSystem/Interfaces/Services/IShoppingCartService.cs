using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs;

namespace PharmacyManagementSystem.Interfaces.Services
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCartItem> AddItemToCart(AddShoppingCartItemDTO addShoppingCartDTO);
        public Task< ShoppingCart> GetCartByUserId(int userId);

        public Task<ShoppingCartItem> UpdateItemInCart(UpdateItemInCartDTO updateItemInCartDTO);
        public Task<ShoppingCartItem> RemoveItemFromCart(int cartId);
    }
}
