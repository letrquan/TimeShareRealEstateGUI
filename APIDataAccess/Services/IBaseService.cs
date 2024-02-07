using APIDataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Services
{
    public interface IBaseService
    {
        Task<ResponseMessage> Put<TModel>(TModel model, string? path = null, Dictionary<string, string>? param = null, string? token = null);
        Task<ResponseMessage> Delete(string? path = null, Dictionary<string, string>? param = null, string? token = null);
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
