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

        [SF] private Transform _body = null;
        [SF] private ScriptableEventInt _onDestroyed = null;

        private int     _instanceID = -1;
        private Vector2 _direction  = Vector2.zero;
        private Rigidbody2D _rigidbody = null;

        private const string LASER_TAG = "Laser";

// SETTINGS

        public float Force {
            get { return _rigidbody.totalTorque; }
            set { _rigidbody.AddForce(_direction * value, ForceMode2D.Impulse); }
        }

        public float Torque {
            get { return _rigidbody.totalTorque; }
            set { _rigidbody.AddTorque(value, ForceMode2D.Impulse); }
        }

        public Vector2 Direction {
            get { return _direction;  }
            set { _direction = value; }
        }

        public Vector2 Size {
            get { return _body.localScale;  }
            set { _body.localScale = value; }
        }

// INITIALISATION

        private void Awake(){
            _rigidbody  = GetComponent<Rigidbody2D>();
            _instanceID = GetInstanceID();
        }

        #region OLD CODE
        //private void Start(){
        //    SetDirection();
        //    AddForce();
        //    AddTorque();
        //    SetSize();
        //}

        //private void SetDirection(){
        //    var size = new Vector2(3f, 3f);
        //    var target = new Vector3(
        //        Random.Range(-size.x, size.x),
        //        Random.Range(-size.y, size.y)
        //    );

        //    _direction = (target - transform.position).normalized;
        //}

        //private void AddForce(){
        //    var force = Random.Range(_minForce, _maxForce);
        //    _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        //}

        //private void AddTorque(){
        //    var torque = Random.Range(_minTorque, _maxTorque);

        //    var roll = Random.Range(0, 2);
        //    if (roll == 0) torque = -torque;

        //    _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        //}

        //private void SetSize(){
        //    var size = Random.Range(_minSize, _maxSize);
        //    _transform.localScale = new Vector3(size, size, 0f);
        //}
        #endregion

// DAMAGE HANDLING

        // This does nothing cause the asteroid has no trigger collider?!
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
