using System;
using UnityEngine;

namespace CharlieCares.ScoreSystem
{
    public class ScoreManager
    {
        private static uint _currentScore = 0;
        public static uint CurrentScore => _currentScore;
        public static event Action<float> OnScoreChanged;

        public static void AddScore(uint increment)
        {
            _currentScore += increment;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public static void ResetScore()
        {
            _currentScore = 0;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}