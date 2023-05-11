﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSyaMCServices.DL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eSyaMCServices.WebAPI.Utility
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApikeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue("dbContextType", out var dbContextType);
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            eSyaEnterprise._connString = configuration.GetConnectionString(dbContextType + ":dbConn_eSyaEnterprise");

            await next();
        }
    }
}
