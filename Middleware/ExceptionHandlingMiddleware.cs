using CinemaBookingAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CinemaBookingAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = ex switch
            {
                MovieNotFoundException => HttpStatusCode.NotFound,
                BookingNotFoundException => HttpStatusCode.NotFound,
                ShowTimeNotFoundException => HttpStatusCode.NotFound,
                CustomerNotFoundException => HttpStatusCode.NotFound,
                MovieAlreadyExistsException => HttpStatusCode.Conflict,
                InvalidBookingException => HttpStatusCode.UnprocessableEntity,
                _ => HttpStatusCode.InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = ex.GetType().Name,
                Detail = ex.Message
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}