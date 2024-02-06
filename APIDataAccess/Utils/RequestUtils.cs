using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Utils
{
    public class RequestUtils
    {
        public static string GetQueryPath(Dictionary<string, string>? param)
        {
            if (param == null)
                return "";
            string path = "?";
            foreach (var item in param)
            {
                path += item.Key;
                path += "=" + item.Value;
                path += "&";
            }
            path = path.Remove(path.Length - 1);
            return path;
        }

        public static void AddTokenHeader(HttpClient client, string? token = null)
        {
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
