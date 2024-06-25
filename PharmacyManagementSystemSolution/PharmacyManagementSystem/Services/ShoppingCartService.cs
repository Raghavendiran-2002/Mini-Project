using PharmacyManagementSystem.Exceptions.ShoppingCart;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs;

namespace PharmacyManagementSystem.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository<int, ShoppingCart> _shoppingRepo;
        private readonly IShoppingCartItemsRepository<int, ShoppingCartItem> _shoppingCartItem;
        private readonly IProductRepository<int, Product> _productRepo;

        public ShoppingCartService(IShoppingCartRepository<int, ShoppingCart> shoppingRepo, IShoppingCartItemsRepository<int, ShoppingCartItem> shoppingCartItem, IProductRepository<int, Product> productRepo)
        {
            _shoppingRepo = shoppingRepo;
            _shoppingCartItem = shoppingCartItem;
            _productRepo = productRepo;
        }

        public async Task<ShoppingCartItem> AddItemToCart(AddShoppingCartItemDTO addShoppingCartDTO)
        {
            var product = await _productRepo.Get(addShoppingCartDTO.ProductID);
            if (product.Stock < addShoppingCartDTO.Quantity)
                throw new InvalidDataException("Quantity is not available");
            product.Stock -= addShoppingCartDTO.Quantity;
            await _productRepo.Update(product);
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
            var cart = await _shoppingRepo.Get(userId);
            return cart;
        }

        public async Task<ShoppingCartItem> RemoveItemFromCart(int ShoppingCartItemId)
        {
            var cartItem = await _shoppingCartItem.Get(ShoppingCartItemId);
            var product = await _productRepo.Get(cartItem.ProductID);
            product.Stock += cartItem.Quantity;
            await _productRepo.Update(product);
            var cart = await _shoppingCartItem.Delete(ShoppingCartItemId);
            if (cart == null)
                throw new CartItemNotFound("Cart item not found");
            return cart;
        }

        public async Task<ShoppingCartItem> UpdateItemInCart(UpdateItemInCartDTO updateItemInCartDTO)
        {
            ShoppingCartItem cartItem = (await _shoppingCartItem.Get(updateItemInCartDTO.CartItemID));
            if (cartItem == null)
                throw new CartItemNotFound("Cart item not found");
            ShoppingCart usercart = (await _shoppingRepo.Get(updateItemInCartDTO.CartID));
            cartItem = MapUpdateCartItemDTOtoShoppingCartItem(cartItem, updateItemInCartDTO);
            cartItem = await _shoppingCartItem.Update(cartItem);
            return cartItem;
        }

        public async Task<int> DeleteCartItemByUserId(int cartId)
        {
            var cart = await _shoppingRepo.Get(cartId);
            if (cart == null)
                throw new CartItemNotFound("Cart item not found");
            cart.ShoppingCartItems.Clear();
            await _shoppingRepo.Update(cart);
            return cartId;
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
