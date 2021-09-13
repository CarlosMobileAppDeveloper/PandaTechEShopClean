using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Models.Category;

namespace PandaTechEShop.Services.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryInfo>> GetCategoriesAsync();
    }
}
