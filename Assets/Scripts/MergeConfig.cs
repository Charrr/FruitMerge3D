using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the order of merging.
/// </summary>
namespace CharlieCares.FruitMerge
{
    [CreateAssetMenu(fileName = "MergeConfig", menuName = "Scriptable Objects/MergeConfig")]
    public class MergeConfig : ScriptableObject
    {
        [SerializeField] private List<FruitConfig> _fruitConfigs = new();

        public int FruitConfigCount => _fruitConfigs.Count;

        private void OnValidate()
        {
            InitMergeScores();
        }

        public void InitMergeScores()
        {
            for (int i = 0; i < FruitConfigCount; i++)
            {
                _fruitConfigs[i].MergeScore = i + 1;
                Debug.Log($"{_fruitConfigs[i].Name} has been assigned MergeScore of {i + 1}.");
            }
        }

        public FruitConfig GetFruitConfigByIndex(int index)
        {
            if (index >= FruitConfigCount)
                Debug.LogError($"Index {index} out of range. Fallback to the last element.");
            return _fruitConfigs[Mathf.Clamp(index, 0, FruitConfigCount - 1)];
        }

        public FruitConfig GetFruitConfigByName(string query)
        {
            return _fruitConfigs.Find(fc => fc.name == query);
        }

        public FruitConfig GetRandomFruitConfig()
        {
            return _fruitConfigs[Random.Range(0, FruitConfigCount)];
        }

        public FruitConfig GetNextFruitConfigInOrder(FruitConfig fruit)
        {
            int fruitIndex = _fruitConfigs.IndexOf(fruit);
            if (fruitIndex < 0)
            {
                Debug.LogError($"Fruit type {fruit.Name} is not registered.", fruit);
                return null;
            }
            return _fruitConfigs[Mathf.Clamp(fruitIndex + 1, 0, FruitConfigCount - 1)];
        }
    }
}
