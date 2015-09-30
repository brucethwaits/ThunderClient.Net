using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThunderClient.Net
{
    public class Thunder
    {
        private string _server = "";
        private string _apiKey = "";
        private string _secretKey = "";
        private IWebProxy _proxy = null;
        private int? _timeout = null;

        public Thunder(string server, string apiKey, string secretKey, IWebProxy proxy = null, int? timeout = null)
        {
            _server = server;
            _apiKey = apiKey;
            _secretKey = secretKey;
            _proxy = proxy;
            _timeout = timeout;
        }

        private static string SerializeContentAsJSON(object content)
        {
            Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            jsonSerializer.Serialize(sw, content);
            string output = sb.ToString();

            output = output.Replace(@"\u0026", "&");

            return output;
        }

        public int SendMessageToChannel(object message, string channel)
        {
            string url = string.Format("http://{0}/api/1.0.0/{1}/channels/{2}/", _server, _apiKey, channel);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Thunder-Secret-Key", _secretKey);
            request.Method = "POST";
            request.ContentType = "application/json";
            
            if (_proxy != null)
            {
                request.Proxy = _proxy;
            }

            if (_timeout.HasValue)
            {
                request.Timeout = _timeout.Value;
            }

            string messageText = SerializeContentAsJSON(message);

            byte[] toBytes = Encoding.ASCII.GetBytes(messageText);

            Stream s = request.GetRequestStream();

            s.Write(toBytes, 0, toBytes.Length);

            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            JObject o = JObject.Parse(objText);

            return (int)o["count"];
        }

        public int GetUserCount()
        {
            string url = string.Format("http://{0}/api/1.0.0/{1}/users/", _server, _apiKey);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Thunder-Secret-Key", _secretKey);
            request.Method = "GET";

            if (_proxy != null)
            {
                request.Proxy = _proxy;
            }

            if (_timeout.HasValue)
            {
                request.Timeout = _timeout.Value;
            }

            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            JObject o = JObject.Parse(objText);

            return (int)o["count"];
        }

        public List<string> GetUsersInChannel(string channel)
        {
            string url = string.Format("http://{0}/api/1.0.0/{1}/channels/{2}/", _server, _apiKey, channel);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Thunder-Secret-Key", _secretKey);
            request.Method = "GET";

            if (_proxy != null)
            {
                request.Proxy = _proxy;
            }

            if (_timeout.HasValue)
            {
                request.Timeout = _timeout.Value;
            }

            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            JObject o = JObject.Parse(objText);

            return o["users"].Select(x => x.ToString()).ToList();
        }

        public int SendMessageToUser(object message, string user_id)
        {
            string url = string.Format("http://{0}/api/1.0.0/{1}/users/{2}/", _server, _apiKey, user_id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Thunder-Secret-Key", _secretKey);
            request.Method = "POST";
            request.ContentType = "application/json";

            if (_proxy != null)
            {
                request.Proxy = _proxy;
            }

            if (_timeout.HasValue)
            {
                request.Timeout = _timeout.Value;
            }

            string messageText = SerializeContentAsJSON(message);

            byte[] toBytes = Encoding.ASCII.GetBytes(messageText);

            Stream s = request.GetRequestStream();

            s.Write(toBytes, 0, toBytes.Length);

            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            JObject o = JObject.Parse(objText);

            return (int)o["count"];
        }

        public bool IsUserOnline(string user_id)
        {
            string url = string.Format("http://{0}/api/1.0.0/{1}/users/{2}/", _server, _apiKey, user_id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Thunder-Secret-Key", _secretKey);
            request.Method = "GET";

            if (_proxy != null)
            {
                request.Proxy = _proxy;
            }

            if (_timeout.HasValue)
            {
                request.Timeout = _timeout.Value;
            }

            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            JObject o = JObject.Parse(objText);

            return (bool)o["online"];
        }

        public void DisconnectUser(string user_id)
        {
            string url = string.Format("http://{0}/api/1.0.0/{1}/users/{2}/", _server, _apiKey, user_id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Thunder-Secret-Key", _secretKey);
            request.Method = "DELETE";
            request.ContentType = "application/json";

            if (_proxy != null)
            {
                request.Proxy = _proxy;
            }

            if (_timeout.HasValue)
            {
                request.Timeout = _timeout.Value;
            }

            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();
        }
    }
}
