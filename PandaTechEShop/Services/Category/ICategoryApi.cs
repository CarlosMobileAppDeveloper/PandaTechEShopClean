using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Category;
using Refit;

namespace PandaTechEShop.Services.Category
{
    public interface ICategoryApi
    {
        [Get("/api/categories")]
        Task<List<CategoryInfo>> GetCategories([Authorize("Bearer")] string token);
    }
}
