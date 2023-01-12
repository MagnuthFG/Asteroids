using SF = UnityEngine.SerializeField;
using UnityEngine;
using Asteroids;

namespace Magnuth
{
    /// <summary>
    /// Gameplay segment settings
    /// </summary>
    [CreateAssetMenu(fileName = "Segment Setting",
     menuName = "Magnuth/Segment Settings", order = 0)]
    public class SegmentSettings : ScriptableObject
    {
        [SF] private WaveSettings _waves = null;
        [SF] private AsteroidGroupSettings _asteroids = null;

// PROPERTIES

        public int   WaveCount  => _waves.WaveCount;
        public float WaveLength => _waves.WaveLength;

        public Vector2Int MinMaxAsteroidCount => _asteroids.MinMaxCount;
        public AsteroidSettings[] AsteroidSettings => _asteroids.AsteroidSettings;
        public Asteroid[] AsteroidPrefabs => _asteroids.AsteroidPrefabs;
    }
}