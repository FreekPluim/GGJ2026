using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public KeybindsSO keybinds;

    private void Update()
    {
        if (Input.GetKeyDown(keybinds.restartLevel))
        {
            LoadScene(2);
        }
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public static void LoadSceneRelative(int indexOffset)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + indexOffset);
    }
}
