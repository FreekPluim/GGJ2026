using UnityEngine;
using UnityEngine.Events;

public class Interactable : Obstacle
{
    [SerializeField] UnityEvent onActivate;
    [SerializeField] UnityEvent onDeactivate;

    [SerializeField] GameObject objectOnTop;

    public override void Start()
    {
        base.Start();
    }

    public void CheckOverlap(Vector3Int objectPosition, GameObject obj)
    {
        if (objectPosition == gridPosition)
        {
            objectOnTop = obj;
            onActivate.Invoke();
        }
        else
        {
            if (objectOnTop != obj) return;
            else
            {
                objectOnTop = null;
                onDeactivate.Invoke();
            }
        }
    }

}
