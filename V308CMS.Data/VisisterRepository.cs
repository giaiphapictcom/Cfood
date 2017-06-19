using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;
using System.Net.Http;

namespace V308CMS.Data
{
    public interface VisisterInterface
    {
        void update(int uid = 0);
    }

    public class VisisterRepository : VisisterInterface
    {

        private V308CMSEntities entities;
        private VisisterRepository tbl;
        #region["Contructor"]

        public VisisterRepository()
        {
            this.entities = new V308CMSEntities();
            this.tbl = new VisisterRepository();
        }

        public VisisterRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;

        }

        #endregion
        #region["Vung cac thao tac Dispose"]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.entities != null)
                {
                    this.entities.Dispose();
                    this.entities = null;
                }
            }
        }
        #endregion

        public void update(int uid = 0)
        {
            
            try
            {
                

                var visister = new Visister();
                visister.ip_address = HttpContext.Current.Request.UserHostAddress;
                visister.ip_address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                visister.useragent = System.Web.HttpContext.Current.Request.UserAgent;
                visister.platform = System.Web.HttpContext.Current.Request.Browser.Platform;
                visister.browser = System.Web.HttpContext.Current.Request.Browser.Id;

                visister.timestamp = DateTime.Now;
                visister.host = System.Web.HttpContext.Current.Request.Url.Host;
                visister.uid = uid;

                var item = from v in entities.VisisterTbl where v.ip_address == visister.ip_address select v;
                if ( item.Count() < 1)
                {
                    entities.VisisterTbl.Add(visister);
                    entities.SaveChanges();
                }
                

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        //private const string HttpContext = "MS_HttpContext";
        //private const string RemoteEndpointMessage =
        //    "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        //private const string OwinContext = "MS_OwinContext";

        //public static string GetClientIpAddress()
        //{
        //    var request = HttpRequestMessage;
        //    // Web-hosting. Needs reference to System.Web.dll
        //    if (request.Properties.ContainsKey(HttpContext))
        //    {
        //        dynamic ctx = request.Properties[HttpContext];
        //        if (ctx != null)
        //        {
        //            return ctx.Request.UserHostAddress;
        //        }
        //    }

        //    // Self-hosting. Needs reference to System.ServiceModel.dll. 
        //    if (request.Properties.ContainsKey(RemoteEndpointMessage))
        //    {
        //        dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
        //        if (remoteEndpoint != null)
        //        {
        //            return remoteEndpoint.Address;
        //        }
        //    }

        //    // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
        //    if (request.Properties.ContainsKey(OwinContext))
        //    {
        //        dynamic owinContext = request.Properties[OwinContext];
        //        if (owinContext != null)
        //        {
        //            return owinContext.Request.RemoteIpAddress;
        //        }
        //    }

        //    return null;
        //}
    }
}
