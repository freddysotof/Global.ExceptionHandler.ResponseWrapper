namespace Global.ExceptionHandler.ResponseWrapper.Config
{
    public class MiddlewareSettings
    {
        public bool UseTimeLoggingMiddleware { get; set; }
        public bool UseCultureMiddleware { get; set; }
        public bool UsePaginationResponseWrapperMiddleware { get; set; }
    }
}
