using UnityEngine;

namespace Assets.Scripts.Singleton
{
    public class Singleton<T> : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = GetComponent<T>();
        }
    }
}
