using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetController : MonoBehaviour
{
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Reset the current scene
            ResetScene();
        }
    }

    void ResetScene()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
