using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Helpers
{
    public class HelperFeature
    {
        private static HelperFeature _instance;
        public static HelperFeature Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HelperFeature();
                }
                return _instance;
            }
        }

        public async Task<string> CallApiAsync(string api, string accessToken)
        {
            string responseData = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response = await client.GetAsync(api);

                    // Kiểm tra xem yêu cầu có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc nội dung của phản hồi
                        responseData = await response.Content.ReadAsStringAsync();

                    }
                    else
                    {
                        Console.WriteLine("Lỗi khi gọi API. Mã trạng thái: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return responseData;
        }

        public async Task<string> CallApiAsyncPost<T>(string apiUrl, string accessToken, T postData)
        {
            string responseData = "";
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    // Tạo nội dung cần gửi đi (nếu có)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Gửi request POST
                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, postData);

                    // Kiểm tra xem request có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc và hiển thị dữ liệu trả về (nếu có)
                        responseData = await response.Content.ReadAsStringAsync();

                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return responseData;
        }


        public async Task<string> CallApiAsyncPut<T>(string apiUrl, string accessToken, T data)
        {
            string responseData = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {


                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return responseData;
        }

        public async Task<string> CallApiAsyncDelete(string apiUrl, string accessToken)
        {
            string responseData = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return responseData;
        }
    }
}