using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {

        public float m_scoreValue;
        public int m_currentWave;
        
        [Header("Health")]
        public float m_scoreTresholdForHealing;
        public float m_healingPercentage;
        
        [Header("Damage")]
        public float m_scoreTresholdForDamage;
        public float m_damagePercentage;
        
        [Header("Range")]
        public float m_scoreTresholdForLenght;
        public float m_lengtPercentage;
        public float m_scoreTresholdForWidth;
        public float m_widthPercentage;


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
