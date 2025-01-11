using System.Collections.Generic;
using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class FruitManager : MonoBehaviour
    {
        [SerializeField] private Transform _spawnRoot;
        [SerializeField] private List<FruitConfig> _fruitConfigs;
        [SerializeField] private TopViewController _topViewMap;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnFruitRandomly();
            }
        }

        private void Start()
        {
            _topViewMap.OnClickOnMap += SpawnRandomFruitAtPosition;
        }

        private void OnDestroy()
        {
            _topViewMap.OnClickOnMap -= SpawnRandomFruitAtPosition;
        }

        public void SpawnFruit(FruitConfig config, Vector3 spawnPos)
        {
            if (config.Prefab == null)
            {
                Debug.LogError($"Fruit {config} does not have a prefab, please assgin one to its config.");
                return;
            }

            Fruit fruit = Instantiate(config.Prefab, _spawnRoot);
            fruit.transform.position = spawnPos;
            fruit.SetConfig(config);
            fruit.OnCollidedWithFruit += HandleFruitCollision;
        }

        public void SpawnFruit(string fruitName, Vector3 spawnPos)
        {
            FruitConfig chosenFruit = _fruitConfigs.Find(fc => fc.name == fruitName);
            if (chosenFruit == null)
            {
                Debug.LogError($"Fruit type {fruitName} cannot be found.");
                return;
            }
            SpawnFruit(chosenFruit, spawnPos);
        }

        public void SpawnFruitRandomly()
        {
            Vector3 spawnPos = new Vector3(Random.Range(-4f, 4f), 6f, Random.Range(-4f, 4f));
            SpawnFruit(_fruitConfigs[Random.Range(0, _fruitConfigs.Count)], spawnPos);
        }

        private void SpawnRandomFruitAtPosition(Vector2 posNormalized)
        {
            float width = 10f;
            float height = 10f;
            Vector3 spawnPos = new Vector3(width * posNormalized.x, 6f, height * posNormalized.y);
            SpawnFruit(_fruitConfigs[Random.Range(0, _fruitConfigs.Count)], spawnPos);
        }

        private void HandleFruitCollision(Fruit fruitA, Fruit fruitB)
        {
            int configIndex = _fruitConfigs.IndexOf(fruitA.Config);
            if (configIndex < 0)
            {
                Debug.LogError($"Fruit type {fruitA.Config.name} is not registered.", fruitA);
            }
            FruitConfig newFruitType = _fruitConfigs[Mathf.Clamp(configIndex + 1, 0, _fruitConfigs.Count - 1)];
            Vector3 spawnPos = (fruitA.transform.position + fruitB.transform.position) / 2;
            Destroy(fruitA.gameObject);
            Destroy(fruitB.gameObject);
            SpawnFruit(newFruitType, spawnPos);
        }
    }
}