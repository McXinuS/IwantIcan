using System.Collections.Generic;
using System.Linq;
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
                if (_categories?.Count > 0)
                    return _categories;

                _restManager = new RestManager();
                _categories = Task.Run(() => _restManager.GetCategoryListAsync()).Result;
                return _categories;
            }
        }

        public int[] Selected { get; set; } = {};

        public List<CategoryModel> GetCategoryList()
        {
            return Categories;
        }

        public CategoryModel GetCategory(int id)
        {
            return Categories?.FirstOrDefault(t => t.id == id);
        }

        public int IndexOf(int id)
        {
            for (var i = 0; i < Categories.Count; i++)
            {
                if (Categories[i].id == id)
                    return i;
            }

            return -1;
        }
    }
}
