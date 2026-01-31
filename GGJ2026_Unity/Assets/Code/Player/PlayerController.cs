using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] KeybindsSO keybinds;

    Vector3Int currentTile;

    private void Start()
    {
        currentTile = MapManager.Instance.GetGridPositionFromPosition(transform.position);
        transform.position = currentTile;
    }

    private void Update()
    {
        if (MapManager.Instance != null)
        {
            if (Input.GetKeyDown(keybinds.Right))
            {
                TryMove(Vector3Int.right);
            }
            if (Input.GetKeyDown(keybinds.Left))
            {
                TryMove(Vector3Int.left);
            }
            if (Input.GetKeyDown(keybinds.Up))
            {
                TryMove(Vector3Int.up);
            }
            if (Input.GetKeyDown(keybinds.Down))
            {
                TryMove(Vector3Int.down);
            }
        }
    }

    void TryMove(Vector3Int direction)
    {
        if (!MapManager.Instance.CheckIsWalkable(currentTile + direction)) { return; }
        if (MapManager.Instance.GetOccupiedTile(currentTile + direction) != null)
        {
            if (MapManager.Instance.GetOccupiedTile(currentTile + direction).walkable)
            {
                currentTile += direction;
                transform.position = currentTile;
                return;
            }

            if (MapManager.Instance.GetOccupiedTile(currentTile + direction).TryMove(direction, out bool KillSelf))
            {
                //MoveSelf
                currentTile += direction;
                transform.position = currentTile;
                return;
            }
        }
        else
        {
            currentTile += direction;
            transform.position = currentTile;
            return;
        }
    }
}
