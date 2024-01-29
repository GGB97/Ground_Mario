using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapIterator
{
    private readonly Tilemap _tilemap;
    private TileBase[] _nullTileArray;
    private BoundsInt _worldBounds;
    private BoundsInt _chunkBounds;

    public TilemapIterator(int worldSizeX, int worldSizeY, int offsetY, int chunkNum, Tilemap tilemap)
    {
        _tilemap = tilemap;

        _worldBounds = new BoundsInt()
        {
            position = new Vector3Int(-worldSizeX / 2, offsetY, 0),
            size = new Vector3Int(worldSizeX, worldSizeY, 1),
        };
        
        _chunkBounds = new BoundsInt()
        {
            size = new Vector3Int(worldSizeX / chunkNum, worldSizeY, 1),
        };

        _nullTileArray = Enumerable.Repeat<TileBase>(null, _chunkBounds.size.x * _chunkBounds.size.y).ToArray();
    }

    public void MoveChunk(bool moveRight)
    {
        Vector3Int startPos;
        Vector3Int endPos;
        
        startPos = new Vector3Int(
            _worldBounds.position.x + (moveRight ? 0 : _worldBounds.size.x - _chunkBounds.size.x),
            _worldBounds.position.y,
            0);

        endPos = new Vector3Int(
            _worldBounds.position.x + (moveRight ? _worldBounds.size.x : -_chunkBounds.size.x),
            _worldBounds.position.y,
            0);
        
        SetTiles(startPos, endPos);

        _worldBounds.position = new Vector3Int(
            _worldBounds.position.x + (moveRight ? _chunkBounds.size.x : -_chunkBounds.size.x),
            _worldBounds.position.y, 0);
    }

    private void SetTiles(Vector3Int startPos, Vector3Int endPos)
    {
        _chunkBounds.position = startPos;

        var tileArray = _tilemap.GetTilesBlock(_chunkBounds);
        _tilemap.SetTilesBlock(_chunkBounds, _nullTileArray);

        _chunkBounds.position = endPos;
        _tilemap.SetTilesBlock(_chunkBounds, tileArray);
    }
}
