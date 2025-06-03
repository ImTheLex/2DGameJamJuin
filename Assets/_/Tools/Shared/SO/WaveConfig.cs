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
        public bool m_repeatEveryCentury;

        
        [Header("Wave Basic Phantom Config")]
        public int m_phantomAmount;
        public float m_basicHealthIncrement;
        public float m_basicSpeedIncrement;
        
        [Header("Wave Medium Phantom Config")]
        public int m_mediumPhantomAmount;
        public float m_mediumHealthIncrement;
        public float m_mediumSpeedIncrement;

        
        [Header("Wave Hard Phantom Config")]
        public int m_hardPhantomAmount;
        public float m_hardHealthIncrement;
        public float m_hardSpeedIncrement;


        
        [Header("Wave Boss Phantom Config")]
        public int m_bossPhantomAmount;
        public float m_bossHealthIncrement;
        public float m_bossSpeedIncrement;

        
        [HideInInspector]
        public int m_totalPhantomAmount => m_phantomAmount + m_mediumPhantomAmount + m_hardPhantomAmount + m_bossPhantomAmount;
    }
}
