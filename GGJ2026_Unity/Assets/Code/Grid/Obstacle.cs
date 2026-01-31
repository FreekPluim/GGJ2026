using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3Int gridPosition;

    public virtual void Start()
    {
        gridPosition = MapManager.Instance.GetGridPositionFromPosition(transform.position);
        MapManager.Instance.SetOccupiedTile(gridPosition, this);
        transform.position = gridPosition;
    }


    public virtual bool TryMove(Vector3Int direction, out bool KillSelf)
    {
        KillSelf = false;
        return false;
    }
}
