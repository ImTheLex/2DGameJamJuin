using System;
using System.Diagnostics;
using Tools;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LampBehaviour : MonoBehaviour
{
    private PhantomBehaviour _phantomBehaviour;
    public LampConfig _lampConfig;
    public ScoreConfig _scoreConfig;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_phantomBehaviour == null)
        {
            _phantomBehaviour = other.GetComponent<PhantomBehaviour>();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Other " + other.name + "Damage: " + _lampConfig.m_damage);
        
        //_phantomBehaviour.TakeDamage(_lampConfig.m_damage);
        
        var bh = other.GetComponent<PhantomBehaviour>();
        bh.TakeDamage(_lampConfig.m_damage);
    }
   
}
