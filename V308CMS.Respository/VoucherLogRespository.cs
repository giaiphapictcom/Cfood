using System;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IVoucherLogRespository
    {
        void Log(int userId, string userName, int voucherId, string voucherCode,  DateTime createdAt);

    }
    public  class VoucherLogRespository: IVoucherLogRespository
    {
        public void Log(int userId, string userName, int voucherId, string voucherCode, DateTime createdAt)
        {
            using (var entities = new V308CMSEntities())
            {
                var voucherLog = new VoucherLog
                {
                    UserId = userId,
                    UserName = userName,
                    VoucherId = voucherId,
                    VoucherCode = voucherCode,
                    CreatedAt = createdAt
                };
                entities.VoucherLog.Add(voucherLog);
                entities.SaveChanges();

            }
        }
    }
}
