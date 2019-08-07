using Pixburb.CommonModel;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Implementation
{
    public class CategoryDataReader : ICategoryDataReader
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<List<Categories>> GetCategory()
        {
            
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Get_Category";
            var reader = cmd.ExecuteReader();

            List<Category> categories = new List<Category>();
            while (reader.Read())
            {
                Category category = new Category();
                category.Id = Convert.ToInt32(reader["Category_Id"]);
                if (reader["Parent_Id"] != DBNull.Value)
                {
                    category.ParentId = Convert.ToInt32(reader["Parent_Id"]); 
                }
                category.CategoryName = Convert.ToString(reader["CategoryName"]);
                category.Image.FileName = Convert.ToString(reader["ImageName"]);
                category.Image.FileContent = Encoding.ASCII.GetBytes(Convert.ToString((reader["ImageContent"])));
                categories.Add(category);
            }

            conn.Close();
            List<Categories> cat = new List<Categories>();
            cat = this.FillRecursive(categories, null);

            return Task.FromResult(cat);
        }

        public List<Categories> FillRecursive(List<Category> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ParentId.Equals(parentId)).Select(item => new Categories
            {
                Id = item.Id,
                ParentId = item.ParentId,
                CategoryName = item.CategoryName,
                SubCategories = FillRecursive(flatObjects, item.Id)
            }).ToList();
        }

        public Task<List<CategoryBase>> GetCategoryLOV()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Get_Category";
            var reader = cmd.ExecuteReader();

            List<CategoryBase> categories = new List<CategoryBase>();
            while (reader.Read())
            {
                CategoryBase category = new CategoryBase();
                category.Id = Convert.ToInt32(reader["Category_Id"]);
                category.CategoryName = Convert.ToString(reader["CategoryName"]);
                categories.Add(category);
            }

            conn.Close();
            return Task.FromResult(categories);
        }
    }
}
