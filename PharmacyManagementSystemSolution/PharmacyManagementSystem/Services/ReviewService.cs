﻿using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Exceptions.Review;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ReviewDTOs;

namespace PharmacyManagementSystem.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository<int, Review> _reviewRepo;
        private readonly IOrderRepository<int, Order> _orderRepo;
        private readonly IProductRepository<int, Product> _productRepo;

        public ReviewService(IReviewRepository<int ,Review> reviewRepo, IProductRepository<int , Product> productRepo, IOrderRepository<int ,Order> orderRepo)
        {
            _reviewRepo = reviewRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;                
        }

        public  async Task<Review> AddReview(int userId, ReviewCreationDto reviewDto)
        {          
            var product = (await _productRepo.Get()).FirstOrDefault(r => r.ProductID==reviewDto.ProductID);
            if (product == null)
            {
                throw new ProductNotFound("Product not found.");
            }


            var hasPurchasedProduct = await _orderRepo.PurchasedProductForReview(userId, reviewDto);

            if (!hasPurchasedProduct)
            {
                throw new UserNotPurchasedProduct("User has not purchased this product.");
            }

            var review = new Review
            {
                UserID = userId,
                ProductID = reviewDto.ProductID,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,                
            };

            review = await _reviewRepo.Add(review);
            return review;
        }

        public  async Task<IEnumerable<ReviewDto>> GetReviewsForProduct(int productId)
        {
            var reviews = (await _reviewRepo.Get())
           .Where(r => r.ProductID == productId)
           .Select(r => new ReviewDto
           {
               UserID = r.UserID,
               ProductID = r.ProductID,
               Rating = r.Rating,
               Comment = r.Comment
           })
           .ToList();

            return reviews;
        }
    }
}
