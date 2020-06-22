using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views.UIViews
{
    public class GradeToggleView : MonoBehaviour, IGradeToggleView
    {
        public Button Toggle { get; private set; }
        
        private readonly Color _onColor = new Color(1f, 1f, 1f, 0.5f);
        private readonly Color _offColor = Color.black;
        private Image _btnImage;
        
        public void ToggleOn()
        {
            _btnImage.color = _onColor;
            Toggle.interactable = false;
        }
        
        public void ToggleOff()
        {
            _btnImage.color = _offColor;
            Toggle.interactable = true;
        }
        
        public void DisableBtnForTime(float delay)
        {
            StartCoroutine(DisableBtnDelay(delay));
        }

        private IEnumerator DisableBtnDelay(float delay)
        {
            Toggle.interactable = false;
            yield return new WaitForSeconds(delay);
            Toggle.interactable = true;
        }
        
        private void OnEnable()
        {
            Toggle = GetComponent<Button>();
            _btnImage = GetComponent<Image>();
        }
    }
}