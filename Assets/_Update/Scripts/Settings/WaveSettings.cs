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
        [SF] private Vector2Int _minMaxWaveCount = new Vector2Int(2, 3);
        [SF] private Vector2 _minMaxWaveLength   = new Vector2(8f, 10f);

        public Vector2Int MinMaxWaveCount => _minMaxWaveCount;
        public Vector2 MinMaxWaveLength   => _minMaxWaveLength;
    }
}