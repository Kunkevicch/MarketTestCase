using System;
using System.Collections;
using UnityEngine;

namespace MarketTestCase
{
    public class MobileInput : MonoBehaviour, IInput
    {
        [SerializeField] private Joystick _joystickMove;

        public event Action<Vector2> MoveInput;
        public event Action<Vector2> TouchInput;

        private void OnEnable()
        {
            StartCoroutine(MoveToDirectionRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(MoveToDirectionRoutine());
        }

        private IEnumerator MoveToDirectionRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => _joystickMove.Direction != Vector2.zero);

                while (_joystickMove.Direction != Vector2.zero)
                {
                    MoveInput?.Invoke(_joystickMove.Direction.normalized);
                    yield return null;
                }

                MoveInput?.Invoke(Vector2.zero);
            }
        }
    }
}
