using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace Magnuth
{
    [CreateAssetMenu(fileName = "Engine Settings",
     menuName = "Magnuth/Engine Settings", order = 4)]
    public class EngineSettings : ScriptableObject
    {
        [Header("Engine Settings")]
        [SF] private float _throttlePower = 2.76f;
        [SF] private float _rotationPower = 5.85f;

        public float ThrottlePower => _throttlePower;
        public float RotationPower => _rotationPower;
    }
}