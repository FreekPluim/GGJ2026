using UnityEngine;
using UnityEngine.Events;

public class Moveable : Obstacle
{
    public UnityEvent<Vector3Int, GameObject> ChangedPosition;

    public SpriteRenderer spriteRenderer;
    public Sprite onButton, offButton;

    public override void Start()
    {
        base.Start();
    }

    public void SetSpriteOn()
    {
        spriteRenderer.sprite = onButton;
    }

    public void SetSpriteOff()
    {
        spriteRenderer.sprite = offButton;
    }

    public override bool TryMove(Vector3Int direction)
    {
        if (MapManager.Instance.GetOccupiedTile(gridPosition + direction) != null && !MapManager.Instance.GetOccupiedTile(gridPosition + direction).walkable) return false;

        if (MapManager.Instance.CheckIsWalkable(gridPosition + direction))
        {
            gridPosition += direction;
            transform.position = gridPosition;
            MapManager.Instance.MoveOccupiedTile(this, direction);
            ChangedPosition.Invoke(gridPosition, gameObject);
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
                ChangedPosition.Invoke(gridPosition, gameObject);
                return true;
            }
        }
        return false;
    }
}
