using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    
    public class AffilateCodeRespository
    {

        public AffilateCode Find(string code)
        {
            using (var entities = new V308CMSEntities())
            {
               return entities.AffilateCode.FirstOrDefault(item => item.Code == code);
            }
        }
        public string Insert(string code, byte status, DateTime createdAt)
        {
            using (var entities = new V308CMSEntities())
            {
                var affilateCode =entities.AffilateCode.FirstOrDefault(item => item.Code == code);
                if (affilateCode == null)
                {
                    entities.AffilateCode.Add(new AffilateCode
                    {
                        Code = code,
                        Status = status,
                        CreatedAt = createdAt
                    });
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";

            }
        }

        public int limit = 40;
        public List<AffilateCode> items(out int total, int page = -1) {
            var items = new List<AffilateCode>();
            total = 0;
            using (var entities = new V308CMSEntities())
            {
                var CodeQuery = entities.AffilateCode.Where(c => c.Status == 1);
                CodeQuery = CodeQuery.Where(c => !entities.Account.Any(a => a.affiliate_code == c.Code));

                items = CodeQuery.OrderBy(c => c.Code).ToList();
                if (page > 0)
                {
                    total = items.Count();
                    items = items.Skip((page - 1) * limit).Take(limit).ToList();
                }
            }
            return items;
        }

    }
}
