﻿namespace RecipeHelper.Application.Common.Responses
{
    public class Response<T>
    {
        public bool Succeeded { get; protected set; }
        public T Data { get; protected set; }
        public string Message { get; protected set; }
        public List<string> Errors { get; set; }

        public Response() { }

        public Response(T data, string message)
        {
            Succeeded = true;
            Data = data;
            Message = message;
        }

        public Response(string message)
        {
            Succeeded = true;
            Message = message;
        }

        #region Success

        public static Response<T> Success()
        {
            var result = new Response<T>() { Succeeded = true };
            return result;
        }

        public static Response<T> Success(string message) 
        {
            var result = new Response<T>() { Succeeded = true, Message = message };
            return result;
        }

        public static Response<T> Success(T data, string message)
        {
            var result = new Response<T> { Succeeded = true, Data = data, Message = message };
            return result;
        }

        #endregion

        #region Fail
        public static Response<T> Fail()
        {
            var result = new Response<T> { Succeeded = false };
            return result;
        }
        public static Response<T> Fail(string message)
        {
            var result = new Response<T> { Succeeded = false, Message = message };
            return result;
        }
        public static Response<T> Fail(string message, List<string> errors)
        {
            var result = new Response<T> { Succeeded = false, Message = message, Errors = errors };
            return result;
        }
        public static Response<T> Fail(List<string> errors)
        {
            var result = new Response<T> { Succeeded = false, Errors = errors };
            return result;
        }

        #endregion

    }
}