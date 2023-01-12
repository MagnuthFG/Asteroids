using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace Magnuth
{
    [CreateAssetMenu(fileName = "Asteroid Settings",
     menuName = "Magnuth/Asteroid Settingss", order = 3)]
    public class AsteroidSettings : ScriptableObject
    {
        [SF] private Vector2 _minMaxForce  = new Vector2(2, 6);
        [SF] private Vector2 _minMaxSize   = new Vector2(0.25f, 1);
        [SF] private Vector2 _minMaxTorque = new Vector2(0.1f, 0.5f);

        public Vector2 MinMaxForce  => _minMaxForce;
        public Vector2 MinMaxSize   => _minMaxSize;
        public Vector2 MinMaxTorque => _minMaxTorque;
    }
}
