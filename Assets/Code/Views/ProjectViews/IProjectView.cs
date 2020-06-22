namespace Code.Views.ProjectViews
{
    public interface IProjectView
    {
        int Id { get; set; }
        float TotalCost { get; set; }
        int FlatCount { get; set; } 
        void ResizeProjectHeight(float value);
    }
}