using UnityEngine;
using Characters;
using Managers;

namespace Misc
{
    public class PaintingTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
            {
                player.TriggerState?.Invoke("Painting");
                GameManager.TriggerPaintingMode?.Invoke();
            }
        }
    }
}

