using UnityEngine;
using UnityEngine.InputSystem;

namespace CharlieCares
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private float _rotateViewIncrement = 5f;
        private Camera _camera;
        private Keyboard _keyboard;

        private void Awake()
        {
            _camera = Camera.main;
            _keyboard = Keyboard.current;
        }

        private void Update()
        {
            if (_keyboard.leftArrowKey.isPressed)
            {
                OrbitViewLeft();
            }
            else if (_keyboard.rightArrowKey.isPressed)
            {
                OrbitViewRight();
            }
        }

        private void OrbitView(float angle)
        {
            _camera.transform.RotateAround(Vector3.zero, Vector3.up, angle * Time.deltaTime);
        }

        private void OrbitViewLeft()
        {
            OrbitView(_rotateViewIncrement);
        }

        private void OrbitViewRight()
        {
            OrbitView(-_rotateViewIncrement);
        }
    }
}