public static class HelloWorldApi
{
    public static void MapHelloWorldRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello World!").ExcludeFromDescription();
    }
}