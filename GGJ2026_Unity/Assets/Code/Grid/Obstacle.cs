using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3Int gridPosition;

    public bool fillAble = false;
    public bool walkable = false;

    public virtual void Start()
    {
        gridPosition = MapManager.Instance.GetGridPositionFromPosition(transform.position);
        MapManager.Instance.SetOccupiedTile(gridPosition, this);
        transform.position = gridPosition;
    }


    public virtual bool TryMove(Vector3Int direction)
    {
        return false;
    }
}
