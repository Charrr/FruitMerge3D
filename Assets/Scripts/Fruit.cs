using System;
using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class Fruit : MonoBehaviour
    {
        private FruitConfig _config;
        private Rigidbody _rb;
        private bool _isUnderPreview = true;
        private PreviewFruit _previewBehaviour;

        public event Action<Fruit, Fruit> OnCollidedWithFruit;

        public FruitConfig Config => _config;
        public bool IsUnderPreview
        {
            get => _isUnderPreview;
            set
            {
                _isUnderPreview = value;
                _rb.detectCollisions = !value;
                _rb.useGravity = !value;
                if (value)
                {
                    if (!TryGetComponent(out _previewBehaviour))
                        _previewBehaviour = gameObject.AddComponent<PreviewFruit>();
                }
                else
                {
                    if (_previewBehaviour)
                        Destroy(_previewBehaviour);
                }
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void SetConfig(FruitConfig config)
        {
            name = config.Name + GetInstanceID();
            _config = config;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_config == null)
                return;

            // To avoid fruits from merging before being dropped.
            if (_isUnderPreview)
                return;

            if (collision.gameObject.TryGetComponent<Fruit>(out var otherFruit))
            {
                if (otherFruit.IsUnderPreview)
                    return;

                // Compare ID to avoid the same collision action being invoked twice.
                if (otherFruit.Config == _config && otherFruit.GetInstanceID() < GetInstanceID())
                {
                    Debug.Log($"{name} collided with {otherFruit.name}.", this);
                    OnCollidedWithFruit?.Invoke(this, otherFruit);
                }
            }
        }
    }
}