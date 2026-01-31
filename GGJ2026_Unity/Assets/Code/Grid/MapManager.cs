using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<TileDataSO> tileData;

    [SerializeField] Tile walkableObstacle, endOpenTile, endClosedTile;

    private Dictionary<TileBase, TileDataSO> dataFromTiles;
    private Dictionary<Obstacle, Vector3Int> occupiedSpaces;

    [SerializeField] Transform end;
    Vector3Int endOnGrid;


    private void Awake()
    {
        Instance = this;

        dataFromTiles = new Dictionary<TileBase, TileDataSO>();
        occupiedSpaces = new Dictionary<Obstacle, Vector3Int>();

        foreach (var data in tileData)
        {
            foreach (var tile in data.tiles)
            {
                dataFromTiles.Add(tile, data);
            }
        }

        endOnGrid = GetGridPositionFromPosition(end.position);
    }

    public void CloseExit()
    {
        tilemap.SetTile(endOnGrid, endClosedTile);
    }
    public void OpenExit()
    {
        tilemap.SetTile(endOnGrid, endOpenTile);
    }

    public Vector3Int GetGridPositionFromPosition(Vector3 pos)
    {
        return tilemap.WorldToCell(pos);
    }
    public bool CheckIsWalkable(Vector3Int cell)
    {
        return dataFromTiles[tilemap.GetTile(cell)].Walkable;
    }
    public bool CheckIsObstacle(Vector3Int cell)
    {
        return dataFromTiles[tilemap.GetTile(cell)].Obstacle;
    }
    public ObstacleType GetObstacleType(Vector3Int cell)
    {
        return dataFromTiles[tilemap.GetTile(cell)].ObstacleType;
    }
    public TileDataSO GetTileDataFromCell(Vector3Int cell)
    {
        return dataFromTiles[tilemap.GetTile(cell)];
    }
    public void SetTileToWalkableObstacle(Vector3Int cell)
    {
        tilemap.SetTile(cell, walkableObstacle);
    }

    //Occupied Tile stuff
    public void SetOccupiedTile(Vector3Int cell, Obstacle obstacle)
    {
        occupiedSpaces.Add(obstacle, cell);
    }
    public Obstacle GetOccupiedTile(Vector3Int cell)
    {
        return occupiedSpaces.FirstOrDefault(c => c.Value == cell).Key;
    }
    public void MoveOccupiedTile(Obstacle obstacle, Vector3Int direction)
    {
        occupiedSpaces[obstacle] += direction;
    }
    public void RemoveOccupiedTile(Obstacle obstacle)
    {
        occupiedSpaces.Remove(obstacle);
        Destroy(obstacle.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(end.transform.position, 0.4f);
    }
}
