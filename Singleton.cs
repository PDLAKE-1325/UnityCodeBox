using UnityEngine;

namespace shamiu.Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        virtual protected bool UseDontDestroyOnLoad => true;
        public static bool ExistNow => instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindFirstObjectByType(typeof(T));
                }

                if (instance == null)
                {
                    Debug.LogWarning($"[싱글톤] {typeof(T).Name} 인스턴스가 없어서 자동 생성됨");
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (ExistNow)
            {
                Destroy(gameObject);

                return;
            }

            if (UseDontDestroyOnLoad)
            {
                if (transform.parent != null && transform.root != null)
                {
                    DontDestroyOnLoad(transform.root.gameObject);
                }
                else
                {
                    DontDestroyOnLoad(gameObject);
                }
            }

            instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (instance == this as T)
                instance = null;
        }
    }
}