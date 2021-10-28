﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace MerchandiseService.Infrastructure.StartupFilters
{
    internal class SwaggerStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                next(app);
            };
        }
    }
}