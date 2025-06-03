using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        [Header("Wave Generic Settings")]
        public int m_startingWave;
        public int m_endingWave;
        public bool m_repeatEveryCentury;
        
        [Header("Wave Basic Phantom Config")]
        public int m_phantomAmount;
        public float m_phantomHealthIncrement;
        public int m_phantomHealthIncrementFrequency;
        
        [Header("Wave Medium Phantom Config")]
        public int m_mediumPhantomAmount;
        
        [Header("Wave Hard Phantom Config")]
        public int m_hardPhantomAmount;
        
        [HideInInspector]
        public int m_totalPhantomAmount => m_phantomAmount + m_mediumPhantomAmount + m_hardPhantomAmount;
    }
}
