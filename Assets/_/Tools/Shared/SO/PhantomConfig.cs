using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "PhantomConfig", menuName = "Scriptable Objects/PhantomConfig")]
    public class PhantomConfig : ScriptableObject
    {
        [Header("Basic Phantom Config")]
        public int m_basicHealth = 5;
        public float m_basicSpeed = 1;
        public float m_basicSpeedModifier;
        public int m_basicDamage;
        public float m_basicScoreValueOnDeath = 1;
        
        [Header("Medium Phantom Config")]
        public int m_mediumHealth = 10;
        public float m_mediumSpeed = 1;
        public float m_mediumSpeedModifier;
        public int m_mediumPhantomDamage;
        public float m_mediumScoreValueOnDeath = 2;

        [Header("Hard Phantom Config")]
        public int m_hardHealth = 15;
        public float m_hardSpeed = 1;
        public float m_hardSpeedModifier;
        public int m_hardPhantomDamage;
        public float m_hardScoreValueOnDeath = 3;
        
        [Header("Boss Phantom Config")]
        public int m_bossHealth = 50;
        public float m_bossSpeed = 1;
        public float m_bossSpeedModifier;
        public int m_bossPhantomDamage;
        public float m_bossScoreValueOnDeath = 5;

    }
}
