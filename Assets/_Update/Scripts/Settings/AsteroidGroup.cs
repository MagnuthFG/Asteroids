using SF = UnityEngine.SerializeField;
using UnityEngine;
using Asteroids;

namespace Magnuth
{
    /// <summary>
    /// Gameplay settings for asteroids
    /// </summary>
    [CreateAssetMenu(fileName = "Asteroid Group",
     menuName = "Magnuth/Asteroid Group", order = 2)]
    public class AsteroidGroup : ScriptableObject
    {
        [SF] private Vector2Int _minMaxAsteroidCount = Vector2Int.one;
        [SF] private Vector2 _minMaxSpawnDelay = new Vector2(0.5f, 1);

        [SF] private AsteroidSettings[] _asteroidSettings = null;
        [SF] private Asteroid[] _asteroidPrefabs = null;

        public Vector2Int MinMaxCount   => _minMaxAsteroidCount;
        public Vector2 MinMaxSpawnDelay => _minMaxSpawnDelay;

        public AsteroidSettings[] AsteroidSettings => _asteroidSettings;
        public Asteroid[] AsteroidPrefabs => _asteroidPrefabs;
    }
}