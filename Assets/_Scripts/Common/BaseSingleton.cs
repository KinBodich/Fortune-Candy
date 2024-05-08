using UnityEngine;

namespace Common
{
    public class BaseSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError($"More than 1 instance of {name}");
                Destroy(this);
            }
            else
            {
                Instance = this as T;
            }
        }
    }
}