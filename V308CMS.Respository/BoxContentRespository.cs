using System.Collections.Generic;
using System.Linq;
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
        public List<BoxContent> GetListBoxContent(int subCategoryLimit =3, int productLimit =6)
        {
            var result = new List<BoxContent>();
            using (var entities = new V308CMSEntities())
            {
                var listHomeCategory = entities.ProductType
                        .Where(productType => productType.IsHome && productType.Status == true)
                        .OrderBy(productType => productType.Number)
                        .Select(productType=>productType)
                        .ToList();            
                if (listHomeCategory.Any())
                {
                    for (int i = 0,total = listHomeCategory.Count; i < total; i++)
                    {

                        var category = listHomeCategory[i];
                       
                        var listSubcategory = (from subCategory in entities.ProductType
                            where subCategory.Parent == category.ID && subCategory.Status ==true
                            orderby subCategory.Number
                            select  subCategory
                            ).Take(subCategoryLimit).ToList();
                        var boxContent = new BoxContent
                        {
                            Category = category,
                            ListSubCategory = listSubcategory
                        };

                        for (int k = 0, subTotal = listSubcategory.Count; k<subTotal; k++)
                        {
                            var subCategory = listSubcategory[k];
                            var boxContentItem = new BoxContentItem
                            {
                                SubCategory = subCategory,
                                Products = 
                                (from product in entities.Product.Include("ProductImages")
                                    where product.Status == true && product.Type == subCategory.ID
                                    orderby product.Number
                                    select product
                                    ).Take(productLimit).ToList()
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
