using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public interface INewsGroupRepository
    {
        List<NewsGroups> GetListRoot();
        List<NewsGroups> GetListParent(int rootId = 0);

        List<NewsGroups> GetList(string keyword = "", int rootId = 0, int parentId = 0, int childId = 0, int page = 1,
            int pageSize = 10);
    }

    public class NewsGroupRepository: INewsGroupRepository
    {
        private readonly V308CMSEntities _entities;

        public NewsGroupRepository(V308CMSEntities entities)
        {
            _entities = entities;
        }

        public List<NewsGroups> GetListRoot()
        {
            return (from item in _entities.NewsGroups
                where item.Parent == 0
                    orderby  item.ID descending 
                select item
                ).ToList();
        }
        public List<NewsGroups> GetListParent(int rootId = 0)
        {
            
            return rootId==0? 
                 new List<NewsGroups>() : 
                (from item in _entities.NewsGroups
                    where item.Parent == rootId
                 orderby item.ID descending
                    select item
                ).ToList();
        }
        public List<NewsGroups> GetList(string keyword = "", int rootId = 0, int parentId = 0, int childId = 0, int page = 1, int pageSize = 10)
        {
            var listGroup = (from newsGroup in _entities.NewsGroups.AsEnumerable()
                             orderby newsGroup.ID descending
                             select newsGroup);
            //Loc theo tu khoa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var keywordLower = keyword.ToLower();
                listGroup = (from newsGroup in listGroup
                             where newsGroup.Name.ToLower().Contains(keywordLower)
                             orderby newsGroup.ID descending
                             select newsGroup);
            }
            //Loc theo childId
            if (childId > 0)
            {
                return (from newsGroup in listGroup
                    where newsGroup.ID == childId
                    orderby newsGroup.ID descending
                    select newsGroup).ToList();
            }
            //Loc theo ParentId
            if (parentId > 0)
            {
              return (from newsGroup in listGroup
                             where newsGroup.Parent == parentId
                             orderby newsGroup.ID descending
                             select newsGroup).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            //Loc theo RootId
            if (rootId > 0)
            {
                var listParent = (from newsGroup in listGroup
                                  where newsGroup.Parent == rootId
                                  orderby newsGroup.ID descending
                                  select newsGroup.ID).ToList();
                if (listParent.Count > 0)
                {
                    var listParentString = string.Join(",", listParent.ToArray());
                    listGroup = (from newsGroup in listGroup.AsEnumerable()
                        where newsGroup.Parent>0 &&((newsGroup.Parent == rootId) ||(listParentString.Contains(newsGroup.Parent + ",")
                              || listParentString.Contains("," + newsGroup.Parent)) ) 
                        orderby newsGroup.ID descending
                        select newsGroup);
                }
                else
                {
                    return new List<NewsGroups>();
                }
             
            }
            return listGroup.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }
    }
}