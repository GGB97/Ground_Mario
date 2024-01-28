using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator
{
    private readonly int _worldSizeX;
    private readonly int _worldSizeY;
    private readonly int _offsetY;

    private readonly Tilemap _tilemap;

    private readonly Dictionary<string, CustomRuleTile> _dictionary;
    private readonly List<string> _terrainTiles;

    private string _unbreakableTile;
    private int _totalWeight;

    public TilemapGenerator(int worldSizeX, int worldSizeY, int offsetY, Tilemap tilemap, Dictionary<string, CustomRuleTile> dictionary)
    {
        _worldSizeX = worldSizeX;
        _worldSizeY = worldSizeY;
        _offsetY = offsetY;
        _tilemap = tilemap;
        _dictionary = dictionary;

        _terrainTiles = new List<string>();
        _totalWeight = 0;
    }

    // TileType에 따라 tileID로 분류
    public void InitMapTiles()
    {
        foreach (var item in _dictionary)
        {
            switch (item.Value.tileType)
            {
                case CustomRuleTile.TileType.Terrain:
                    _terrainTiles.Add(item.Key);
                    _totalWeight += item.Value.weight;
                    break;
                case CustomRuleTile.TileType.Unbreakable:
                    _unbreakableTile = item.Key;
                    break;
            }
        }
    }

    // 맵 생성 코드
    public void Generate()
    {
        var startPointX = -_worldSizeX / 2;
        var endPointX = _worldSizeX / 2;
        var startPointY = _offsetY + 2;
        var endPointY = _offsetY - _worldSizeY;

        for (var x = startPointX; x < endPointX; x++)
        {
            for (var y = startPointY; y >= endPointY; y--)
            {
                // 끝 부분은 막기 위해 unbreakableTile을 생성
                if (y > startPointY - 2 || y == endPointY)
                    _tilemap.SetTile(new Vector3Int(x, y, 0), GetUnbreakableTile());
                else
                    _tilemap.SetTile(new Vector3Int(x, y, 0), PickTile());
            }
        }
    }

    // 각 Tile에 저장되어 있는 weight에 따라 랜덤으로 tile 생성
    private CustomRuleTile PickTile()
    {
        var random = Random.Range(0, _totalWeight);

        foreach (var key in _terrainTiles)
        {
            // 예외처리
            if (!_dictionary.TryGetValue(key, out var customTile))
                continue;

            if (customTile.weight > random)
                return customTile;

            random -= customTile.weight;
        }

        return null;
    }

    private CustomRuleTile GetUnbreakableTile()
    {
        return !_dictionary.TryGetValue(_unbreakableTile, out var customTile) ? null : customTile;
    }
}

