using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTitleLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    private void Start()
    {
        int currentRoomNumber = SceneManager.GetActiveScene().buildIndex;
        string levelTitle = LevelManager.Instance.levelTitle;

        label.text = $"Room {currentRoomNumber} -\n{levelTitle}";
    }
}
