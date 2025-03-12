using System;
using UnityEngine;
using Zenject;

namespace MarketTestCase
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IPicker
    {
        [SerializeField] private PlayerConfig _config;

        [SerializeField] private Transform _head;
        [SerializeField] private Transform _hand;

        private IPickable _currentPickableItem;
        private float _xRotation;

        private IInput _input;
        private CharacterController _controller;

        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        public event Action ItemPicked;
        public event Action ItemThrowed;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _input.MoveInput += OnMoveInput;
            _input.RotationInput += OnRotationInput;
            _input.TouchInput += OnTouchInput;
            _input.ThrowInput += OnThrowInput;
        }

        private void OnDisable()
        {
            _input.MoveInput -= OnMoveInput;
            _input.RotationInput -= OnRotationInput;
            _input.TouchInput -= OnTouchInput;
            _input.ThrowInput -= OnThrowInput;
        }

        private void FixedUpdate()
        {
            HandleGravity();
        }

        private void HandleGravity()
        {
            _controller.Move(Vector3.down);
        }

        private void OnMoveInput(Vector2 moveDirection)
        {
            Vector3 move = _head.forward * moveDirection.y + _head.right * moveDirection.x;
            move.y = 0f;
            _controller.Move(_config.MoveSpeed * Time.deltaTime * move);
        }

        private void OnRotationInput(Vector2 rotationInput)
        {
            float mouseX = rotationInput.x * _config.HorizontalLookSensitivity * Time.deltaTime;
            float mouseY = rotationInput.y * _config.VerticalLookSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            _head.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }

        private void OnTouchInput(Vector2 touchPosition)
        {
            if (_currentPickableItem != null)
                return;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _config.ItemPickDistance, _config.ItemLayer))
            {
                if (hit.transform.TryGetComponent(out IPickable pickable))
                {
                    _currentPickableItem = pickable;
                    _currentPickableItem.Pick(_hand);
                    ItemPicked?.Invoke();
                }
            }
        }

        private void OnThrowInput()
        {
            if (_currentPickableItem == null)
                return;
            _currentPickableItem.Throw(_hand.forward, _config.ThrowForce);
            _currentPickableItem = null;
            ItemThrowed?.Invoke();
        }
    }
}
