using System.Collections.Generic;
using IWantICan.Core.Models;
using Java.Security;

namespace IWantICan.Core.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get a list of categories.
        /// </summary>
        /// <returns>A list of categories.</returns>
        List<CategoryModel> GetCategoryList();

        /// <summary>
        /// Get information about the category.
        /// </summary>
        /// <param name="id">ID of the category.</param>
        /// <returns>The information about the category.</returns>
        CategoryModel GetCategory(int id);

        /// <summary>
        /// Current selected category.
        /// </summary>
        int[] Selected { get; set; }
    }
}
