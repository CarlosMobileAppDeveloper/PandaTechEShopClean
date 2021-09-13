using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Product;

namespace PandaTechEShop.Services.Product
{
    public interface IProductService
    {
        Task<ProductInfo> GetProductByIdAsync(int productId);
        Task<List<ProductInfo>> GetProductsByCategoryAsync(int categoryId);
        Task<List<TrendingProduct>> GetTrendingProductsAsync();
    }
}
