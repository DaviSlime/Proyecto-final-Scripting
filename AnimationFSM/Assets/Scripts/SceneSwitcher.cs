using UnityEngine;
using UnityEngine.SceneManagement;   

public class SceneSwitcher : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is null or empty!");
            return;
        }

        Debug.Log("Attempting to load scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
            return;
        }

        Debug.Log("Attempting to load scene with index: " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

}