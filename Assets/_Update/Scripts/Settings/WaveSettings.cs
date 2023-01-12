using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace Magnuth
{
    /// <summary>
    /// Gameplay settings for waves/encounters
    /// </summary>
    [CreateAssetMenu(fileName = "Wave Setting",
     menuName = "Magnuth/Wave Settings", order = 1)]
    public class WaveSettings : ScriptableObject
    {
        [SF] private int   _waveCount  = 1;
        [SF] private float _waveLength = 10.0f;

        public int WaveCount    => _waveCount;
        public float WaveLength => _waveLength;
    }
}