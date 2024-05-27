using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs;

namespace PharmacyManagementSystem.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<int, ShoppingCart> _shoppingRepo;

        public ShoppingCartService(IRepository<int , ShoppingCart> shoppingRepo)
        {
            _shoppingRepo = shoppingRepo;
        }

        public async Task<ShoppingCart> AddItemToCart(AddShoppingCartItemDTO addShoppingCartDTO)
        {
            ShoppingCart shoppingCart = MapAddShoppingCartItemDTOToShoppingCart(addShoppingCartDTO);    
            var cart = await _shoppingRepo.Add(shoppingCart);
            return cart;
        }

        private ShoppingCart MapAddShoppingCartItemDTOToShoppingCart(AddShoppingCartItemDTO addShoppingCartDTO)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.UserID = addShoppingCartDTO.UserID;
            cart.ProductID = addShoppingCartDTO.ProductID;
            cart.Quantity = addShoppingCartDTO.Quantity;
            return cart;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllCartByUserId(int userId)
        {
            var cart = (await _shoppingRepo.Get()).Where(c => c.UserID == userId).ToList();
            return cart;
        }

        public async Task<ShoppingCart> RemoveItemFromCart(int cartId)
        {
            var cart =await _shoppingRepo.Delete(cartId);
            return cart;
        }

        public async Task<ShoppingCart> UpdateItemInCart(UpdateItemInCartDTO updateItemInCartDTO)
        {
            ShoppingCart cart =await _shoppingRepo.Get(updateItemInCartDTO.CartID);
            cart = MapUpdateItemInCartDTOToShoppingCart(cart , updateItemInCartDTO);
            var shoppingCart = await _shoppingRepo.Update(cart);
            return shoppingCart;
        }

        private ShoppingCart MapUpdateItemInCartDTOToShoppingCart(ShoppingCart cart, UpdateItemInCartDTO updateItemInCartDTO)
        {
            cart.ProductID = updateItemInCartDTO.ProductID;
            cart.UserID = updateItemInCartDTO.UserID;
            cart.Quantity = updateItemInCartDTO.Quantity;
            return cart;
        }
    }
}
