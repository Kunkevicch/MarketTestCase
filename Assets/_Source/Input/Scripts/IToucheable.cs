using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarketTestCase
{
    public interface IToucheable : IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
    {
        public event Action<Vector2> PointerMove;
        public event Action<Vector2> PointerClick;
    }
}
