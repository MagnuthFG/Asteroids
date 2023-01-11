using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace Magnuth
{
    /// <summary>
    /// Gameplay settings for asteroids
    /// </summary>
    [CreateAssetMenu(fileName = "Asteroids Setting",
     menuName = "Magnuth/Asteroids Settings", order = 1)]
    public class AsteroidsSettings : ScriptableObject
    {
        [SF] private Vector2Int _minMaxAsteroidCount = Vector2Int.one;
        [SF] private Vector2    _minMaxAsteroidSpeed = Vector2Int.one;
        [SF] private GameObject[] _asteroidPrefabs   = null;

        public Vector2Int MinMaxAsteroidCount => _minMaxAsteroidCount;
        public Vector2    MinMaxAsteroidSpeed => _minMaxAsteroidSpeed;
        public GameObject[] AsteroidPrefabs   => _asteroidPrefabs;
    }
}