using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3Int gridPosition;

    private void Awake()
    {
        gridPosition = MapManager.Instance.GetGridPositionFromPosition(transform.position);
        MapManager.Instance.SetOccupiedTile(gridPosition, this);
    }


    public virtual bool TryMove(Vector3Int direction)
    {
        return false;
    }
}
