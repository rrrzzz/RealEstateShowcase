using Code.Views.UIViews;
using UnityEngine.Events;

namespace Code.Controllers
{
    public class UiController : IUiController
    {
        private const float ButtonClickCooldown = 0.5f;
        private IGradeToggleView _costToggle;
        private IGradeToggleView _flatsToggle;

        private readonly IUiGenerator _uiGenerator;
        
        public UiController(IUiGenerator generator)
        {
            _uiGenerator = generator;
            InitializeToggles();
        }
        
        public void AddCostToggleClickListener(UnityAction listener)
        {
            _costToggle.Toggle.onClick.AddListener(listener);
        }
        
        public void AddFlatsToggleClickListener(UnityAction listener)
        {
            _flatsToggle.Toggle.onClick.AddListener(listener);
        }

        private void InitializeToggles()
        {
            _costToggle = _uiGenerator.CreateCostToggle();
            _flatsToggle = _uiGenerator.CreateFlatCountToggle();
            
            _costToggle.Toggle.onClick.AddListener(ToggleCost);
            _flatsToggle.Toggle.onClick.AddListener(ToggleFlats);
        }

        private void ToggleCost()
        {
            _flatsToggle.ToggleOff();
            _costToggle.ToggleOn();
            _flatsToggle.DisableBtnForTime(ButtonClickCooldown);
        }

        private void ToggleFlats()
        {
            _flatsToggle.ToggleOn();
            _costToggle.ToggleOff();
            _costToggle.DisableBtnForTime(ButtonClickCooldown);
        }
    }
}