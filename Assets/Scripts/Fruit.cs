using System;
using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class Fruit : MonoBehaviour
    {
        private FruitConfig _config;
        public FruitConfig Config => _config;
        public event Action<Fruit, Fruit> OnCollidedWithFruit;

        public void SetConfig(FruitConfig config)
        {
            transform.localScale = config.Scale * Vector3.one;
            name = config.Name + GetInstanceID();
            _config = config;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_config == null)
                return;

            if (collision.gameObject.TryGetComponent<Fruit>(out var otherFruit))
            {
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

