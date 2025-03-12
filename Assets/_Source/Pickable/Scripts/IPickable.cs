using UnityEngine;

namespace MarketTestCase
{
    public interface IPickable
    {
        public void Pick(Transform parent);
        public void Throw(Vector3 direction, float force);
    }
}
