using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Respository
{
    public interface IProductOrderRespository
    {
        List<ProductOrder> GetListOrder(byte searchType,string keyword,byte status,
            DateTime startDate, DateTime endDate, out int totalRecord, int page =1, int pageSize=25);

        ProductOrder FindToEdit(int id);
        List<ProductOrder> Take(int count =10);
        string UpdateDetail(int id, string detail);
        string ChangeStatus(int id, int status);
        string Delete(int id);        
    }
    public  class ProductOrderRespository: IProductOrderRespository
    {
        public List<ProductOrder> GetListOrder(
            byte searchType, string keyword, byte status,
            DateTime startDate, DateTime endDate,out int totalRecord, int page = 1, int pageSize = 25)
        {
            using (var entities = new V308CMSEntities())
            {
                IEnumerable<ProductOrder> listOrder = (from order in entities.ProductOrder
                    select order
                    );
                if (status > 0)
                {
                    listOrder = (from order in listOrder
                                 where order.Status == status
                        select order
                        );
                }
                if ((startDate != DateTime.MinValue) && startDate<=endDate)
                {
                    if (startDate == endDate)
                    {
                        listOrder = (from order in listOrder
                                     where order.Date == startDate
                            select order
                            );
                    }
                    else
                    {
                        var endDateCheck = endDate.AddDays(1);
                        listOrder = (from order in listOrder
                                     where order.Date>= startDate && order.Date< endDateCheck
                                     select order
                            );
                    }
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    var keywordLower = keyword.Trim().ToLower();
                    if (searchType == (byte)OrderSearchTypeEnum.All)
                    {

                        listOrder = (from order in listOrder.AsEnumerable()
                                     where order.FullName.ToLower().Contains(keywordLower) ||
                                     order.Phone.ToLower().Contains(keywordLower) ||
                                     order.Address.ToLower().Contains(keywordLower)
                                     select order
                               );
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByName)
                    {

                        listOrder = (from order in listOrder.AsEnumerable()
                                     where order.FullName.ToLower().Contains(keywordLower)                                  
                                     select order
                               );
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByPhone)
                    {

                        listOrder = (from order in listOrder.AsEnumerable()
                                     where order.Phone.ToLower().Contains(keywordLower)
                                     select order
                               );
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByAddress)
                    {

                        listOrder = (from order in listOrder.AsEnumerable()
                                     where order.Address.ToLower().Contains(keywordLower)
                                     select order
                               );
                    }
                }
                totalRecord = listOrder.Count();
                return (from order in listOrder
                    orderby order.Date descending
                    select order
                    ).Skip((page-1)*pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            
        }

        public ProductOrder FindToEdit(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from order in entities.ProductOrder.Include("OrderDetail")
                    where order.ID == id
                    select order
                    ).FirstOrDefault();
            }
        }

        public List<ProductOrder> Take(int count = 10)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from order in entities.ProductOrder
                 orderby order.Date descending
                 select order
                    ).Take(count)
                    .ToList();
            }
           
        }

        public string UpdateDetail(int id, string detail)
        {
            using (var entities = new V308CMSEntities())
            {
                var orderUpdateDetail = (from order in entities.ProductOrder
                                         where order.ID == id
                        select order
                    ).FirstOrDefault();
                if (orderUpdateDetail != null)
                {
                    orderUpdateDetail.Detail = detail;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string ChangeStatus(int id, int status)
        {
            using (var entities = new V308CMSEntities())
            {
                var orderUpdateDetail = (from order in entities.ProductOrder
                                         where order.ID == id
                                         select order
                                         ).FirstOrDefault();
                if (orderUpdateDetail != null)
                {
                    orderUpdateDetail.Status = status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var orderDelete = (from order in entities.ProductOrder.Include("OrderDetail")
                                   where order.ID == id
                                         select order
                                        ).FirstOrDefault();
                if (orderDelete != null)
                {
                    if (orderDelete.OrderDetail != null)
                    {
                        foreach (var orderItem in orderDelete.OrderDetail)
                        {
                            orderDelete.OrderDetail.Remove(orderItem);
                            entities.SaveChanges();
                        }
                        
                        
                    }
                    entities.ProductOrder.Remove(orderDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }
    }
}
