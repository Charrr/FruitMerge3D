using UnityEngine;

namespace CharlieCares.FruitMerge
{
    [CreateAssetMenu(fileName = "FruitConfig", menuName = "Scriptable Objects/FruitConfig")]
    public class FruitConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Fruit _prefab;
        [SerializeField] private AudioClip _mergeSound;
        public string Name => _name;
        public Fruit Prefab => _prefab;
        public int MergeScore { get; set; }
        public AudioClip MergeSound => _mergeSound;
    }
}