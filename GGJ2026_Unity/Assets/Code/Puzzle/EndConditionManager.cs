using UnityEngine;

public class EndConditionManager : MonoBehaviour
{
    public static EndConditionManager instance;
    private void Awake()
    {
        instance = this;
    }

    public int neededCount;
    int count;

    public void AddOneToEnd()
    {
        count++;
        CheckAllActive();
    }
    public void RemoveOneToEnd()
    {
        count--;
        CheckAllActive();
    }

    public bool CheckAllActive()
    {
        if (neededCount != count)
        {
            MapManager.Instance.CloseExit();
            return false;
        }

        MapManager.Instance.OpenExit();
        if (AudioManager.instance != null) AudioManager.instance.PlayOneShot("OnOpenDoor");
        return true;
    }
}
