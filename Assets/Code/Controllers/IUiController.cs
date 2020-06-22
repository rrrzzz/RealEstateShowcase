using UnityEngine.Events;

namespace Code.Controllers
{
    public interface IUiController
    {
        void AddCostToggleClickListener(UnityAction listener);
        void AddFlatsToggleClickListener(UnityAction listener);
    }
}