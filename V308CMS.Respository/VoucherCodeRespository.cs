using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IVoucherCodeRespository
    {
        VoucherCode Find(int id);
        VoucherCode Find(string code);
        VoucherCode FindWithVoucher(int id);
        VoucherCode FindFirst(string code);
        VoucherCode FindFirstWithVoucher(string code);
        string Apply(string code);


    }
    public class VoucherCodeRespository: IVoucherCodeRespository
    {
        public VoucherCode Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.VoucherCode.FirstOrDefault(voucherCode => voucherCode.Id == id);
            }
         }

        public VoucherCode Find(string code)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.VoucherCode.Include("Voucher").FirstOrDefault(voucherCode => voucherCode.Code == code);
            }
        }


        public VoucherCode FindWithVoucher(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.VoucherCode.Include("Voucher").FirstOrDefault(voucherCode => voucherCode.Id == id);
            }
        }

        public VoucherCode FindFirst(string code)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.VoucherCode.FirstOrDefault(voucherCode => voucherCode.Code == code);
            }
        }

        public VoucherCode FindFirstWithVoucher(string code)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.VoucherCode.Include("Voucher").FirstOrDefault(voucherCode => voucherCode.Code == code);
            }
        }

        public string Apply(string code)
        {
            using (var entities = new V308CMSEntities())
            {
                var voucherCodeApply = entities.VoucherCode.FirstOrDefault(voucherCode => voucherCode.Code == code);
                if (voucherCodeApply == null)
                {
                    return "not_exist";

                }
                voucherCodeApply.State = (byte) StateEnum.Active;
                entities.SaveChanges();
                return "ok";
            }

        }
    }
}
