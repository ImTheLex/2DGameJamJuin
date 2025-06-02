using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {

        public int m_scoreValue;

        public void AddScore(int scoreValue)
        {
            m_scoreValue += scoreValue;
        }

        public void ResetScore()
        {
            m_scoreValue = 0;
        }
    }
}
