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

        public Vector2Int MinMaxWaveCount => _waves.MinMaxWaveCount;
        public Vector2 MinMaxWaveLength   => _waves.MinMaxWaveLength;

        public Vector2Int MinMaxAsteroidCount   => _asteroids.MinMaxCount;
        public Vector2 MinMaxAsteroidSpawnDelay => _asteroids.MinMaxSpawnDelay;

        public AsteroidSettings[] AsteroidSettings => _asteroids.AsteroidSettings;
        public Asteroid[] AsteroidPrefabs => _asteroids.AsteroidPrefabs;
    }
}