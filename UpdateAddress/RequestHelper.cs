using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace UpdateAddress
{
    public enum RequestType
    {
        Get = 0,
        Post = 1
    }
    public class RequestHelper
    {
        private RequestType _requestType;
        private string _url;
        private NameValueCollection _parameters;
        public RequestHelper()
        {
            _parameters = new NameValueCollection();
        }
        public RequestHelper(string url)
            : this()
        {
            _url = url;
        }

        public RequestHelper(string url, NameValueCollection parameters)
            : this(url)
        {
            _parameters = parameters;
        }

        public RequestHelper(string url, NameValueCollection parameters, RequestType requestType)
            : this(url, parameters)
        {
            _requestType = requestType;
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;

            }
        }

        public RequestType RequestType
        {
            get
            {
                return _requestType;
            }
            set
            {
                _requestType = value;

            }
        }

        public NameValueCollection Parameters
        {
            get
            {
                return _parameters;

            }
            set
            {
                _parameters = value;

            }
        }

        public string Send(string url)
        {
            _url = url;
            return Send();
        }

        public string Send()
        {
            var parameters = new StringBuilder();
            for (int i = 0; i < _parameters.Count; i++)
            {
                EncodeAndAddItem(ref parameters, _parameters.GetKey(i), _parameters[i]);
            }
            string result = SendRequest(_url, parameters.ToString());
            return result;
        }

        private void EncodeAndAddItem(ref StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null)
            {
                baseRequest = new StringBuilder();
            }
            if (baseRequest.Length != 0)
            {
                baseRequest.Append("&");
            }
            baseRequest.Append(key);
            baseRequest.Append("=");
            baseRequest.Append(System.Web.HttpUtility.UrlEncode(dataItem));

        }




        public string SendRequest(string url, string postData)
        {
            HttpWebRequest request = null;
            if (RequestType == RequestType.Post)
            {
                var uri = new Uri(url);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                using (Stream writeStream = request.GetRequestStream())
                {
                    var encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(postData);

                    writeStream.Write(bytes, 0, bytes.Length);
                    writeStream.Close();
                }
            }
            else
            {
                var uri = new Uri(url + "?" + postData);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";

            }
            string result = string.Empty;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var readStream = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                result = readStream.ReadToEnd();
                            }
                        }

                    }
                }
            }
            catch (System.Net.WebException exception)
            {

                result = exception.Status.ToString();
            }

            return result;
        }


    }
}
