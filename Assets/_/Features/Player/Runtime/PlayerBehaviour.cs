using System;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.Runtime
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public float m_health;
        public PlayerConfig m_playerConfig;
        
        private void Awake()
        {
            m_health = m_playerConfig.m_health;
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.TryGetComponent<PhantomBehaviour>(out PhantomBehaviour _phantomBehaviour))
            {
                TakeDamage(_phantomBehaviour.m_phantomConfig.m_phantomDamage);
            }
        }

        private void TakeDamage(int damage)
        {
            m_health -= damage;
            if (m_health <= 0)
            {
                Debug.Log("Game Over");
                gameObject.SetActive(false);
                Time.timeScale = 0;
                SceneManager.LoadScene("Scoring_scene");
            }
        }
    }
}
