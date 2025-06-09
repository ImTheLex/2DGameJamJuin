using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "LampConfig", menuName = "Scriptable Objects/LampConfig")]
    public class LampConfig : ScriptableObject
    {

        public float m_damage;
        public float m_lenght;
        public float m_width;

        [Header("Ultimate")] 
        public float m_ultimateWidth;
        public float m_ultimateRangeBonus;
        public float m_ultimateDamageBonus;
        
    }
}
