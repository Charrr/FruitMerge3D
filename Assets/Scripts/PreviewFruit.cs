using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class PreviewFruit : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationDelta = Vector3.one;

        private void Update()
        {
            transform.Rotate(_rotationDelta);
        }
    }
}
