using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        [Header("Wave Generic Settings")]
        public int m_startingWave;
        public int m_endingWave;
        public float m_spawnInterval;
        public float m_spawnIntervalReduction;
        public float m_spawnIntervalWave;
        public bool m_repeatEveryCentury;


        [Header("Wave Basic Phantom Config")] 
        public int m_extraBasicWaveFrequency;
        public int m_phantomAmount;
        
        public float m_basicHealthIncrement;
        public float m_basicSpeedIncrement;
        
        [Header("Wave Medium Phantom Config")]
        public int m_extraMediumWaveFrequency;
        public int m_mediumPhantomAmount;
        public float m_mediumHealthIncrement;
        public float m_mediumSpeedIncrement;

        
        [Header("Wave Hard Phantom Config")]
        public int m_extraHardWaveFrequency;
        public int m_hardPhantomAmount;
        public float m_hardHealthIncrement;
        public float m_hardSpeedIncrement;


        
        [Header("Wave Boss Phantom Config")]
        public int m_bossPhantomAmount;
        public float m_bossHealthIncrement;
        public float m_bossSpeedIncrement;


        private void OnValidate()
        {
            m_spawnIntervalWave = Mathf.Max(m_spawnIntervalWave, 0.01f);
            m_spawnIntervalReduction = Mathf.Max(m_spawnIntervalReduction,0.1f);
            m_extraBasicWaveFrequency = Mathf.Max(1, m_extraBasicWaveFrequency);
            m_extraMediumWaveFrequency = Mathf.Max(1, m_extraMediumWaveFrequency);
            m_extraHardWaveFrequency = Mathf.Max(1, m_extraHardWaveFrequency);
        }
        
        [HideInInspector]
        public int m_totalPhantomAmount => m_phantomAmount + m_mediumPhantomAmount + m_hardPhantomAmount + m_bossPhantomAmount;
    }
}
