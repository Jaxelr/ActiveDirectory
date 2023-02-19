using System;
using System.Threading.Tasks;
using ActiveDirectory.Models.Entities;
using Carter.Cache;
using Carter.ModelBinding;
using Carter.Response;
using Microsoft.AspNetCore.Http;

namespace ActiveDirectory.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Encapsulate execution of handler with the corresponding validation logic
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="ctx">The http context to process</param>
    /// <param name="handler">A func handler that will be validated and executed</param>
    /// <returns name="Task">A Task object with the results</returns>
    public static async Task ExecHandler<TOut>(this HttpContext ctx, Func<TOut> handler)
    {
        try
        {
            var response = handler();

            if (response is null)
            {
                ctx.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            ctx.Response.StatusCode = StatusCodes.Status200OK;
            await ctx.Response.Negotiate(response);
        }
        catch (Exception ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.Negotiate(new FailedResponse(ex));
        }
    }

    /// <summary>
    /// Encapsulate execution of handler with the validation logic and storage on cache using the key provided
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="ctx">The http context to process</param>
    /// <param name="handler">A func handler that will be validated and executed</param>
    /// <returns name="Task">A Task object with the results</returns>
    public static async Task ExecHandler<TOut>(this HttpContext ctx, int cacheTimespan, Func<TOut> handler)
    {
        try
        {
            ctx.AsCacheable(cacheTimespan);

            var response = handler();

            if (response is null)
            {
                ctx.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            ctx.Response.StatusCode = StatusCodes.Status200OK;
            await ctx.Response.Negotiate(response);
        }
        catch (Exception ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.Negotiate(new FailedResponse(ex));
        }
    }

    /// <summary>
    /// Encapsulate execution of handler with the validation logic while binding and validating the http request
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="ctx">The http context to process</param>
    /// <param name="handler">A func handler that will be validated and executed</param>
    public static async Task ExecHandler<TIn, TOut>(this HttpContext ctx, TIn @in, Func<TIn, TOut> handler)
    {
        try
        {
            var result = ctx.Request.Validate(@in);

            if (!result.IsValid)
            {
                ctx.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await ctx.Response.Negotiate(result.GetFormattedErrors());
                return;
            }

            var response = handler(@in);

            if (response == null)
            {
                ctx.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            ctx.Response.StatusCode = StatusCodes.Status200OK;
            await ctx.Response.Negotiate(response);
        }
        catch (ArgumentNullException ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            await ctx.Response.Negotiate(new FailedResponse(ex));
        }
        catch (Exception ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.Negotiate(new FailedResponse(ex));
        }
    }

    /// <summary>
    /// Encapsulate execution of handler with the validation logic while binding,
    /// validating the http request and storing on cache using the key provided
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="ctx">The http context to process</param>
    /// <param name="cacheTimespan">Time alive for the store to keep</param>
    /// <param name="handler">A func handler that will be validated and executed</param>
    /// <returns name="Task">A Task object with the results</returns>
    public static async Task ExecHandler<TIn, TOut>(this HttpContext ctx, TIn @in, int cacheTimespan, Func<TIn, TOut> handler)
    {
        try
        {
            var result = ctx.Request.Validate(@in);

            if (!result.IsValid)
            {
                ctx.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await ctx.Response.Negotiate(result.GetFormattedErrors());
                return;
            }

            ctx.AsCacheable(cacheTimespan);

            var response = handler(@in);

            if (response == null)
            {
                ctx.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            ctx.Response.StatusCode = StatusCodes.Status200OK;
            await ctx.Response.Negotiate(response);
        }
        catch (ArgumentNullException ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            await ctx.Response.Negotiate(new FailedResponse(ex));
        }
        catch (Exception ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.Negotiate(new FailedResponse(ex));
        }
    }
}
