using UnityEngine;
using UnityEngine.InputSystem;

namespace CharlieCares.FruitMerge.Interaction
{
    public class TouchscreenInteractionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference _dragAction;
        [SerializeField] private InputActionReference _primaryFingerPosAction, _secondaryFingerPosAction;
        [SerializeField] private RectTransform _pnlInteractionView;

        [Header("Configuration")]
        [SerializeField] private Vector2 _dragFactor = Vector2.one * 5f;
        [SerializeField] private float _pinchFactor = 5f;

        public Vector2 DebugDragValue;
        public Vector2 _currentPosFinger0, _currentPosFinger1;

        private Vector2 _lastPosFinger0, _lastPosFinger1;
        private float _currentFingerDistance, _lastFingerDistance;

        private InteractionManager _interactionManager;

        private void Awake()
        {
            _interactionManager = FindFirstObjectByType<InteractionManager>();
        }

        private void OnEnable()
        {
            _dragAction.action.Enable();
            _primaryFingerPosAction.action.Enable();
            _secondaryFingerPosAction.action.Enable();
        }

        private void OnDisable()
        {
            _dragAction.action.Disable();
            _primaryFingerPosAction.action.Disable();
            _secondaryFingerPosAction.action.Disable();
        }

        private void Update()
        {
            if (Touchscreen.current == null)
                return;

            int touchCount = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count;

            if (touchCount == 0)
                return;

            if (touchCount == 1)
            {
                _currentPosFinger0 = Touchscreen.current.touches[0].position.value;

                if (!IsInInteractionView(_currentPosFinger0))
                {
                    DebugDragValue = Touchscreen.current.touches[0].delta.value;
                    OrbitViewOnDrag(DebugDragValue);
                }
            }
        }

        private void OrbitViewOnDrag(Vector2 drag)
        {
            _interactionManager.OrbitViewHorizontally(drag.x * _dragFactor.x);
            _interactionManager.OrbitViewVertically(-drag.y * _dragFactor.y);
        }

        private bool IsInInteractionView(Vector2 point)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_pnlInteractionView, point);
        }
    }
}