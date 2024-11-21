using UnityEngine;
using Characters;

namespace Misc
{
    public class FinishLineTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AI ai))
            {
                TimerFunction.Create(() => ai.TriggerState("Cheer"), 0.5f, "Cheer" + other.GetInstanceID().ToString());
            }
        }
    }
}

