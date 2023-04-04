
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Web
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizationFilter /* : ActionFilterAttribute*/
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base.OnActionExecuting(context);
        //    var services = context.HttpContext.RequestServices;
        //    var cache = (IMemoryCache)services.GetService(typeof(IMemoryCache));
        //    var currentLang = "sq";
        //    var result = GetCachedData(cache);
           
        //}


        //public string GetCachedData(IMemoryCache cache)
        //{
        //    string cacheEntry;

        //    if (!cache.TryGetValue("Translator", out cacheEntry))
        //    {
        //        Random r = new Random();
        //        cacheEntry = "Bedri Mustafa " + r.Next(10, 50);
        //        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
        //        cache.Set("Translator", cacheEntry, cacheEntryOptions);
        //    }

        //    return cacheEntry;
        //}

        //private readonly RequestDelegate _next;
        //public LocalizationFilter(RequestDelegate next)
        //{
        //    _next = next;
        //}
        //public async Task InvokeAsync(HttpContext context)
        //{

        //    //await context.Response.("- Before Message -  \n\r");
        //    ////await _next(context);
        //    //await context.Response.WriteAsync("\n\r - After Message - ");
        //}


    }

    //public static class LocalizationCustomExtension
    //{
    //    public static IApplicationBuilder UseLocalizationCustomMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<LocalizationFilter>();
    //    }
    //}


    //public class LogResourceFilter : Attribute, IResourceFilter
    //{
    //    public void OnResourceExecuting(
    //        ResourceExecutingContext context)
    //    {
    //        Console.WriteLine("Executing!");
    //    }

    //    public void OnResourceExecuted(
    //        ResourceExecutedContext context)
    //    {
    //        Console.WriteLine("Executed”");
    //    }
    //}
}
