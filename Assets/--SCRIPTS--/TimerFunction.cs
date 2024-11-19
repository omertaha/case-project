using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I didn't write this class. I picked parts of it from several coders on the web and put it together.
/// I use this tool in my every project.
/// Maybe it can be better performance-wise in the future.
/// *Taha*
/// </summary>
namespace Misc
{
    public class TimerFunction
    {

        private static List<TimerFunction> _activeTimerList;
        private static GameObject _initGameObject;

        private static void InitIfNeeded()
        {
            if (_initGameObject == null)
            {
                _initGameObject = new GameObject("FunctionTimer_InitGameObject");
                _activeTimerList = new List<TimerFunction>();
            }
        }

        public static TimerFunction Create(Action action, float timer, string timerName = null)
        {
            InitIfNeeded();
            GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));

            TimerFunction functionTimer = new TimerFunction(action, timer, timerName, gameObject);

            gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionTimer.Update;

            _activeTimerList.Add(functionTimer);

            return functionTimer;
        }

        private static void RemoveTimer(TimerFunction functionTimer)
        {
            InitIfNeeded();
            if (functionTimer == null)
                return;

            _activeTimerList.Remove(functionTimer);
        }

        public static void StopTimer(string timerName)
        {
            InitIfNeeded();
            for (int i = 0; i < _activeTimerList.Count; i++)
            {
                if (_activeTimerList[i]._timerName == timerName)
                {
                    // Stop this timer
                    _activeTimerList[i].DestroySelf();
                    i--;
                }
            }
        }



        // Dummy class to have access to MonoBehaviour functions
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action OnUpdate;
            private void FixedUpdate()
            {
                if (OnUpdate != null) OnUpdate?.Invoke();
            }
        }

        private Action _action;
        private float _timer;
        private string _timerName;
        private GameObject _gameObject;
        private bool _isDestroyed;

        private TimerFunction(Action action, float timer, string timerName, GameObject gameObject)
        {
            this._action = action;
            this._timer = timer;
            this._timerName = timerName;
            this._gameObject = gameObject;
            _isDestroyed = false;
        }

        public void Update()
        {
            if (!_isDestroyed)
            {
                _timer -= Time.fixedDeltaTime;
                if (_timer < 0)
                {
                    // Trigger the action
                    _action?.Invoke();
                    DestroySelf();
                }
            }
        }

        private void DestroySelf()
        {
            _isDestroyed = true;
            UnityEngine.Object.Destroy(_gameObject);
            RemoveTimer(this);
        }
    }
}

