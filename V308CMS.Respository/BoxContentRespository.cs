using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using V308CMS.Data;

namespace V308CMS.Respository
{
    
    public class BoxContentRespository
    {
        public List<Product> GetListProductByCategory(int subCateId, int productLimit =6)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from product in entities.Product.Include("ProductImages")
                 where product.Status == true
                 orderby product.Number
                 select product).Take(productLimit).ToList();
            }
            
        }
        public async Task<List<BoxContent>> GetListBoxContentAsync(int subCategoryLimit = 3, int productLimit = 6)
        {
            var result = new List<BoxContent>();
            using (var entities = new V308CMSEntities())
            {
                var listHomeCategory = await entities.ProductType
                        .Where(productType => productType.IsHome && productType.Status == true)
                        .OrderBy(productType => productType.Number)
                        .Select(productType => productType)
                        .ToListAsync();
                if (listHomeCategory.Any())
                {
                    for (int i = 0, total = listHomeCategory.Count; i < total; i++)
                    {

                        var category = listHomeCategory[i];

                        var listSubcategory = await (from subCategory in entities.ProductType
                                               where subCategory.Parent == category.ID && subCategory.Status == true
                                               orderby subCategory.Number
                                               select subCategory
                            ).Take(subCategoryLimit).ToListAsync();
                        var boxContent = new BoxContent
                        {
                            Category = category,
                            ListSubCategory = listSubcategory
                        };

                        for (int k = 0, subTotal = listSubcategory.Count; k < subTotal; k++)
                        {
                            var subCategory = listSubcategory[k];
                            var boxContentItem = new BoxContentItem
                            {
                                SubCategory = subCategory,
                                Products = await
                                (from product in entities.Product.Include("ProductImages")
                                            where product.Status == true && product.Type == subCategory.ID
                                            orderby product.Number
                                            select product
                                    ).Take(productLimit).ToListAsync()
                            };
                            boxContent.ListContentItem.Add(boxContentItem);

                        }
                        result.Add(boxContent);


                    }
                }
            }
            return result;
        }

    }
}
