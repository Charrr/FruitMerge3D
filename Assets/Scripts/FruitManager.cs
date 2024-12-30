using System.Collections.Generic;
using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class FruitManager : MonoBehaviour
    {
        [SerializeField] private Fruit _fruitPrefab;
        [SerializeField] private Transform _spawnRoot;

        [SerializeField] private List<FruitConfig> _fruitConfigs;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnFruit();
            }
        }

        public void SpawnFruit()
        {
            Fruit fruit = Instantiate(_fruitPrefab, _spawnRoot);
            fruit.transform.position = new Vector3(Random.Range(-4f, 4f), 6f, Random.Range(-4f, 4f));
            fruit.SetConfig(_fruitConfigs[Random.Range(0, _fruitConfigs.Count)]);
        }
    }
}