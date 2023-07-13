﻿namespace SupplyChain.App.ViewModels
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }

        public ApiResponse(bool success, T data, string message = null, string errorCode = null)
        {
            Success = success;
            Data = data;
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
