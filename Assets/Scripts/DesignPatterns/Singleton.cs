using UnityEngine;

namespace DesignPatterns
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this as T;
            else if (Instance != this as T)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}