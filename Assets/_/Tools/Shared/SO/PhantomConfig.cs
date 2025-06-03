using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "PhantomConfig", menuName = "Scriptable Objects/PhantomConfig")]
    public class PhantomConfig : ScriptableObject
    {
        public int m_health = 5;
        public float m_speed = 1;
        public float m_speedModifier;
        public int m_phantomDamage;
        public float m_scoreValueOnDeath = 1;
    }
}
