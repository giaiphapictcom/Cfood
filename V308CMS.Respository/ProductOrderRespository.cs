using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Respository
{
    public interface IProductOrderRespository
    {
        List<ProductOrder> GetListOrder(byte searchType,string keyword,byte status,
            DateTime? startDate, DateTime? endDate, out int totalRecord, int page =1, int pageSize=25);
    }
    public  class ProductOrderRespository: IProductOrderRespository
    {
        public List<ProductOrder> GetListOrder(
            byte searchType, string keyword, byte status,
            DateTime? startDate, DateTime? endDate,out int totalRecord, int page = 1, int pageSize = 25)
        {
            using (var entities = new V308CMSEntities())
            {
                var listOrder = (from order in entities.ProductOrder
                    select order
                    );
                if (status > 0)
                {
                    listOrder = (from order in entities.ProductOrder
                        where order.Status == status
                        select order
                        );
                }
                if (startDate.HasValue && endDate.HasValue && startDate<=endDate)
                {
                    if (startDate == endDate)
                    {
                        listOrder = (from order in entities.ProductOrder
                            where order.Date == startDate
                            select order
                            );
                    }
                    else
                    {
                        listOrder = (from order in entities.ProductOrder
                                     where order.Date>= startDate && order.Date<=endDate
                                     select order
                            );
                    }
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    var keywordLower = keyword.ToLower();
                    if (searchType == (byte)OrderSearchTypeEnum.All)
                    {

                        listOrder = (from order in entities.ProductOrder
                                     where order.FullName.ToLower().Contains(keywordLower) ||
                                     order.Phone.ToLower().Contains(keywordLower) ||
                                     order.Address.ToLower().Contains(keywordLower)
                                     select order
                               );
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByName)
                    {

                        listOrder = (from order in entities.ProductOrder
                                     where order.FullName.ToLower().Contains(keywordLower)                                  
                                     select order
                               );
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByPhone)
                    {

                        listOrder = (from order in entities.ProductOrder
                                     where order.Phone.ToLower().Contains(keywordLower)
                                     select order
                               );
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByAddress)
                    {

                        listOrder = (from order in entities.ProductOrder
                                     where order.Address.ToLower().Contains(keywordLower)
                                     select order
                               );
                    }
                }
                totalRecord = listOrder.Count();
                return (from order in listOrder.Include("OrderDetail")
                    orderby order.Date descending
                    select order
                    ).Skip((page-1)*pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            
        }
    }
}
