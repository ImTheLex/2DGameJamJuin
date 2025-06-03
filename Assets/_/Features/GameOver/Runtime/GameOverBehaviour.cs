using TMPro;
using Tools;
using UnityEngine;

public class GameOverBehaviour : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public ScoreConfig m_scoreConfig;
    private void Awake()
    {
        _text = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText($"GAME OVER <br>Votre score: {m_scoreConfig.m_scoreValue}<br> Vagues Survécue: {m_scoreConfig.m_currentWave}");
    }

}
