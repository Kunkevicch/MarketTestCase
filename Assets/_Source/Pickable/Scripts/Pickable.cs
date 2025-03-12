using UnityEngine;

namespace MarketTestCase
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pickable : MonoBehaviour, IPickable
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Pick(Transform parent)
        {
            _rb.isKinematic = true;
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }

        public void Throw(Vector3 direction, float force)
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
