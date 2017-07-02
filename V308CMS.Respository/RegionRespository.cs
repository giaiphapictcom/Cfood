using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IRegionRespository
    {
        List<Region> GetListRegionByParentId(int parentId = 0);
        string Insert(string name, string code, int parentId);
        List<Region> GetListIn(int regionId, int cityId, int wardId);
    }
    public class RegionRespository: IRegionRespository
    {
        public List<Region> GetListRegionByParentId(int parentId = 0)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.Region.Where(region => region.ParentId == parentId)
                        .OrderBy(region => region.Id)
                        .ToList();
            }
        }
        public string Insert(string name, string code, int parentId) 
        {
            using (var entities = new V308CMSEntities())
            {
                var checkRegion = entities.Region.FirstOrDefault(region => region.Name == name && region.ParentId == parentId);
                if (checkRegion == null)
                {
                    var newRegion = new Region
                    {
                        Name = name,
                        Code = code,
                        ParentId = parentId
                    };
                    entities.Region.Add(newRegion);
                    entities.SaveChanges();
                    return newRegion.Id.ToString();
                }
                return "exists";
            }
        }

        public List<Region> GetListIn(int regionId, int cityId, int wardId)
        {
            using (var entities = new V308CMSEntities())
            {   return
                    entities.Region.AsEnumerable()
                    .Where(region=>region.Id == regionId || region.Id==cityId || region.Id ==wardId)
                    .OrderBy(region=>region.Id)
                        .ToList();
            }
        }
    }
}
