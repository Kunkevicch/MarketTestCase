using System;

namespace MarketTestCase
{
    public interface IPickEventMediator
    {
        void AddListener(PickEventType eventType, Action callback);
        void RemoveListener(PickEventType eventType, Action callback);
        void SendMessage(PickEventType eventType);
    }
}