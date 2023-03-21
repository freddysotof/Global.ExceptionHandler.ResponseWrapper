using Global.ExceptionHandler.ResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Global.ExceptionHandler.ResponseWrapper.Services
{
    public interface IResponseWrapperService<T> where T : class
    {
        SuccessResponseWrapper<T> WrapPagedResponse(string response);
        SuccessResponseWrapper<T> WrapResponse(string response);
        //PagedResponse<DestinationModel> PagedResponse(List<SourceData> rawData, int? limit, int offset);
    }
}
