using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "LampConfig", menuName = "Scriptable Objects/LampConfig")]
    public class LampConfig : ScriptableObject
    {

        public float m_damage;
        public float m_lenght;
        public float m_width;
    }
}
