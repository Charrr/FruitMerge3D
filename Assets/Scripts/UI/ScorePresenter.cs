using UnityEngine;
using TMPro;
using CharlieCares.ScoreSystem;

namespace CharlieCares.FruitMerge
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtScore;

        private void Awake()
        {
            if (!_txtScore) _txtScore = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            ScoreManager.OnScoreChanged += UpdateScoreOnView;
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreChanged -= UpdateScoreOnView;
        }

        private void Start()
        {
            ScoreManager.ResetScore();
        }

        private void UpdateScoreOnView(float score)
        {
            _txtScore.text = $"Score: {score}";
        }
    }
}