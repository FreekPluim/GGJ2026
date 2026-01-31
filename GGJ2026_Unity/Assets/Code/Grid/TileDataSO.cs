using UnityEngine;
using UnityEngine.Tilemaps;


public enum ObstacleType { Hole, NONE }
[CreateAssetMenu(fileName = "New TileData", menuName = "Tiles/TileData")]
public class TileDataSO : ScriptableObject
{
    public TileBase[] tiles;
    public bool Walkable = false;
    public bool Obstacle = false;
    public bool Skippable = false;
    public ObstacleType ObstacleType = ObstacleType.NONE;

}
