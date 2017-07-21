using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface IProductOrderRespository
    {
        List<ProductOrder> GetListOrder(byte searchType,string keyword,int status,
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
            byte searchType, string keyword, int status,
            DateTime startDate, DateTime endDate,out int totalRecord, int page = 1, int pageSize = 25)
        {
            using (var entities = new V308CMSEntities())
            {
                IEnumerable<ProductOrder> listOrder = (from order in entities.ProductOrder select order );
                if (status > 0)
                {
                    listOrder = listOrder.Where(o=>o.Status == status);
                    //listOrder = (from order in listOrder
                    //             where order.Status == status
                    //    select order
                    //    );
                }
                if ((startDate != DateTime.MinValue) && startDate<=endDate)
                {
                    if (startDate == endDate)
                    {
                        //listOrder = (from order in listOrder
                        //             where order.Date == startDate
                        //    select order
                        //    );
                        listOrder = listOrder.Where(o=>o.Date == startDate);
                    }
                    else
                    {
                        //var endDateCheck = endDate.AddDays(1);
                        //listOrder = (from order in listOrder
                        //             where order.Date>= startDate && order.Date< endDateCheck
                        //             select order
                        //    );
                        listOrder = listOrder.Where(o => o.Date >= startDate && o.Date < endDate.AddDays(1));
                    }
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    var keywordLower = keyword.Trim().ToLower();
                    if (searchType == (byte)OrderSearchTypeEnum.All)
                    {

                        //listOrder = (from order in listOrder.AsEnumerable()
                        //             where order.FullName.ToLower().Contains(keywordLower) ||
                        //             order.Phone.ToLower().Contains(keywordLower) ||
                        //             order.Address.ToLower().Contains(keywordLower)
                        //             select order
                        //       );
                        listOrder = listOrder.Where(o=> o.FullName.ToLower().Contains(keywordLower) ||o.Phone.ToLower().Contains(keywordLower) || o.Address.ToLower().Contains(keywordLower));
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByName)
                    {

                        //listOrder = (from order in listOrder.AsEnumerable()
                        //             where order.FullName.ToLower().Contains(keywordLower)                                  
                        //             select order
                        //       );
                        listOrder = listOrder.Where(o=>o.FullName.ToLower().Contains(keywordLower));
                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByPhone)
                    {

                        //listOrder = (from order in listOrder.AsEnumerable()
                        //             where order.Phone.ToLower().Contains(keywordLower)
                        //             select order
                        //       );
                        listOrder = listOrder.Where(o => o.Phone.ToLower().Contains(keywordLower));

                    }
                    if (searchType == (byte)OrderSearchTypeEnum.ByAddress)
                    {

                        //listOrder = (from order in listOrder.AsEnumerable()
                        //             where order.Address.ToLower().Contains(keywordLower)
                        //             select order
                        //       );
                        listOrder = listOrder.Where(o => o.Address.ToLower().Contains(keywordLower));
                    }
                }
                totalRecord = listOrder.Count();

                if (pageSize > 0)
                {
                    return listOrder.OrderByDescending(o => o.Date).Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                }
                else {
                    return listOrder.OrderByDescending(o => o.Date).ToList();
                }
                

                //return (from order in listOrder
                //    orderby order.Date descending
                //    select order
                //    ).Skip((page-1)*pageSize)
                //    .Take(pageSize)
                //    .ToList();
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
            var items = new List<ProductOrder>();
            using (var entities = new V308CMSEntities())
            {
                try
                {
                    items = (from order in entities.ProductOrder
                             orderby order.Date descending
                             select order
                    ).Take(count)
                    .ToList();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            return items;
           
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

                    if (status == (int)OrderStatusEnum.Complete) {
                        var OrderDetailRepo = new ProductOrderDetailRespository();
                        var RevenueGainRepo = new RevenueGainRepository();

                        var ItemDetails = OrderDetailRepo.GetListOrderDetailByOrderId(orderUpdateDetail.ID);
                        double money = 0;
                        if (ItemDetails.Count > 0)
                        {
                            foreach (var d in ItemDetails)
                            {
                                var revenue = RevenueGainRepo.ItemLatest((int)d.item_id);
                                if (revenue != null) {
                                    money += (double)(revenue.value * d.item_price / 100);
                                }
                                
                            }
                        }
                        orderUpdateDetail.revenue = money;
                    }

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
                        try {
                            foreach (var orderItem in orderDelete.OrderDetail)
                            {
                                orderDelete.OrderDetail.Remove(orderItem);
                                entities.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);
                        }


                    }
                    entities.ProductOrder.Remove(orderDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public int OrderCountByManagers(List<int> members) {
            int value = 1;
            using (var entities = new V308CMSEntities())
            {
                var orders = entities.ProductOrder.Select(o => o);
                orders = orders.Where(o => members.Contains((int)o.AccountID));
                value = orders.Count();
            }
                
            return value;
        }

        public string UpdateRevenuePay(int id, double money)
        {
            using (var entities = new V308CMSEntities())
            {
                var order = entities.ProductOrder.Where(o => o.ID == id).FirstOrDefault();

                if (order != null)
                {
                    if (order.revenue < money) {
                        return Result.NotExists;
                    }

                    order.revenue_payed = (double)money;
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.NotExists;

            }
        }
    }
}
