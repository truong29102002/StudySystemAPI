using Microsoft.Extensions.Logging;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.EF;
using StudySystem.Data.EF.Repositories.Interfaces;
using StudySystem.Data.Entites;
using StudySystem.Data.Models.Request;
using StudySystem.Data.Models.Response;
using StudySystem.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service
{
    public class CartService : BaseService, ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _currentUser;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        public CartService(ILogger<CartService> logger, IUnitOfWork unitOfWork, UserResoveSerive currentUser) : base(unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser.GetUser();
            _cartRepository = unitOfWork.CartRepository;
            _cartItemRepository = unitOfWork.CartItemRepository;
        }
        /// <summary>
        /// GetCart
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CartResponseModel> GetCart()
        {
            CartResponseModel cartResponseModel = new CartResponseModel();
            try
            {
                var rs = _cartRepository.GetCart(_currentUser);
                cartResponseModel = rs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return cartResponseModel;
        }
        /// <summary>
        /// InsertOrUpdateCart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateCart(CartInsertRequestModel model)
        {
            try
            {
                string cartId = StringUtils.NewGuid();
                var cartUser = await _cartRepository.InsertCartUser(_currentUser, cartId).ConfigureAwait(false);
                if (cartUser == null)
                {
                    await ProcessUpdateCartItem(model, cartId);
                }
                else
                {
                    await ProcessUpdateCartItem(model, cartUser.CartId);
                }
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }
        /// <summary>
        /// ProcessUpdateCartItem
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        private async Task ProcessUpdateCartItem(CartInsertRequestModel model, string cartId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            var oldItem = (await _cartItemRepository.FindAllAsync(x => x.CartId.Equals(cartId))).ToDictionary(x => x.ProductId);
            foreach (var item in model.CartInsertData)
            {
                
                int quantityNew = 0;
                if (oldItem.TryGetValue(item.ProductId, out var existingItem))
                {
                    quantityNew = existingItem.Quantity + item.Quantity;
                }
                else
                {
                    quantityNew = item.Quantity;

                }
                cartItems.Add(new CartItem
                {
                    CartId = cartId,
                    ProductId = item.ProductId,
                    Quantity = quantityNew,
                    Price = item.Price,
                    CreateUser = _currentUser,
                    UpdateUser = _currentUser
                });
            }
            await _unitOfWork.BulkUpdateAsync(cartItems).ConfigureAwait(false);
        }
    }
}
