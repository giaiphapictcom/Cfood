using System;
using System.Linq;
using V308CMS.Data.Helpers;
using V308CMS.Data.Models;

namespace V308CMS.Data
{
    public interface SubscribeInterfaceRepository
    {
        string Insert(string email);
    }
    public class SubscribeRepository : SubscribeInterfaceRepository
    {
        public string Insert(string email)
        {
            try
            {
                using (var entities = new V308CMSEntities()) {
                    var item = new Subscribe
                    {
                        Email = email,
                    };
                    entities.SubscribeTbl.Add(item);
                    entities.SaveChanges();

                    return item.Id.ToString();
                }
                    
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool CheckSubscribed() {
            bool subscribed = false;
            using (var entities = new V308CMSEntities())
            {
                var VisisterRepo = new VisisterRepository();
                int VisisterId = VisisterRepo.CurrentVisisterId();
                var subscribe = entities.SubscribeTbl.Where(s => s.Visister == VisisterId).Select(s => s).FirstOrDefault();
                if (subscribe != null && subscribe.Id > 0) {
                    subscribed = true;
                }

            }
            return subscribed;
        }
    }


}