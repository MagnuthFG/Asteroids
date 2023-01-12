using SF = UnityEngine.SerializeField;
using Random = UnityEngine.Random;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;
using Magnuth;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        #region OLD CODE
        //[SerializeField] private ScriptableEventInt _onAsteroidDestroyed;

        //[Header("Config:")]
        //[SerializeField] private float _minForce;
        //[SerializeField] private float _maxForce;
        //[SerializeField] private float _minSize;
        //[SerializeField] private float _maxSize;
        //[SerializeField] private float _minTorque;
        //[SerializeField] private float _maxTorque;

        //[Header("References:")]
        //[SerializeField] private Transform _shape;
        #endregion

        public AsteroidSettings Settings = null;
        [SF] private ScriptableEventInt _onDestroyed = null;

        private int     _instanceID = -1;
        private Vector3 _direction  = Vector3.zero;

        private Transform   _transform = null;
        private Rigidbody2D _rigidbody = null;

        private const string LASER_TAG = "Laser";

// INITIALISATION

        private void Awake(){
            _rigidbody  = GetComponent<Rigidbody2D>();
            _instanceID = GetInstanceID();
            _transform  = transform;
        }

        private void Start(){
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
        }
        

        private void SetDirection(){
            var size = new Vector2(3f, 3f);
            var target = new Vector3(
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce(){
            var force = Random.Range(
                Settings.MinMaxForce.x, 
                Settings.MinMaxForce.y
            );
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque(){
            var torque = Random.Range(
                Settings.MinMaxTorque.x,
                Settings.MinMaxTorque.y
            );

            var roll = Random.Range(0, 2);
            if (roll == 0) torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize(){
            var size = Random.Range(
                Settings.MinMaxSize.x,
                Settings.MinMaxSize.y
            );
            _transform.localScale = new Vector3(size, size, 0f);
        }

// DAMAGE HANDLING

        private void OnTriggerEnter2D(Collider2D other){
            //if (string.Equals(other.tag, "Laser")){
            //   HitByLaser();
            //}

            // Recomended by Unity
            if (other.CompareTag(LASER_TAG)){ 
                HitByLaser();
            }
        }

        private void HitByLaser(){
            _onDestroyed.Raise(_instanceID);
            Destroy(gameObject);
        }

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
        public void OnHitByLaser(IntReference asteroidId){
            if (_instanceID == asteroidId.GetValue()){
                Destroy(gameObject);
            }
        }
        
        public void OnHitByLaserInt(int asteroidId){
            if (_instanceID == asteroidId){
                Destroy(gameObject);
            }
        }
    }
}
