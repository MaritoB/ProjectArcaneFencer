using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerSingleton : MonoBehaviour
{
    private static SceneManagerSingleton instance;

    public static SceneManagerSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneManagerSingleton>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SceneManagerSingleton).Name);
                    instance = singletonObject.AddComponent<SceneManagerSingleton>();
                }
            }
            return instance;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TryLoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex < SceneManager.sceneCountInBuildSettings -1) 
        {
            ++sceneIndex;
        }
        else
        {
            sceneIndex = 0;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
