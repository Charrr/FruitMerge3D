using UnityEngine;

namespace CharlieCares.FruitMerge.SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _mergeSoundSource;

        public void PlayMergeSound(FruitConfig config)
        {
            _mergeSoundSource.clip = config.MergeSound;
            _mergeSoundSource.Play();
        }
    }
}

