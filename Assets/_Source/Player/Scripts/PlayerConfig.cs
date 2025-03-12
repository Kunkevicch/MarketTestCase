using UnityEngine;

namespace MarketTestCase
{
    [CreateAssetMenu(fileName = "PlayerConfig_", menuName = "Configs/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float HorizontalLookSensitivity { get; private set; }
        [field: SerializeField] public float VerticalLookSensitivity { get; private set; }
        [field: SerializeField] public float ItemPickDistance { get; private set; }
        [field: SerializeField] public float ThrowForce { get; private set; }
        [field: SerializeField] public LayerMask ItemLayer { get; private set; }
    }
}
