using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MarketTestCase
{
    public class MobileInput : MonoBehaviour, IInput
    {
        [SerializeField] private Button _throwButton;

        private Joystick _joystickMove;
        private IToucheable _touchPanel;

        private IPickEventMediator _pickEventMediator;

        [Inject]
        public void Construct(IPickEventMediator eventMediator)
        {
            _pickEventMediator = eventMediator;
        }

        public event Action<Vector2> MoveInput;
        public event Action<Vector2> RotationInput;
        public event Action<Vector2> TouchInput;
        public event Action ThrowInput;

        private void Awake()
        {
            _joystickMove = GetComponentInChildren<Joystick>();
            _touchPanel = GetComponentInChildren<IToucheable>();
        }

        private void OnEnable()
        {
            StartCoroutine(MoveToDirectionRoutine());

            _touchPanel.PointerMove += OnPointerMoved;
            _touchPanel.PointerClick += OnPointerClicked;
            _throwButton.onClick.AddListener(() => ThrowInput?.Invoke());
            _pickEventMediator.AddListener(PickEventType.Throwed, OnItemThrowed);
            _pickEventMediator.AddListener(PickEventType.Picked, OnItemPicked);
        }

        private void OnDisable()
        {
            StopCoroutine(MoveToDirectionRoutine());

            _touchPanel.PointerMove -= OnPointerMoved;
            _touchPanel.PointerClick -= OnPointerClicked;
            _throwButton.onClick.RemoveListener(() => ThrowInput?.Invoke());
            _pickEventMediator.RemoveListener(PickEventType.Throwed, OnItemThrowed);
            _pickEventMediator.RemoveListener(PickEventType.Picked, OnItemPicked);
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

        private void OnPointerMoved(Vector2 lookDirection)
        => RotationInput?.Invoke(lookDirection);

        private void OnPointerClicked(Vector2 touchPosition)
        => TouchInput?.Invoke(touchPosition);

        private void OnItemPicked()
        => _throwButton.gameObject.SetActive(true);

        private void OnItemThrowed()
        => _throwButton.gameObject.SetActive(false);
    }
}
