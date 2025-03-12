using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarketTestCase
{
    public class TouchPanel : MonoBehaviour, IToucheable
    {
        public event Action<Vector2> PointerMove;
        public event Action<Vector2> PointerClick;

        private bool _isPointerDown;
        private bool _isPointerDragged;

        private Vector2 _pointerStartPosition;
        private Vector2 _pointerDragDirection;

        private void Update()
        {
            if (!_isPointerDown) return;

            PointerMove?.Invoke(_pointerDragDirection);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
            _pointerStartPosition = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isPointerDragged = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isPointerDown) return;
            _isPointerDragged = true;
            _pointerDragDirection = (eventData.position - _pointerStartPosition).normalized;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isPointerDragged = false;
            _pointerDragDirection = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isPointerDragged)
                return;

            PointerClick?.Invoke(eventData.position);
        }
    }
}
