namespace Code.Models.ProjectModels
{
    public readonly struct ProjectsInfo
    {
        public float MaxCost { get; }
        public int MaxFlatCount { get; }

        public ProjectsInfo(float maxCost, int maxFlatCount)
        {
            MaxCost = maxCost;
            MaxFlatCount = maxFlatCount;
        }
    }
}