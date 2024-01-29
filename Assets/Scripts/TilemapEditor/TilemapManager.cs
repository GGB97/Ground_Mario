using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    // tile을 배치할 tilemap
    [SerializeField] private Tilemap tilemap;
    // Tile들의 data를 가지고 있는 scriptable object
    [SerializeField] private CustomTilesSO customTilesSO;

    // 타일맵 생성기
    private TilemapGenerator _tilemapGenerator;
    
    // Tile들을 가지고 있는 dictionary
    public Dictionary<string, CustomRuleTile> TileDictionary { get; private set; }

    // tilemap size
    public int worldSizeX = 900;
    public int worldSizeY = 100;
    // 바닥 위치 offset
    public int offsetY = -5;
    public int chunkNum = 9;
    
    private void Start()
    {
        InitTileDict();
        GenerateMap();
        StartDig(GameManager.Instance.player.transform.position.x);
    }

    // customTiles의 값을 dictionary에 추가하는 method
    private void InitTileDict()
    {
        TileDictionary = customTilesSO.customRuleTiles.ToDictionary(tile => tile.GetTileID());
    }

    // 맵 생성 method
    private void GenerateMap()
    {
        _tilemapGenerator = new TilemapGenerator(worldSizeX, worldSizeY, offsetY, tilemap, TileDictionary);
        _tilemapGenerator.InitMapTiles();
        _tilemapGenerator.Generate();
    }

    public void StartDig(float positionX)
    {
        var tiles = Enumerable.Repeat<TileBase>(null, 4).ToList();
        tiles.InsertRange(tiles.Count, customTilesSO.passTiles);
        
        var bounds = new BoundsInt()
        {
            position = new Vector3Int((int)positionX, offsetY - 4, 0),
            size = new Vector3Int(2, 5, 1),
        };
        
        tilemap.SetTilesBlock(bounds, tiles.ToArray());
    }
}
    