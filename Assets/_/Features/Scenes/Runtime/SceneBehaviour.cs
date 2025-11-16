using Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Features.Scene.Runtime
{
    public class SceneBehaviour : MonoBehaviour
    {
    
    
    [SerializeField] private ScoreConfig m_scoreConfig;
    public void StartGame()
    {
        Time.timeScale = 1;
        m_scoreConfig.ResetScore();
        SceneManager.LoadScene(_mainGameScene);
        
    }

    public void ShowScore()
    {
        SceneManager.LoadScene(_scoringScene);
    }

    public void QuitGame()
    {
       m_scoreConfig.ResetScore();
       Application.Quit();
    }

    public void Return()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
    
    #region Privates

        [SerializeField] private string _mainMenuScene;
        [SerializeField] private string _mainGameScene;
        [SerializeField] private string _scoringScene;

    #endregion
    }
  
}
