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

    [SerializeField] List<GameObject> coinPrefabs; // enum CoinValue 순서대로 넣어줘야함

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
        StartDig();
    }

    // customTiles의 값을 dictionary에 추가하는 method
    private void InitTileDict()
    {
        TileDictionary = customTilesSO.customRuleTiles.ToDictionary(tile => tile.GetTileID().ToString());
    }

    // 맵 생성 method
    private void GenerateMap()
    {
        _tilemapGenerator = new TilemapGenerator(worldSizeX, worldSizeY, offsetY, tilemap, TileDictionary);
        _tilemapGenerator.InitMapTiles();
        _tilemapGenerator.Generate();
    }

    // 기지에서 플레이어 전환하면서 땅 팔 때, 토관 생성.
    public void StartDig()
    {
        var positionX = GameManager.Instance.playerBase.transform.position.x - 1;

        if (IsPassTile(positionX) || IsPassTile(positionX + 1))
            return;
        
        var tiles = Enumerable.Repeat<TileBase>(null, 4).ToList();
        tiles.AddRange(customTilesSO.passTiles);

        var bounds = new BoundsInt()
        {
            position = new Vector3Int((int)positionX, offsetY - 4, 0),
            size = new Vector3Int(2, 5, 1),
        };

        tilemap.SetTilesBlock(bounds, tiles.ToArray());
    }
    
    private bool IsPassTile(float positionX)
    {
        return tilemap.GetTile<CustomRuleTile>(new Vector3Int((int)positionX, offsetY, 0)).GetTileID() == TileID.pass;
    }

    public void DestroyTarget(Vector3Int target)
    {
        TileID tileId = tilemap.GetTile<CustomRuleTile>(target).GetTileID();
        int coinValue = CalculateCoinValue(tileId);

        tilemap.SetTile(target, null);

        if (coinValue != (int)CoinValue.NoValue)  // 프리팹 생성
            Instantiate(coinPrefabs[(int)coinValue], new Vector3(target.x + .5f, target.y + .5f, target.z), Quaternion.identity);
        
    }

    private int CalculateCoinValue(TileID tileId)
    {
        int rand = Random.Range(0, 5);

        switch (tileId)
        {
            case TileID.reward:
                return (rand < 4) ? (int)CoinValue.Coin_10 : (int)CoinValue.Coin_30;
            case TileID.chest:
                return (rand < 3) ? (int)CoinValue.Coin_30 : (int)CoinValue.Coin_50;
            default:
                return (int)CoinValue.NoValue; // 코인 가치가 없는 경우
        }
    }
}
