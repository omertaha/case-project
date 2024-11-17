using UnityEngine;
using System;

namespace Managers
{
    public abstract class ManagerBase : MonoBehaviour
    {
        public Action Ready;
    }


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

                    if (_instance == null)
                    {
                        Debug.LogError($"Instance of {typeof(T)} is required in the scene but is missing.");
                    }
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

