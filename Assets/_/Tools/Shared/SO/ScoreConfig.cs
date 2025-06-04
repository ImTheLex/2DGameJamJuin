using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {

        public float m_scoreValue;
        public int m_currentWave;
        public float m_scoreTresholdForHealing;
        public float m_healingPercentage;
        public float m_scoreTresholdForDamage;
        

        public void SaveWaves(int waveCount)
        {
            m_currentWave = waveCount;
        }
        public void AddScore(float scoreValue)
        {
            m_scoreValue += scoreValue;
            
        }

        public void HealPlayer()
        {
            
        }
        public void ResetScore()
        {
            m_scoreValue = 0;
        }
    }
}
