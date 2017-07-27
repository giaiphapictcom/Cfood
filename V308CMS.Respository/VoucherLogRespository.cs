using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
            try
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
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }


            }
            
        }
    }
}
