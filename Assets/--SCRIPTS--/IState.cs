using UnityEngine;

namespace Characters
{
    public interface IState
    {
        void InsertValue(int value)
        {
            //Override at will.
        }

        void OnEnter();
        void Tick();
        void OnExit();
    }
}
