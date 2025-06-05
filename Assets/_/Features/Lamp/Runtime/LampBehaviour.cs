using System;
using System.Diagnostics;
using Tools;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LampBehaviour : MonoBehaviour
{
    [Header("References")]
    private PhantomBehaviour _phantomBehaviour;
    public LampConfig _lampConfig;
    public ScoreConfig m_scoreConfig;
    private float _currentDamage;
    private float _configDamage;
    private float _currentLenght;
    private float _currentWidth;
    private float _scoreTresholdForDamage;
    private float _scoreTresholdForLenght;
    private float _scoreTresholdForWidth;
    

    private void Awake()
    {
        _configDamage = _lampConfig.m_damage;
        _currentDamage = _configDamage;
        _currentLenght = _lampConfig.m_lenght;
        _currentWidth = _lampConfig.m_width;
        transform.localScale = new Vector3(_currentWidth,_currentLenght, 0);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_phantomBehaviour == null)
        {
            _phantomBehaviour = other.GetComponent<PhantomBehaviour>();
        }
    }

    private void Update()
    {
        IncreaseDamage();
        IncreaseRange();
        IncreaseWidth();
    }

    private void IncreaseDamage()
    {
        if (m_scoreConfig.m_scoreValue >= _scoreTresholdForDamage)
        {
            _scoreTresholdForDamage += m_scoreConfig.m_scoreTresholdForDamage;
            float amount = (_configDamage / 100) * m_scoreConfig.m_damagePercentage;
            
              _currentDamage += amount;

        }
    }

    private void IncreaseRange()
    {
        if (m_scoreConfig.m_scoreValue >= _scoreTresholdForLenght)
        {
            _scoreTresholdForLenght += m_scoreConfig.m_scoreTresholdForLenght;
            float amount = (_currentLenght / 100) * m_scoreConfig.m_lengtPercentage;
            
            _currentLenght += amount;
            transform.localScale = new Vector3(_currentWidth,_currentLenght, 0);

        }
    }
    
    private void IncreaseWidth()
    {
        if (m_scoreConfig.m_scoreValue >= _scoreTresholdForWidth)
        {
            _scoreTresholdForWidth += m_scoreConfig.m_scoreTresholdForWidth;
            float amount = (_currentLenght / 100) * m_scoreConfig.m_widthPercentage;
            
            _currentWidth += amount;
            transform.localScale = new Vector3(_currentWidth,_currentLenght, 0);

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Other " + other.name + "Damage: " + _lampConfig.m_damage);
        
        //_phantomBehaviour.TakeDamage(_lampConfig.m_damage);
        
        var bh = other.GetComponent<PhantomBehaviour>();
        bh.TakeDamage(_currentDamage);
    }
   
}