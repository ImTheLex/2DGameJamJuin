using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    private Scene _previousScene;
    public void StartGame()
    {
        _previousScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Maingame_scene");
        
    }

    public void ShowScore()
    {
        _previousScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Scoring_scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Return()
    {
        //SceneManager.LoadScene(_previousScene.buildIndex);
        SceneManager.LoadScene("mainmenu_scene");
    }
}
