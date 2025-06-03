using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    private Scene _previousScene;
    [SerializeField]
    private ScoreConfig m_scoreConfig;
    public void StartGame()
    {
        _previousScene = SceneManager.GetActiveScene();
        m_scoreConfig.ResetScore();
        SceneManager.LoadScene("Maingame_scene");
        Time.timeScale = 1;
        
    }

    public void ShowScore()
    {
        _previousScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Scoring_scene");
    }

    public void QuitGame()
    {
       m_scoreConfig.ResetScore();
       Application.Quit();
    }

    public void Return()
    {
        //SceneManager.LoadScene(_previousScene.buildIndex);
        SceneManager.LoadScene("mainmenu_scene");
    }
}
