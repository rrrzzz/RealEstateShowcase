using UnityEngine.UI;

namespace Code.Views.UIViews
{
    public interface IGradeToggleView
    {
        Button Toggle { get; }
        void DisableBtnForTime(float delay);
        void ToggleOn();
        void ToggleOff();
    }
}