using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarRemoteControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public float SteeringAngle { get; set; }
        public float Acceleration { get; set; }
        public int HighLevelControl { get; set; }

        private Steering s;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            s = new Steering();
            s.Start();
            HighLevelControl = 0; // 0 - straight, 1 - left, 2 - right

        }

        private void FixedUpdate()
        {
          // ### High level controls ###
          int oldControl = HighLevelControl;
          if (Input.GetKey(KeyCode.I)) {
            HighLevelControl = 0;
          } else if (Input.GetKey(KeyCode.J)) {
            HighLevelControl = 1;
          } else if (Input.GetKey(KeyCode.L)) {
            HighLevelControl = 2;
          }
          if (oldControl != HighLevelControl) {
            m_Car.setHighLevelControl(HighLevelControl);
          }

            // If holding down W or S control the car manually
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                s.UpdateValues();
                m_Car.Move(s.H, s.V, s.V, 0f);
            } else

            {
				m_Car.Move(SteeringAngle, Acceleration, Acceleration, 0f);
            }
        }
    }
}
