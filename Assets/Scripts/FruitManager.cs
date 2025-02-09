using System.Collections.Generic;
using CharlieCares.ScoreSystem;
using UnityEngine;

namespace CharlieCares.FruitMerge
{
    public class FruitManager : MonoBehaviour
    {
        [SerializeField] private Transform _spawnRoot;
        [SerializeField] private MergeConfig _mergeConfig;
        [SerializeField] private TopViewController _topViewMap;

        private Fruit _previewFruit;

        private void Update()
        {
            if (!_previewFruit)
                return;

            _previewFruit.transform.position = new Vector3(10f * _topViewMap.CursorPosNormalized.x, 6f, 10f * _topViewMap.CursorPosNormalized.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ConfirmDropFruit();
            }
        }

        private void Start()
        {
            _topViewMap.OnClickOnMap += HandleClickOnTopViewMap;
            _previewFruit = SpawnRandomFruit();
        }

        private void OnDestroy()
        {
            _topViewMap.OnClickOnMap -= HandleClickOnTopViewMap;
        }

        public Fruit SpawnFruit(FruitConfig config, Vector3 spawnPos = default, bool preview = false)
        {
            if (config.Prefab == null)
            {
                Debug.LogError($"Fruit {config} does not have a prefab, please assgin one to its config.");
                return null;
            }

            Fruit fruit = Instantiate(config.Prefab, _spawnRoot);
            fruit.IsUnderPreview = preview;
            fruit.transform.position = spawnPos;
            fruit.SetConfig(config);
            fruit.OnCollidedWithFruit += HandleFruitCollision;
            return fruit;
        }

        public Fruit SpawnFruit(string fruitName, Vector3 spawnPos = default, bool preview = false)
        {
            FruitConfig chosenFruit = _mergeConfig.GetFruitConfigByName(fruitName);
            if (chosenFruit == null)
            {
                Debug.LogError($"Fruit type {fruitName} cannot be found.", this);
                return null;
            }
            return SpawnFruit(chosenFruit, spawnPos, preview);
        }

        public Fruit SpawnRandomFruit()
        {
            return SpawnFruit(_mergeConfig.GetRandomFruitConfig(), preview: true);
        }

        private void HandleClickOnTopViewMap(Vector2 posNormalized)
        {
            ConfirmDropFruit();
        }

        private void ConfirmDropFruit()
        {
            if (_previewFruit == null)
                return;

            _previewFruit.IsUnderPreview = false;
            _previewFruit = SpawnRandomFruit();
        }

        private void HandleFruitCollision(Fruit fruitA, Fruit fruitB)
        {
            FruitConfig newFruitType = _mergeConfig.GetNextFruitConfigInOrder(fruitA.Config);
            Vector3 spawnPos = (fruitA.transform.position + fruitB.transform.position) / 2;
            ScoreManager.AddScore(fruitA.Config.MergeScore);
            Destroy(fruitA.gameObject);
            Destroy(fruitB.gameObject);
            SpawnFruit(newFruitType, spawnPos);
        }
    }
}