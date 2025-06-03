using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {

        public float m_scoreValue;
        public int m_currentWave;


        public void SaveWaves(int waveCount)
        {
            m_currentWave = waveCount;
        }
        public void AddScore(float scoreValue)
        {
            m_scoreValue += scoreValue;
        }

        public void ResetScore()
        {
            m_scoreValue = 0;
        }
    }
}
