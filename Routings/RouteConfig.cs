namespace WorkPlanner.Routings;

public class RouteConfig
{
    public static void RegisterRoutes(WebApplication app)
    {
        // Mapowanie kontrolerów, w tym TasksController
        app.MapControllers();
    }
}