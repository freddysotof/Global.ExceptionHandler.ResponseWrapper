using Global.ExceptionHandler.ResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Global.ExceptionHandler.ResponseWrapper.Services
{
    public interface IResponseWrapperService
    {
        SuccessResponseWrapper WrapPagedResponse(string response, HttpContext context);
        SuccessResponseWrapper WrapResponse(string response, HttpContext context);
        //PagedResponse<DestinationModel> PagedResponse(List<SourceData> rawData, int? limit, int offset);
    }
}
