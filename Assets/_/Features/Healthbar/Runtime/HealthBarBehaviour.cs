using System;
using Player.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public PlayerBehaviour m_player;
    private Image _componentImage;

    private void Awake()
    {
       _componentImage = GetComponent<Image>();
    }

    private void Update()
    {
        float normalizedHealth = Mathf.Clamp01(m_player.m_health / m_player.m_playerConfig.m_maxHealth);
        _componentImage.fillAmount = normalizedHealth;
    }
}
