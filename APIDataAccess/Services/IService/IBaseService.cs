using APIDataAccess.DTO;
using APIDataAccess.DTO.ResponseModels.Helpers;

namespace APIDataAccess.Services
{
    public interface IBaseService
    {
        Task<ResponseResult<TModelRes>> Put<TModel, TModelRes>(TModel model, string? path = null, Dictionary<string, string>? param = null, string? token = null);
        Task<ResponseResult<TModelRes>> Delete<TModelRes>(string? path = null, Dictionary<string, string>? param = null, string? token = null);
        Task<ResponseMessage> Post<TModel>(TModel model, string? path = null, string? token = null);
        Task<ResponseMessage> Post(string? path = null, string? token = null);
        Task<ResponseMessage> Put(string? path = null, string? token = null);
        Task<(TModelRes?, ResponseMessage)> PostWithResponse<TModelRes, TModelReq>(TModelReq model, string? path = null, string? token = null);
        Task<(TModelRes?, ResponseMessage)> PostWithResponse<TModelRes>(string? path = null, string? token = null);
        Task<(List<TModel>?, ResponseMessage)> GetListAsync<TModel>(Func<TModel, bool>? expression = null, string? path = null, Dictionary<string, string>? param = null, string? token = null);
        Task<(List<TModel>?, ResponseMessage)> GetOdataListAsync<TModel>(Func<TModel, bool>? expression = null, string? path = null, Dictionary<string, string>? param = null, string? token = null);
        Task<(TModel?, ResponseMessage)> GetModelAsync<TModel>(string? path = null, Dictionary<string, string>? param = null, string? token = null);
    }
}
