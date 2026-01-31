using UnityEngine;
using UnityEngine.Events;

public class Moveable : Obstacle
{
    public UnityEvent<Vector3Int, GameObject> moved;

    public override void Start()
    {
        base.Start();
    }

    public override bool TryMove(Vector3Int direction, out bool KillSelf)
    {
        KillSelf = false;

        if (MapManager.Instance.CheckIsWalkable(gridPosition + direction))
        {
            gridPosition += direction;
            transform.position = gridPosition;
            MapManager.Instance.MoveOccupiedTile(this, direction);
            moved.Invoke(gridPosition, gameObject);
            return true;
        }
        if (MapManager.Instance.CheckIsObstacle(gridPosition + direction))
        {
            if (MapManager.Instance.GetObstacleType(gridPosition + direction) == ObstacleType.Hole)
            {
                gridPosition += direction;
                transform.position = gridPosition;
                MapManager.Instance.SetTileToWalkableObstacle(gridPosition);
                MapManager.Instance.RemoveOccupiedTile(this);
                KillSelf = true;
                moved.Invoke(gridPosition, gameObject);
                return true;
            }
        }
        return false;
    }
}
