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
        public ScoreConfig m_scoreConfig;
        private float _scoreTresholdForHealing;
        
        private void Awake()
        {
            m_health = m_playerConfig.m_health;
            _scoreTresholdForHealing = m_scoreConfig.m_scoreTresholdForHealing;
        
        }

        private void Update()
        {
            HealPlayer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.TryGetComponent<PhantomBehaviour>(out PhantomBehaviour _phantomBehaviour))
            {
                TakeDamage(_phantomBehaviour.m_phantomDamage);
            }
        }

        private void HealPlayer()
        {
            if (m_scoreConfig.m_scoreValue >= _scoreTresholdForHealing)
            {
                _scoreTresholdForHealing += m_scoreConfig.m_scoreTresholdForHealing;
                var amount = (m_playerConfig.m_maxHealth / 100) * m_scoreConfig.m_healingPercentage;
                    
                
                if (m_health + amount >= m_playerConfig.m_maxHealth)
                {
                    m_health = m_playerConfig.m_maxHealth;
                }
                else
                {
                    m_health += amount;
                }
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
