using System;
using System.Threading.Tasks;
using Carter.ModelBinding;
using Carter.Response;
using Microsoft.AspNetCore.Http;

namespace ActiveDirectory.Extensions
{
    public static class ModuleExtensions
    {
        /// <summary>
        /// Encapsulate execution of handler with the corresponding validation logic
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="res">An http response that will be populated</param>
        /// <param name="handler">A func handler that will be validated and executed</param>
        /// <returns></returns>
        public static Task ExecHandler<TOut>(this HttpResponse res, Func<TOut> handler)
        {
            try
            {
                var response = handler();

                if (response == null)
                {
                    res.StatusCode = 204;
                    return Task.CompletedTask;
                }

                res.StatusCode = 200;
                return res.Negotiate(response);
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                return res.Negotiate(ex.Message);
            }
        }

        /// <summary>
        /// Encapsulate execution of handler with the validation logic and storage on cache using the key provided
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="res">An http response that will be populated</param>
        /// <param name="key">A string key that will be used to identify the request</param>
        /// <param name="store">A cache store provided by the client</param>
        /// <param name="handler">A func handler that will be validated and executed</param>
        /// <returns></returns>
        public static Task ExecHandler<TOut>(this HttpResponse res, string key, Store store, Func<TOut> handler)
        {
            try
            {
                var response = store.GetOrSetCache(key, () => handler());

                if (response == null)
                {
                    res.StatusCode = 204;
                    return Task.CompletedTask;
                }

                res.StatusCode = 200;
                return res.Negotiate(response);
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                return res.Negotiate(ex.Message);
            }
        }

        /// <summary>
        /// Encapsulate execution of handler with the validation logic while binding and validating the http request
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="res">An http response that will be populated</param>
        /// <param name="req">An http request that will be binded and validated</param>
        /// <param name="handler">A func handler that will be validated and executed</param>
        /// <returns></returns>
        public static Task ExecHandler<TIn, TOut>(this HttpResponse res, HttpRequest req, Func<TIn, TOut> handler)
        {
            try
            {
                var (validationResult, data) = req.BindAndValidate<TIn>();

                if (!validationResult.IsValid)
                {
                    res.StatusCode = 422;
                    return res.Negotiate(validationResult.GetFormattedErrors());
                }

                var response = handler(data);

                if (response == null)
                {
                    res.StatusCode = 204;
                    return Task.CompletedTask;
                }

                res.StatusCode = 200;
                return res.Negotiate(response);
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                return res.Negotiate(ex.Message);
            }
        }

        /// <summary>
        /// Encapsulate execution of handler with the validation logic while binding,
        /// validating the http request and storing on cache using the key provided
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="res">An http response that will be populated</param>
        /// <param name="req">An http request that will be binded and validated</param>
        /// <param name="key">A string key that will be used to identify the request</param>
        /// <param name="store">A cache store provided by the client</param>
        /// <param name="handler">A func handler that will be validated and executed</param>
        /// <returns></returns>
        public static Task ExecHandler<TIn, TOut>(this HttpResponse res, HttpRequest req, string key, Store store, Func<TIn, TOut> handler)
        {
            try
            {
                var (validationResult, data) = req.BindAndValidate<TIn>();

                if (!validationResult.IsValid)
                {
                    res.StatusCode = 422;
                    return res.Negotiate(validationResult.GetFormattedErrors());
                }

                var response = store.GetOrSetCache(key, () => handler(data));

                if (response == null)
                {
                    res.StatusCode = 204;
                    return Task.CompletedTask;
                }

                res.StatusCode = 200;
                return res.Negotiate(response);
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                return res.Negotiate(ex.Message);
            }
        }
    }
}
