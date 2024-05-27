using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs;

namespace PharmacyManagementSystem.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository<int, ShoppingCart> _shoppingRepo;
        private readonly IRepository<int, ShoppingCartItem> _shoppingCartItem;
        private readonly IProductRepository<int, Product> _productRepo;

        public ShoppingCartService(IShoppingCartRepository<int, ShoppingCart> shoppingRepo, IRepository<int, ShoppingCartItem> shoppingCartItem, IProductRepository<int, Product> productRepo)
        {
            _shoppingRepo = shoppingRepo;
            _shoppingCartItem = shoppingCartItem;
            _productRepo = productRepo;
        }

        public async Task<ShoppingCartItem> AddItemToCart(AddShoppingCartItemDTO addShoppingCartDTO)
        {            
            ShoppingCartItem cartItem = MapAddShoppingCartItemDTOToShoppingCartItem(addShoppingCartDTO);
            var item = (await _shoppingCartItem.Add(cartItem));
            return item;
        }

        private ShoppingCartItem MapAddShoppingCartItemDTOToShoppingCartItem(AddShoppingCartItemDTO addShoppingCartDTO)
        {
            ShoppingCartItem cartItem = new ShoppingCartItem();
            cartItem.CartID = addShoppingCartDTO.CartID;
            cartItem.ProductID = addShoppingCartDTO.ProductID;
            cartItem.Quantity = addShoppingCartDTO.Quantity;
            return cartItem;
        }

        public async Task<ShoppingCart> GetCartByUserId(int userId)
        {
            var cartIt = await _shoppingRepo.Get(userId);
            return cartIt;
        }

        public async Task<ShoppingCartItem> RemoveItemFromCart(int ShoppingCartItemId)
        {
            var cart = await _shoppingCartItem.Delete(ShoppingCartItemId);
            return cart;
        }

        public async Task<ShoppingCartItem> UpdateItemInCart(UpdateItemInCartDTO updateItemInCartDTO)
        {
            ShoppingCartItem cartItem = (await _shoppingCartItem.Get(updateItemInCartDTO.CartItemID));
            cartItem = MapUpdateCartItemDTOtoShoppingCartItem(cartItem, updateItemInCartDTO);
            cartItem = await _shoppingCartItem.Update(cartItem);
            return cartItem;
        }

        private ShoppingCartItem MapUpdateCartItemDTOtoShoppingCartItem(ShoppingCartItem cartItem, UpdateItemInCartDTO updateItemInCartDTO)
        {
            cartItem.ProductID = updateItemInCartDTO.ProductID;
            cartItem.CartID = updateItemInCartDTO.CartID;
            cartItem.Quantity = updateItemInCartDTO.Quantity;
            return cartItem;
        }
    }
}
