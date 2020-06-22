using UnityEngine;

namespace Code.Utility
{
    public class MonoBehaviourProvider : MonoBehaviour
    {
        public GameObject CreateInstance(GameObject prefab)
        {
            return Instantiate(prefab);
        }

        public GameObject CreateInstance(GameObject prefab, Transform parent)
        {
            return Instantiate(prefab, parent);
        }
        
        public GameObject CreateInstance(GameObject prefab, Vector3 position)
        {
            return Instantiate(prefab, position, Quaternion.identity);
        }
    }
}