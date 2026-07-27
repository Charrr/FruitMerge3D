using UnityEngine;
using UnityEngine.InputSystem;

namespace CharlieCares.FruitMerge.Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private float _rotateViewIncrement = 60f;
        [SerializeField] private float _zoomViewIncrement = 30f;
        [SerializeField] private float _minDistance = 6f, _maxDistance = 20f;
        [SerializeField] private Vector2 _dragFactor = Vector2.one * 5f;
        [SerializeField] private RectTransform _pnlInteractionView;
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
            if (_keyboard != null)
                UpdateKeyboardControls();
            if (_mouse != null)
                UpdateMouseControls();
        }

        private void UpdateKeyboardControls()
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
        }

        private void UpdateMouseControls()
        {
            float scrollY = _mouse.scroll.y.value;
            if (scrollY != 0)
            {
                ZoomView(scrollY * _zoomViewIncrement);
            }

            if (_mouse.leftButton.isPressed)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(_pnlInteractionView, _mouse.position.value))
                    return;
                Vector2 drag = _mouse.delta.value;
                OrbitViewHorizontally(drag.x * _dragFactor.x);
                OrbitViewVertically(-drag.y * _dragFactor.y);
            }
        }

        public void OrbitViewHorizontally(float angle)
        {
            _originReference.transform.eulerAngles += new Vector3(0f, angle * Time.deltaTime, 0f);
        }

        public void OrbitViewVertically(float angle)
        {
            float newRotX = _originReference.transform.eulerAngles.x + angle * Time.deltaTime;
            if (newRotX > 89.9f && newRotX <= 180f)
                newRotX = 89.9f;
            else if (newRotX > 180f && newRotX < 270.1f)
                newRotX = 270.1f;

            _originReference.transform.eulerAngles = new Vector3(newRotX, _originReference.transform.eulerAngles.y, _originReference.transform.eulerAngles.z);
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

        public void ZoomView(float zoom)
        {
            float newPosZ = _camera.transform.localPosition.z + zoom * Time.deltaTime;
            _camera.transform.localPosition = new Vector3(0f, 0f, Mathf.Clamp(newPosZ, -_maxDistance, -_minDistance));
        }
    }
}