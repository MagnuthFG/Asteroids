using SF = UnityEngine.SerializeField;
using UnityEngine;

using Random = System.Random;
using System.Collections;
using UnityEditor;

namespace Magnuth
{
    [AddComponentMenu("Magnuth/Asteroid Handler")]
    public class AsteroidHandler : MonoBehaviour
    {
        [SF] private float _spawnRadius = 10f;
        [SF] private sQueue<SegmentSettings> _segments = new();

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

            var spacing  = 360f / count;
            var angleRad = Mathf.Deg2Rad * spacing;

            // add random start angle so it doesn't
            // start at 0 all the time
            for (int i = 0; i < count; i++){
                var index = random.Next(
                    0, segment.AsteroidPrefabs.Length
                );

                var prefab   = segment.AsteroidPrefabs[index];
                var settings = segment.AsteroidSettings[index];
                var asteroid = Instantiate(prefab);

                // Redo this! Assign settings from here instead
                asteroid.Settings = settings;

                asteroid.transform.position = new Vector3(
                    Mathf.Cos(angleRad * i),
                    Mathf.Sin(angleRad * i),
                    0f
                );
            }
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