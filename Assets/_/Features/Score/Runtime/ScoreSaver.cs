using System;
using System.IO;
using TMPro;
using Tools;
using UnityEditorInternal;
using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public ScoreConfig _scoreConfig;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SaveScore();
        }
    }

    public void SaveScore()
    {
        Debug.Log(_text.text);
        var userName = _text.text;
        var scoredata = $"{userName} - Score: {_scoreConfig.m_scoreValue} Waves: {_scoreConfig.m_currentWave}";
        var json = JsonUtility.ToJson(scoredata);
        File.WriteAllText(Application.persistentDataPath + "/score  .json", json);

    }

    public void LoadScore()
    {
        
    }
}
