using System;
using System.Collections.Generic;
using UnityEngine;
using Misc;
using System.Collections;

namespace Managers
{
    public class VFXManager : Manager<VFXManager>
    {
        [Serializable]
        public class VFXEntry
        {
            public string Name;
            public GameObject Prefab; // VFX prefab
            public int PoolSize; // Number of objects to pool
        }

        [SerializeField]
        private List<VFXEntry> _vfxList; // List of VFX entries

        private Dictionary<string, GameObject> _vfxDictionary = new Dictionary<string, GameObject>();

        public static Action<string, Vector3, Quaternion> PlayVFX;

        private void Start()
        {
            InitializeVFXPool();
            DontDestroyOnLoad(this);
            Ready?.Invoke();
        }

        private void OnEnable()
        {
            PlayVFX += PlayMyVFX;
        }

        private void OnDisable()
        {
            PlayVFX -= PlayMyVFX;
        }

        private void InitializeVFXPool()
        {
            foreach (var vfxEntry in _vfxList)
            {
                if (!_vfxDictionary.ContainsKey(vfxEntry.Name))
                {
                    _vfxDictionary.Add(vfxEntry.Name, vfxEntry.Prefab);
                    PoolingSystem.AddToPool(vfxEntry.Prefab, vfxEntry.PoolSize);
                }
            }

            Ready?.Invoke();
        }

        /// <summary>
        /// Spawn the VFX at a given position and rotation.
        /// </summary>
        public void PlayMyVFX(string name, Vector3 position, Quaternion rotation)
        {
            if (_vfxDictionary.TryGetValue(name, out GameObject vfxPrefab))
            {
                GameObject vfx = PoolingSystem.GetObject(vfxPrefab);
                vfx.transform.position = position;
                vfx.transform.rotation = rotation;

                // Optionally, handle automatic return of the VFX after some time
                var returner = vfx.GetComponent<VFXReturner>();
                if (returner == null)
                {
                    returner = vfx.AddComponent<VFXReturner>();
                }
                returner.Initialize();
            }
            else
            {
                Debug.Log("VFX Not Found");
            }
        }
    }

    /// <summary>
    /// Helper component to automatically return VFX to the pool after its duration.
    /// We add this on "PlayVFX"
    /// </summary>
    public class VFXReturner : MonoBehaviour
    {
        public void Initialize()
        {
            StartCoroutine(ReturnToPoolAfterDuration());
        }

        private IEnumerator ReturnToPoolAfterDuration()
        {
            var particleSystem = GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                yield return new WaitForSeconds(particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
            }
            else
            {
                yield return new WaitForSeconds(2f); // Default duration
            }

            PoolingSystem.ReturnObject(this.gameObject);
        }
    }
}
