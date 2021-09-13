using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Product;
using Refit;

namespace PandaTechEShop.Services.Product
{
    public interface IProductApi
    {
        [Get("/api/products/{productId}")]
        Task<ProductInfo> GetProductById(int productId, [Authorize("Bearer")] string token);

        [Get("/api/products/productsbycategory/{categoryId}")]
        Task<List<ProductInfo>> GetProductsByCategory(int categoryId, [Authorize("Bearer")] string token);

        [Get("/api/products/trendingproducts")]
        Task<List<TrendingProduct>> GetTrendingProducts([Authorize("Bearer")] string token);
    }
}
