using UnityEngine;
using UnityEngine.InputSystem;

namespace CharlieCares
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private float _rotateViewIncrement = 60f;
        [SerializeField] private float _zoomViewIncrement = 30f;
        [SerializeField] private float _minDistance = 6f, _maxDistance = 20f;
        [SerializeField] private Transform _originReference;
        private Camera _camera;
        private Keyboard _keyboard;
        private Mouse _mouse;

        private void Awake()
        {
            _camera = Camera.main;
            _keyboard = Keyboard.current;
            _mouse = Mouse.current;
            if (!_originReference) _originReference = transform.parent.GetChild(0);
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

            if (_keyboard.upArrowKey.isPressed)
            {
                OrbitViewUp();
            }
            else if (_keyboard.downArrowKey.isPressed)
            {
                OrbitViewDown();
            }

            float scrollY = _mouse.scroll.y.value;
            if (scrollY != 0)
            {
                ZoomView(scrollY * _zoomViewIncrement);
            }
        }

        private void OrbitViewHorizontally(float angle)
        {
            _camera.transform.RotateAround(_originReference.position, Vector3.up, angle * Time.deltaTime);
            _originReference.transform.eulerAngles = new Vector3(0f, _camera.transform.eulerAngles.y, 0f);
        }

        private void OrbitViewVertically(float angle)
        {
            float camPitch = _camera.transform.eulerAngles.x;
            float camPitchNormalized = camPitch < 180f ? camPitch : camPitch - 360f;
            float newAngle = camPitchNormalized + angle * Time.deltaTime;
            if (newAngle <= -90f || newAngle >= 90f)
                return;

            _camera.transform.RotateAround(_originReference.position, _originReference.right, angle * Time.deltaTime);
        }

        private void OrbitViewLeft()
        {
            OrbitViewHorizontally(_rotateViewIncrement);
        }

        private void OrbitViewRight()
        {
            OrbitViewHorizontally(-_rotateViewIncrement);
        }

        private void OrbitViewUp()
        {
            OrbitViewVertically(_rotateViewIncrement);
        }

        private void OrbitViewDown()
        {
            OrbitViewVertically(-_rotateViewIncrement);
        }

        private void ZoomView(float zoom)
        {
            float distance = Vector3.Distance(_originReference.position, _camera.transform.position);
            if ((distance <= _minDistance && zoom > 0) || (distance >= _maxDistance && zoom < 0))
                return;

            _camera.transform.Translate(Vector3.forward * zoom * Time.deltaTime, Space.Self);
        }
    }
}