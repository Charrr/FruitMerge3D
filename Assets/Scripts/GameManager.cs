using CharlieCares.ScoreSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CharlieCares.FruitMerge
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button _btnRestart;
        private FruitManager _fruitManager;

        private void Awake()
        {
            if (!_fruitManager) _fruitManager = FindAnyObjectByType<FruitManager>();
        }

        private void OnEnable()
        {
            _btnRestart.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _btnRestart.onClick.RemoveListener(RestartGame);
        }

        [ContextMenu("Restart Game")]
        public void RestartGame()
        {
            _fruitManager.ClearAllFruits();
            ScoreManager.ResetScore();
        }
    }
}