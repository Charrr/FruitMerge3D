using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace CharlieCares.FruitMerge.Interaction
{
    public class TouchscreenInteractionManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _pnlInteractionView;

        [Header("Configuration")]
        [SerializeField] private Vector2 _dragFactor = Vector2.one * 5f;
        [SerializeField] private float _pinchFactor = 5f;
        
        private Vector2 _currentPosFinger0, _currentPosFinger1;
        private float _currentFingerDistance, _lastFingerDistance;
        private bool _isPinching = false;

        private InteractionManager _interactionManager;

        private const int _MAX_TOUCHCOUNT = 3;

        private void Awake()
        {
            _interactionManager = FindFirstObjectByType<InteractionManager>();
        }

        private void Update()
        {
            if (Touchscreen.current == null)
                return;

            var touches = Touchscreen.current.touches;

            var _validTouches = new List<TouchControl>(capacity: _MAX_TOUCHCOUNT);
            for (int i = 0; i < _MAX_TOUCHCOUNT; i++)
            {
                if (touches[i].isInProgress && !IsInInteractionView(touches[i].position.value))
                    _validTouches.Add(touches[i]);
            }

            if (_validTouches.Count == 1)
            {
                OrbitViewOnDrag(_validTouches[0].delta.value);
                _isPinching = false;
            }
            else if (_validTouches.Count == 2)
            {
                _currentPosFinger0 = _validTouches[0].position.value;
                _currentPosFinger1 = _validTouches[1].position.value;
                _currentFingerDistance = Vector2.Distance(_currentPosFinger0, _currentPosFinger1);

                if (!_isPinching)
                {
                    _lastFingerDistance = _currentFingerDistance;
                    _isPinching = true;
                    return;
                }

                ZoomOnPinch(_currentFingerDistance - _lastFingerDistance);
                _lastFingerDistance = _currentFingerDistance;
            }
            else
            {
                _isPinching = false;
            }
        }

        private void OrbitViewOnDrag(Vector2 drag)
        {
            _interactionManager.OrbitViewHorizontally(drag.x * _dragFactor.x);
            _interactionManager.OrbitViewVertically(-drag.y * _dragFactor.y);
        }

        private void ZoomOnPinch(float pinch)
        {
            if (pinch != 0)
                _interactionManager.ZoomView(pinch * _pinchFactor);
        }

        private bool IsInInteractionView(Vector2 point)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_pnlInteractionView, point);
        }
    }
}