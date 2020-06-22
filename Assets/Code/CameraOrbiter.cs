using UnityEngine;

namespace Code
{
    public class CameraOrbiter : MonoBehaviour
    {
        [SerializeField] private float orbitingSpeed = 2f;
        [SerializeField] private Vector3 center = Vector3.zero;

        private void Update()
        {
            if (!Input.GetMouseButton(1)) return;
            var orbitAmount = orbitingSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.RotateAround(center, Vector3.up, orbitAmount);
        }
    }
}
