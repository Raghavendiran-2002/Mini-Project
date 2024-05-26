using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.Repositories;
using PharmacyManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementUnitTest
{
    public  class ProductServiceTest
    {
        DBPharmacyContext context;
        IProductRepository<int, Product> product;
        IProductService productService;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                   .UseInMemoryDatabase("dummyDsB");
            context = new DBPharmacyContext(optionsBuilder.Options);
            product = new ProductRepository(context);
            productService = new ProductService(product);
            product.Add(new Product() { ProductName = "meds", Description = "meds are usefull", Price = 40, Stock = 5, CategoryID = 1, DiscountID = 1 });
        }
    }
}
