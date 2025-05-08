using System.Collections.Generic;
using UnityEngine;

namespace CharlieCares.FruitMerge.SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private MergeConfig _mergeConfig;
        private List<AudioSource> _mergeSoundSources = new();

        private void Start()
        {
            _mergeSoundSources.Clear();
            for (int i = 0; i < _mergeConfig.FruitConfigCount; i++)
            {
                var audioSource = transform.GetChild(0).gameObject.AddComponent<AudioSource>();
                audioSource.clip = _mergeConfig.GetFruitConfigByIndex(i).MergeSound;
                _mergeSoundSources.Add(audioSource);
            }
        }

        public void PlayMergeSound(FruitConfig fruitConfig)
        {
            _mergeSoundSources[_mergeConfig.GetIndexOfFruitConfig(fruitConfig)].Play();
        }
    }
}

