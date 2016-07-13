using System;

namespace IWantICan.Core.Models
{
    public class CategoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shown { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
