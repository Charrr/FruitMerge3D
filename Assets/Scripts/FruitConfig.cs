using UnityEngine;

namespace CharlieCares.FruitMerge
{
    [CreateAssetMenu(fileName = "FruitConfig", menuName = "Scriptable Objects/FruitConfig")]
    public class FruitData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, Range(0.01f, 10f)] private float _scale = 1f;
        public string Name => _name;
        public float Scale => Mathf.Max(_scale, 0f);
    }
}