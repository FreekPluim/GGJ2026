using UnityEngine;

public class Moveable : Obstacle
{
    public bool fillAble = true;

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
                return true;
            }
        }
        return false;
    }
}
