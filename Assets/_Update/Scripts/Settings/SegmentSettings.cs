using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace Magnuth
{
    /// <summary>
    /// Gameplay segment settings
    /// </summary>
    [CreateAssetMenu(fileName = "Segment Setting",
     menuName = "Magnuth/Segment Settings", order = 2)]
    public class SegmentSettings : ScriptableObject
    {
        [SF] private WaveSettings _waves = null;
        [SF] private AsteroidsSettings _asteroids = null;

// PROPERTIES

        public int   WaveCount  => _waves.WaveCount;
        public float WaveLength => _waves.WaveLength;

        public Vector2Int MinMaxAsteroidCount
            => _asteroids.MinMaxAsteroidCount;
        public Vector2 MinMaxAsteroidSpeed
            => _asteroids.MinMaxAsteroidSpeed;
        public GameObject[] AsteroidPrefabs
            => _asteroids.AsteroidPrefabs;
    }
}