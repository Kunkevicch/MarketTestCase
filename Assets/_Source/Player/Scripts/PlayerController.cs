using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MarketTestCase
{
    public class PlayerController : MonoBehaviour
    {
        private IInput _input;

        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            _input.MoveInput += OnMoveInput;
        }

        private void OnDisable()
        {
            _input.MoveInput -= OnMoveInput;
        }

        private void OnMoveInput(Vector2 moveDirection)
        {
            Debug.Log(moveDirection);
        }
    }
}
