using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Maingame_scene");
    }
}
