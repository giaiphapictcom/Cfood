using System;
using V308CMS.Data.Models;
using V308CMS.Data.Helpers;
using System.Linq;


namespace V308CMS.Data
{
    public interface RevenueGainRepositoryInterfaceRepository
    {
        string Add(int product_id, float value, int uid);
    }
    public class RevenueGainRepository: RevenueGainRepositoryInterfaceRepository
    {
        public string Add(int product_id, float value, int uid)
        {
            try
            {
                
                using (var entities = new V308CMSEntities())
                {
                    var product = entities.Product.Where(p => p.ID == product_id).Select(p=>p).FirstOrDefault();
                    var RevenueLatest = ItemLatest(product_id);
                    if (RevenueLatest != null && RevenueLatest.value == value) {
                        return Result.Ok;
                    }
                    if (product != null) {
                        var revenue = new RevenueGain
                        {
                            product_id = product_id,
                            value = value,
                            created_by = uid,
                            created = DateTime.Now
                        };
                        entities.RevenueGainTbl.Add(revenue);

                        product.Npp = double.Parse(value.ToString());
                        entities.SaveChanges();
                        return Result.Ok;
                    }
                    return Result.NotExists;
                        
                }
                
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public RevenueGain ItemLatest(int product_id) {
            using (var entities = new V308CMSEntities())
            {
                RevenueGain item = new RevenueGain();
                try {
                    item = entities.RevenueGainTbl.Where(r => r.product_id == product_id)
                        .OrderByDescending(p => p.created)
                        .Select(r => r).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return item;
            }
        }
    }
}