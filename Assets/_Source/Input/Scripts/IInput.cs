using System;
using UnityEngine;

namespace MarketTestCase
{
    public interface IInput
    {
        public event Action<Vector2> MoveInput;
        public event Action<Vector2> TouchInput;
    }
}
