using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace Magnuth
{
    [CreateAssetMenu(fileName = "Asteroid Settings",
     menuName = "Magnuth/Asteroid Settings", order = 3)]
    public class AsteroidSettings : ScriptableObject
    {
        [SF] private Vector2 _minMaxForce    = new Vector2(2, 4);
        [SF] private Vector2 _minMaxSize     = new Vector2(0.75f, 1);
        [SF] private Vector2 _minMaxTorque   = new Vector2(1, 3);
        [SF] private Vector2 _minMaxAccuracy = new Vector2(0, 1);
        
        public Vector2 MinMaxForce    => _minMaxForce;
        public Vector2 MinMaxSize     => _minMaxSize;
        public Vector2 MinMaxTorque   => _minMaxTorque;
        public Vector2 MinMaxAccuracy => _minMaxAccuracy;
    }
}
