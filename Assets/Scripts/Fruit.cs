using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class Fruit : MonoBehaviour
    {
        private FruitConfig _config;

        public void SetConfig(FruitConfig config)
        {
            transform.localScale = config.Scale * Vector3.one;
            name = config.Name;
            _config = config;
        }
    }
}

