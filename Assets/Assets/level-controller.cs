using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Scene[] scenes;
    private int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel()
    {
        currentSceneIndex++;
        if (currentSceneIndex >= scenes.Length)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(scenes[currentSceneIndex].name);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(scenes[currentSceneIndex].name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
