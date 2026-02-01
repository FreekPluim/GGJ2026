using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private KeybindsSO keybinds;

    [Space]
    public string levelTitle;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keybinds.restartLevel))
        {
            LoadSceneRelative(0);
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
