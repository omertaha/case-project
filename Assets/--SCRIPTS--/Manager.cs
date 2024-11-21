using UnityEngine;
using System;

namespace Managers
{
    public abstract class ManagerBase : MonoBehaviour
    {
        public Action Ready;
    }

    /// <summary>
    /// All Managers are singleton classes. Some of them DontDestroyOnLoad.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Manager<T> : ManagerBase where T : Manager<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}

