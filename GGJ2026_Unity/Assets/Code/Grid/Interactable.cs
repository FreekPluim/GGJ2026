using UnityEngine;
using UnityEngine.Events;

public class Interactable : Obstacle
{
    [SerializeField] UnityEvent onActivate;
    [SerializeField] UnityEvent onDeactivate;

    public override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onActivate.Invoke();
        Debug.Log("Enter invoked");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onDeactivate.Invoke();
        Debug.Log("Exit invoked");
    }

}
