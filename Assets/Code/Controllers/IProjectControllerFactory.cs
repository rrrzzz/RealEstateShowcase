namespace Code.Controllers
{
    public interface IProjectControllerFactory
    {
        ProjectsManager ProjectsManager { get; }
    }
}