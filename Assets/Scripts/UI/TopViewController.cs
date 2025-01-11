using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CharlieCares.FruitMerge
{
    public class TopViewController : MonoBehaviour
    {
        [SerializeField] private Image _imgIndicator;
        private RectTransform _rt;
        private bool _isPointerInside = false;

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
    }
}

