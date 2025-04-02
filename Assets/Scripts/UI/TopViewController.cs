using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace CharlieCares.FruitMerge
{
    public class TopViewController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _imgIndicator;
        private RectTransform _rt;
        private bool _isPointerInside = false;
        public event Action<Vector2> OnClickOnMap;
        public Vector2 CursorPosNormalized => NormalizePositionOnMap(_imgIndicator.rectTransform.anchoredPosition);

        private void Reset()
        {
            ResolveReferences();
        }

        private void Awake()
        {
            ResolveReferences();
        }

        private void Update()
        {
            Vector2 pointerPos = Input.mousePosition;
            Vector2 pointerLocalPos = transform.InverseTransformPoint(pointerPos);

            _isPointerInside = _rt.rect.Contains(pointerLocalPos);

            if (_isPointerInside)
            {
                _imgIndicator.transform.position = pointerPos;
            }
        }

        private void ResolveReferences()
        {
            if (!_imgIndicator)
                _imgIndicator = GetComponentsInChildren<Image>().First(img => img.name.Contains("Indicator"));
            _rt = GetComponent<RectTransform>();
        }

        private Vector2 NormalizePositionOnMap(Vector2 localPos)
        {
            float width = _rt.rect.width;
            float height = _rt.rect.height;
            return new Vector2(localPos.x / width, localPos.y / height);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //if (_isPointerInside)
            Debug.Log("CursorPosNormalized: " + CursorPosNormalized);
            OnClickOnMap?.Invoke(CursorPosNormalized);
        }
    }
}

