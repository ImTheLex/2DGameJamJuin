using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float m_health = 100;
        public float m_maxHealth = 100;
    }
}
