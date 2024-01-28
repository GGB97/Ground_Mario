using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    //TODO 다른 저장 방법으로 불러오는 걸로 구현.
    [SerializeField] private List<CustomRuleTile> customTiles;

    // Tile들을 가지고 있는 dictionary
    public Dictionary<string, CustomRuleTile> TileDictionary { get; private set; }

    // tilemap size
    public int worldSizeX = 900;
    public int worldSizeY = 100;
    // 바닥 위치 offset
    public int offsetY = -7;
    
    private void Start()
    {
        InitTileDict();
        GenerateMap();
    }

    // customTiles의 값을 dictionary에 추가하는 method
    private void InitTileDict()
    {
        TileDictionary = customTiles.ToDictionary(tile => tile.GetTileID());
    }

    // 맵 생성 method
    private void GenerateMap()
    {
        var tilemapGenerator = new TilemapGenerator(worldSizeX, worldSizeY, offsetY, tilemap, TileDictionary);
        tilemapGenerator.InitMapTiles();
        tilemapGenerator.Generate();
    }
}
    