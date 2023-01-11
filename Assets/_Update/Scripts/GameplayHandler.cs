using SF = UnityEngine.SerializeField;
using UnityEngine;

using Random = System.Random;
using System.Collections;

namespace Magnuth
{
    [AddComponentMenu("Magnuth/Gameplay Handler")]
    public class GameplayHandler : MonoBehaviour
    {
        [SF] private sQueue<SegmentSettings> _segments = null;

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
        /// Executes the gameplay for each segment
        /// </summary>
        private IEnumerator RunSegment(SegmentSettings segment, Random random){
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


        }
    }
}