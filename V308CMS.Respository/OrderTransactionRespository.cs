using System;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IOrderTransactionRespository
    {
        string Create(int orderId, string transactionId, DateTime createdAt, byte status);
        OrderTransaction GetByTransactionId(string transactionId);
        string CompleteTransaction(string transactionId, DateTime finishAt, byte status = (byte)TransactionEnum.Finish);
    }
    public class OrderTransactionRespository: IOrderTransactionRespository
    {
        public string Create(int orderId, string transactionId, DateTime createdAt, byte status)
        {
            using (var entities = new V308CMSEntities())
            {
                var transtaction = entities.OrderTransaction
                      .FirstOrDefault(transaction => transaction.TransactionId == transactionId);
                if (transtaction == null)
                {
                    var newTranstaction = new OrderTransaction
                    {
                        OrderId = orderId,
                        TransactionId = transactionId,
                        CreatedAt = createdAt,
                        Status = status
                    };
                    entities.OrderTransaction.Add(newTranstaction);
                    entities.SaveChanges();
                    return newTranstaction.Id.ToString();
                }
                return transtaction.Id.ToString();
            }
          
        }

        public OrderTransaction GetByTransactionId(string transactionId)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.OrderTransaction
                    .Include("Order").FirstOrDefault(transaction => transaction.TransactionId == transactionId);
            }
        }

        public string CompleteTransaction(string transactionId, DateTime finishAt, byte status = 1 )
        {
            using (var entities = new V308CMSEntities())
            {
                var transtaction = entities.OrderTransaction
                    .FirstOrDefault(transaction => transaction.TransactionId == transactionId);
                if (transtaction != null)
                {
                    transtaction.Status = status;
                    transtaction.FinishAt = finishAt;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
        }
    }
}
