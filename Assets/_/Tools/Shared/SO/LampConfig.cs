using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "LampConfig", menuName = "Scriptable Objects/LampConfig")]
    public class LampConfig : ScriptableObject
    {

        public int m_damage;
        public int m_lenght;
        public int m_width;
    }
}
