using System;
using TMPro;
using Tools;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public ScoreConfig m_scoreConfig;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _text = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText($"Score: {m_scoreConfig.m_scoreValue}");
    }
}
