using UnityEngine;
using UnityEngine.InputSystem;

namespace CharlieCares.FruitMerge.Interaction
{
    public class TouchscreenInteractionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference _dragAction;
        [SerializeField] private InputActionReference _primaryFingerPosAction, _secondaryFingerPosAction;

        [Header("Configuration")]
        [SerializeField] private Vector2 _rotateFactor = Vector2.one * 5f;

        public Vector2 DebugDragValue;
        public Vector2 DebugFingerPos0, DebugFingerPos1;

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
            DebugDragValue = _dragAction.action.ReadValue<Vector2>();
            _interactionManager.OrbitViewHorizontally(DebugDragValue.x * _rotateFactor.x);
            _interactionManager.OrbitViewVertically(-DebugDragValue.y * _rotateFactor.y);

            DebugFingerPos0 = _primaryFingerPosAction.action.ReadValue<Vector2>();
            DebugFingerPos1 = _secondaryFingerPosAction.action.ReadValue<Vector2>();
        }
    }
}