using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "PhantomConfig", menuName = "Scriptable Objects/PhantomConfig")]
    public class PhantomConfig : ScriptableObject
    {
        public int m_health = 5;
        public float m_speed = 2;
        public float m_speedModifier;
    }
}
