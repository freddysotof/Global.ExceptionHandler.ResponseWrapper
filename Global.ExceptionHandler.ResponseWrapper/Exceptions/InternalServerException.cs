﻿using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class InternalServerException : CustomException
    {
        public InternalServerException(string message, List<ResponseError>? errors = default)
            : base(message, errors, HttpStatusCode.InternalServerError) { }
    }
}
