using SF = UnityEngine.SerializeField;
using UnityEngine;
using Variables;
using Magnuth;

namespace Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Engine : MonoBehaviour
    {
        #region OLD CODE
        //[SerializeField] private FloatVariable _throttlePower;
        //[SerializeField] private FloatVariable _rotationPower;

        //[SerializeField] private float _throttlePowerSimple;
        //[SerializeField] private float _rotationPowerSimple;
        #endregion

        [SF] private EngineSettings _settings = null;

        private Rigidbody2D _rigidbody;
        
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Throttle();
            }
        
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                SteerLeft();
            } 
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                SteerRight();
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    
        public void Throttle()
        {
            //_rigidbody.AddForce(transform.up * _throttlePower.Value, ForceMode2D.Force);
            _rigidbody.AddForce(transform.up * _settings.ThrottlePower, ForceMode2D.Force);
        }

        public void SteerLeft()
        {
            //_rigidbody.AddTorque(_rotationPower.Value, ForceMode2D.Force);
            _rigidbody.AddTorque(_settings.RotationPower, ForceMode2D.Force);
        }

        public void SteerRight()
        {
            //_rigidbody.AddTorque(-_rotationPower.Value, ForceMode2D.Force);
            _rigidbody.AddTorque(-_settings.RotationPower, ForceMode2D.Force);
        }
    }
}
