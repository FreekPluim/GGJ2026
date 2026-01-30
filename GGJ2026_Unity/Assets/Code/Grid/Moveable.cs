using UnityEngine;

public class Moveable : Obstacle
{
    public bool fillAble = true;

    public override bool TryMove(Vector3Int direction)
    {
        if (MapManager.Instance.CheckIsWalkable(gridPosition + direction))
        {
            return true;
        }
        if (MapManager.Instance.CheckIsObstacle(gridPosition + direction))
        {
            if (MapManager.Instance.GetObstacleType(gridPosition + direction) == ObstacleType.Hole)
            {
                return true;
            }
        }
        return false;
    }
}
