using SF = UnityEngine.SerializeField;
using Random = System.Random;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Asteroids;

namespace Magnuth
{
    [AddComponentMenu("Magnuth/Asteroid Handler")]
    public class AsteroidHandler : MonoBehaviour
    {
        [Header("Handler Settings")]
        [SF] private float _spawnRadius = 10f;
        [Space, SF] private sQueue<SegmentSettings> _segments = new();

// PROPERTIES

        /// <summary>
        /// Returns the current gameplay segment
        /// </summary>
        public SegmentSettings Segment { get; private set; }

// INITIALISATION

        /// <summary>
        /// Starts the gameplay segment loop
        /// </summary>
        private IEnumerator Start(){
            var random = InitRandom();

            while (_segments.Count > 0){
                Segment = _segments.Dequeue();
                yield return RunSegment(Segment, random);
            }
        }

        /// <summary>
        /// Returns a new Random with a DateTime seed
        /// </summary>
        private Random InitRandom(){
            var date = System.DateTime.Now;
            var seed = $"{date.Year}{date.Hour}{date.Minute}";
            
            return new Random(int.Parse(seed));
        }

// SEGMENT HANDLING

        /// <summary>
        /// Executes the gameplay for this segment
        /// </summary>
        private IEnumerator RunSegment(SegmentSettings segment, Random random){
            if (segment == null) yield break;
            
            var wave = 0;

            while (++wave < segment.WaveCount){
                SpawnAsteroids(segment, random);

                var delay = new WaitForSeconds(
                    segment.WaveLength
                );

                yield return delay;
            }
        }

        /// <summary>
        /// Spawns the asteroids in the scene
        /// </summary>
        private void SpawnAsteroids(SegmentSettings segment, Random random){
            var count = random.Next(
                segment.MinMaxAsteroidCount.x,
                segment.MinMaxAsteroidCount.y
            );

            var spacing   = 360f / count;
            var angleRad  = Mathf.Deg2Rad * spacing;
            var offsetRad = Mathf.Deg2Rad * random.Next(-180, 180);

            for (int i = 0; i < count; i++){
                var index = random.Next(
                    0, segment.AsteroidPrefabs.Length
                );

                var x = Mathf.Cos(offsetRad + angleRad * i) * _spawnRadius;
                var y = Mathf.Sin(offsetRad + angleRad * i) * _spawnRadius;
                var position = new Vector2(x, y);

                InstantiateAsteroids(
                    segment.AsteroidPrefabs[index],  position,
                    segment.AsteroidSettings[index], random
                );
            }
        }

        /// <summary>
        /// Instantiates the asteroid with pseudo-random settings
        /// </summary>
        private void InstantiateAsteroids(Asteroid prefab, 
        Vector2 position, AsteroidSettings settings, Random random){
            var asteroid = Instantiate(prefab);

            asteroid.Size      = GetSize(settings, random);
            asteroid.Direction = GetDirection(position, settings, random);
            asteroid.Force     = GetForce(settings, random);
            asteroid.Torque    = GetTorque(settings, random);

            asteroid.transform.position = position;
        }

// SETTINGS HANDLING

        /// <summary>
        /// Returns the asteroid force
        /// </summary>
        private float GetForce(AsteroidSettings settings, Random random){
            return Mathf.Lerp(
                settings.MinMaxForce.x,
                settings.MinMaxForce.y,
                (float)random.NextDouble()
            );
        }

        /// <summary>
        /// Returns the asteroid torque
        /// </summary>
        private float GetTorque(AsteroidSettings settings, Random random){
            return Mathf.Lerp(
                settings.MinMaxTorque.x,
                settings.MinMaxTorque.y,
                (float)random.NextDouble()
            );
        }

        /// <summary>
        /// Returns the asteroid size
        /// </summary>
        private Vector2 GetSize(AsteroidSettings settings, Random random){
            var x = Mathf.Lerp(
                settings.MinMaxSize.x, 
                settings.MinMaxSize.y,
                (float)random.NextDouble()
            );

            var y = Mathf.Lerp(
                settings.MinMaxSize.x,
                settings.MinMaxSize.y,
                (float)random.NextDouble()
            );

            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns the asteroid direction
        /// </summary>
        private Vector2 GetDirection(Vector2 position, AsteroidSettings settings, Random random){
            var x = Mathf.Lerp(
                settings.MinMaxAccuracy.x,
                settings.MinMaxAccuracy.y, 
                (float)random.NextDouble()
            );

            var y = Mathf.Lerp(
                settings.MinMaxAccuracy.x,
                settings.MinMaxAccuracy.y,
                (float)random.NextDouble()
            );

            var target = new Vector2(
                Mathf.Lerp(-x, x, (int)random.NextDouble()),
                Mathf.Lerp(-y, y, (int)random.NextDouble())
            );

            return (target - position).normalized;
        }

// DEBUGGING
#if UNITY_EDITOR

        /// <summary>
        /// Draws the spawn radius circle
        /// </summary>
        private void OnDrawGizmosSelected(){
            Handles.color = Color.white;

            Handles.DrawWireDisc(
                transform.position, 
                Vector3.forward, 
                _spawnRadius, 2f
            );
        }

#endif
    }
}