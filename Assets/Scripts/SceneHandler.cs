using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    void Start()
    {

    }

    public void ChangeScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        Time.timeScale = 0;

        SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PauseScene"));
    }

    public void ContinueFromPause()
    {
        Time.timeScale = 1;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlayScene"));



        // GameObject pauseButton = GameObject.Find("PauseButton");
        // pauseButton.SetActive(false);

        SceneManager.UnloadSceneAsync("PauseScene");
    }

    public void HomeFromPause()
    {
        // get all ExplosionHandler objects and explode them
        ExplosionHandler[] explosions = FindObjectsOfType<ExplosionHandler>();
        foreach (ExplosionHandler explosion in explosions)
        {
            explosion.Explode();
        }

        Time.timeScale = 1;
        SceneManager.LoadScene("HomeScene");
    }
}
