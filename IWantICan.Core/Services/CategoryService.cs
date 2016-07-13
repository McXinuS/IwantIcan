using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using IWantICan.Core.Services.Api;

namespace IWantICan.Core.Services
{
    public class CategoryService : ICategoryService
    {
        RestManager _restManager;
        List<CategoryModel> _categories;

        List<CategoryModel> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _restManager = new RestManager();
                    _categories = Task.Run(() => _restManager.GetCategoryListAsync()).Result;
                }
                return _categories;
            }
        }

        public int[] Selected { get; set; } = {1};

        public List<CategoryModel> GetCategoryList()
        {
            return Categories;
        }

        public CategoryModel GetCategory(int id)
        {
            if (Categories == null) return null;

            for (var i = 0; i < Categories.Count; i++)
                if (Categories[i].id == id)
                    return Categories[i];

            return null;
        }
    }
}
