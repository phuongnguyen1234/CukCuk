using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CukCuk.Backend.Core.Middlewares
{
    /// <summary>
    /// Middleware xử lý exception
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException vex)
            {
                // nếu lỗi validate thì ném lỗi 400
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var payload = new
                {
                    Success = false,
                    vex.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    errors = vex.Data // Thay đổi 'Errors' thành 'errors' để khớp với frontend
                };

                var json = JsonConvert.SerializeObject(payload);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                // nếu lỗi chung chung của server thì ném lỗi 500
                var result = new Result(false, ex.Message, 500);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var json = JsonConvert.SerializeObject(result);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
