using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IAffilateUserRespository
    {
        AffilateUser GetByUserId(int userId);
        string CheckAffilateId(int userId, string affilateId);    
    }
    public class AffilateUserRespository: IAffilateUserRespository
    {
        public AffilateUser GetByUserId(int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.AffilateUser.FirstOrDefault(affilateUser => affilateUser.UserId == userId && affilateUser.Status == (byte)StateEnum.Active);

            }
        }

        public string CheckAffilateId(int userId, string affilateId)
        {
            using (var entities = new V308CMSEntities())
            {
                var affilateItem =  entities.AffilateUser.FirstOrDefault(affilateUser => affilateUser.UserId == userId);
                if (affilateItem == null)
                {
                    return "not_exists";
                }
                return (affilateItem.AffilateId == affilateId?"ok": "not_exists");
                

            }
        }

        

        public AffilateUser GetByUserIdWithUser(int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                return entities.AffilateUser.Include("User").FirstOrDefault(affilateUser => affilateUser.UserId == userId);

            }
        }


    }
}
