using System;
using UnityEngine;

namespace MarketTestCase
{
    public interface IInput
    {
        public event Action<Vector2> MoveInput;
        public event Action<Vector2> RotationInput;
        public event Action<Vector2> TouchInput;
        public event Action ThrowInput;
    }
}
