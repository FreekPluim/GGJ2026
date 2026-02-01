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
        PlayerController.OnPositionChanged += CheckOverlap;
    }
    private void OnDestroy()
    {
        PlayerController.OnPositionChanged -= CheckOverlap;
    }

    public void CheckOverlap(Vector3Int objectPosition, GameObject obj)
    {
        if (objectPosition == gridPosition)
        {
            objectOnTop = obj;
            if (obj.TryGetComponent(out Moveable moveable))
            {
                moveable.SetSpriteOn();
            }
            if (AudioManager.instance != null) AudioManager.instance.PlayOneShot("OnButtonClicked");
            onActivate.Invoke();
        }
        else
        {
            if (objectOnTop != obj) return;
            else
            {
                if (obj.TryGetComponent(out Moveable moveable))
                {
                    moveable.SetSpriteOff();
                }
                objectOnTop = null;
                onDeactivate.Invoke();
            }
        }
    }

}
