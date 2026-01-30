using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] MapManager mapManager;


    Vector3Int currentTile;

    private void Awake()
    {
        currentTile = mapManager.GetGridPositionFromPosition(transform.position);
        transform.position = currentTile;
    }

    private void Update()
    {
        if (mapManager != null)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (mapManager.CheckIsWalkable(currentTile + Vector3Int.right))
                {
                    MovePlayer(Vector3Int.right);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (mapManager.CheckIsWalkable(currentTile + Vector3Int.left))
                {
                    MovePlayer(Vector3Int.left);
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (mapManager.CheckIsWalkable(currentTile + Vector3Int.up))
                {
                    MovePlayer(Vector3Int.up);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (mapManager.CheckIsWalkable(currentTile + Vector3Int.down))
                {
                    MovePlayer(Vector3Int.down);
                }
            }

        }
    }

    void TryMove(Vector3Int direction)
    {
        currentTile += direction;
        transform.position = currentTile;
    }
}
