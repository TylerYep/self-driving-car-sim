using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car;
        private Steering s;
        private int HighLevelControl;

        private void Awake()
        {
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

            s.UpdateValues();
            m_Car.Move(s.H, s.V, s.V, 0f);

        }
    }
}
