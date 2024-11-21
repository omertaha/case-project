using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I didn't write this class.
/// I just use it all the time!
/// There are small changes for this game.
/// Taha
/// </summary>
namespace Misc
{
    public static class PoolingSystem
    {
        private static Dictionary<string, Queue<GameObject>> _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        private static Dictionary<string, Transform> _containerDictionary = new Dictionary<string, Transform>();
        private static Transform _objectPoolRoot; // Root transform for pooling

        /// <summary>
        /// Adds a GameObject to the pool.
        /// </summary>
        /// <param name="item">The GameObject to pool.</param>
        /// <param name="poolSize">The number of objects to instantiate initially.</param>
        public static void AddToPool(GameObject item, int poolSize)
        {
            EnsureRootExists();

            string key = item.name;

            if (!_poolDictionary.ContainsKey(key))
            {
                if (!_containerDictionary.ContainsKey(key))
                {
                    Transform newTransform = new GameObject(key + "Container").transform;
                    newTransform.parent = _objectPoolRoot;
                    _containerDictionary.Add(key, newTransform);
                }

                Queue<GameObject> objectPool = new Queue<GameObject>();
                _poolDictionary.Add(key, objectPool);
            }

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Object.Instantiate(item, _containerDictionary[key]);
                obj.SetActive(false);
                _poolDictionary[key].Enqueue(obj);
            }
        }

        /// <summary>
        /// Returns a GameObject to the pool.
        /// </summary>
        public static void ReturnObject(GameObject item)
        {
            string key = item.name.Substring(0, item.name.Length - 7); // Remove "(Clone)" from name

            if (!item.activeSelf)
                return;

            if (!_poolDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Object {key} not in pool");
                return;
            }

            item.transform.position = Vector3.zero;
            item.SetActive(false);
            _poolDictionary[key].Enqueue(item);
        }

        /// <summary>
        /// Gets a GameObject from the pool.
        /// </summary>
        public static GameObject GetObject(GameObject item)
        {
            string key = item.name;

            if (!_poolDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Object {key} not in pool");
                return null;
            }

            GameObject obj = _poolDictionary[key].Count > 0
                ? _poolDictionary[key].Dequeue()
                : AddToPoolAndReturnOne(item);

            obj.SetActive(true);
            return obj;
        }

        private static GameObject AddToPoolAndReturnOne(GameObject item)
        {
            AddToPool(item, 10);
            return GetObject(item);
        }

        /// <summary>
        /// Ensures the root container for pooled objects exists and is persistent.
        /// </summary>
        private static void EnsureRootExists()
        {
            if (_objectPoolRoot == null)
            {
                GameObject root = new GameObject("ObjectPoolContainerRoot");
                _objectPoolRoot = root.transform;
                Object.DontDestroyOnLoad(root); // Make it persist across scenes
            }
        }
    }
}
