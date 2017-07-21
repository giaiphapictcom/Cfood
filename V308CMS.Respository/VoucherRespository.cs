using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IVoucherRespository
    {
        Voucher Find(int id);
    }
    public  class VoucherRespository: IVoucherRespository
    {
        public Voucher Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.Voucher.FirstOrDefault(voucherCode => voucherCode.Id == id);
            }
        }
    }
}
