using UnityEngine;

namespace Interface
{
    public interface IHasHealth
    {
        //public GameObject GetGameObjectWithHealth();
        public MonoBehaviour GetGameObjectWithHealth();
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
    }
}
