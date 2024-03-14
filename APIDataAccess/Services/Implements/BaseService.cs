using APIDataAccess.DTO;
using APIDataAccess.DTO.Odata;
using APIDataAccess.DTO.ResponseModels.Helpers;
using APIDataAccess.Utils;
using System.Net.Http.Json;

namespace APIDataAccess.Services.Implements
{
    public abstract class BaseService : IBaseService
    {
        protected HttpClient Client { get; set; } = null!;
        protected StoredURI Path { get; set; } = StoredURI.None;
        public BaseService(IHttpClientFactory clientFactory)
        {
            Client = clientFactory.CreateClient("BaseClient");
        }

        public virtual async Task<ResponseMessage> Post<TModel>(TModel model, string? path = null, string? token = null)
        {
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.PostAsJsonAsync(path, model);
            if(response == null)
            {
                return null;
            }
            ResponseMessage responseMessage = null;
            try
            {
                responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            }
            catch (Exception ex)
            {

            }
            return responseMessage;
        }

        public virtual async Task<ResponseMessage> Post(string? path = null, string? token = null)
        {
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.PostAsync(path, null);
            if (response == null)
            {
                return null;
            }
            ResponseMessage responseMessage = null;
            try
            {
                responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            }
            catch (Exception ex)
            {

            }
            return responseMessage;
        }

        public virtual async Task<(TModelRes?, ResponseMessage)> PostWithResponse<TModelRes, TModelReq>(TModelReq model, string? path = null, string? token = null)
        {
            ResponseMessage responseMessage = null;
            var result = default(TModelRes);
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.PostAsJsonAsync(path, model);
            if (response == null)
            {
                return (result, null);
            }
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<TModelRes>();
            } else
            {
                try
                {
                    responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                }
                catch (Exception ex)
                {

                }
            }
            return (result, responseMessage);
        }

        public virtual async Task<(TModelRes?, ResponseMessage)> PostWithResponse<TModelRes>(string? path = null, string? token = null)
        {
            ResponseMessage responseMessage = null;
            var result = default(TModelRes);
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.PostAsync(path, null);
            if (response == null)
            {
                return (result, null);
            }
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<TModelRes>();
            }
            else
            {
                responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            }
            return (result, responseMessage);
        }

        public virtual async Task<ResponseResult<TModelRes>> Delete<TModelRes>(string? path = null, Dictionary<string, string>? param = null, string? token = null)
        {
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.DeleteAsync(path + RequestUtils.GetQueryPath(param));
            if (response == null)
            {
                return null;
            }
            ResponseResult<TModelRes> responseMessage = null;
            try
            {
                responseMessage = await response.Content.ReadFromJsonAsync<ResponseResult<TModelRes>>();
            }
            catch (Exception ex)
            {

            }
            return responseMessage;
        }

        public virtual async Task<ResponseResult<TModelRes>> Put<TModel, TModelRes>(TModel model, string? path = null, Dictionary<string, string>? param = null, string? token = null)
        {
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.PutAsJsonAsync(path + RequestUtils.GetQueryPath(param), model);
            if (response == null)
            {
                return null;
            }
            ResponseResult<TModelRes> responseMessage = null;
            try
            {
                responseMessage = await response.Content.ReadFromJsonAsync<ResponseResult<TModelRes>>();
            }
            catch (Exception ex)
            {

            }
            return responseMessage;
        }
        public virtual async Task<ResponseMessage> Put(string? path = null, string? token = null)
        {
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.PutAsync(path, null);
            if (response == null)
            {
                return null;
            }
            ResponseMessage responseMessage = null;
            try
            {
                responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            }
            catch (Exception ex)
            {

            }
            return responseMessage;
        }
        public virtual async Task<(List<TModel>?, ResponseMessage)> GetListAsync<TModel>(Func<TModel, bool>? expression = null, string? path = null, Dictionary<string, string>? param = null, string? token = null)
        {
            var result = new List<TModel>();
            ResponseMessage responseMessage = null;
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.GetAsync(path);
            if (response == null)
            {
                return (result, null);
            }
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<TModel>>();
                if (expression != null && result != null)
                {
                    result = result.Where(expression).ToList();
                }
            } else
            {
                try
                {
                    responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                } catch(Exception ex)
                {

                }
            }
            return (result, responseMessage);
        }

        public virtual async Task<(TModel?, ResponseMessage)> GetModelAsync<TModel>(string? path = null, Dictionary<string, string>? param = null, string? token = null)
        {
            TModel? result = default(TModel);
            ResponseMessage responseMessage = null;
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.GetAsync(path + RequestUtils.GetQueryPath(param));
            if (response == null)
            {
                return (result, null);
            }
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<TModel>();
            } else
            {
                try
                {
                    responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                }
                catch (Exception ex)
                {

                }
            }
            return (result, responseMessage);
        }

        public virtual async Task<(List<TModel>?, ResponseMessage)> GetOdataListAsync<TModel>(Func<TModel, bool>? expression = null, string? path = null, Dictionary<string, string>? param = null, string? token = null)
        {
            var result = new OdataList<TModel>();
            ResponseMessage responseMessage = null;
            RequestUtils.AddTokenHeader(Client, token);
            if (Path != StoredURI.None)
            {
                path = Path.ToString() + path;
            }
            var response = await Client.GetAsync(path);
            if (response == null)
            {
                return (result.Value, null);
            }
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<OdataList<TModel>>();
                if (expression != null && result != null && result.Value != null && result.Value.Count > 0)
                {
                    result.Value = result.Value.Where(expression).ToList();
                }
            }
            else
            {
                try
                {
                    responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
                }
                catch (Exception ex)
                {

                }
            }
            return (result.Value, responseMessage);
        }
    }
}
