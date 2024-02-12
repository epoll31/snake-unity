using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        print("scene loaded: " + scene.name);
        
        if (scene.name == "PauseScene") {
            print(gameObject.name);
            gameObject.SetActive(false);
        }
    }

    void OnSceneUnloaded(Scene scene) {
        print("scene unloaded: " + scene.name);

        if (scene.name == "PauseScene") {
            gameObject.SetActive(true);
        }
    }


}
