﻿namespace YediDoga_Server.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterEmployeeRoutes();
        app.RegisterHoneyRoutes();
    }
}
