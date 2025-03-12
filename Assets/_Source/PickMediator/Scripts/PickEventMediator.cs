using System;
using System.Collections.Generic;

namespace MarketTestCase
{
    public class PickEventMediator : IPickEventMediator
    {
        private readonly Dictionary<PickEventType, List<Action>> _listeners = new();

        public void AddListener(PickEventType eventType, Action callback)
        {
            if (!_listeners.ContainsKey(eventType))
            {
                _listeners.Add(eventType, new List<Action>());
            }
            _listeners[eventType].Add(callback);
        }

        public void RemoveListener(PickEventType eventType, Action callback)
        {
            if (_listeners.TryGetValue(eventType, out var callbacks))
            {
                callbacks.Remove(callback);
            }
        }

        public void SendMessage(PickEventType eventType)
        {
            if (!_listeners.TryGetValue(eventType, out var callbacks))
                return;

            foreach (var callback in callbacks.ToArray())
            {
                callback?.Invoke();
            }
        }

    }
}
