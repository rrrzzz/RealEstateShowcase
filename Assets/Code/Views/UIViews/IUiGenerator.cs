namespace Code.Views.UIViews
{
    public interface IUiGenerator
    {
        IGradeToggleView CreateCostToggle();
        IGradeToggleView CreateFlatCountToggle();
    }
}