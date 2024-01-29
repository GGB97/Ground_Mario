using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DefaultTilesData",menuName = "Tiles/Default", order = 0)]
public class CustomTilesSO : ScriptableObject
{
    [Header("Tile Data List")] public List<CustomRuleTile> customRuleTiles;
    [Header("Pass Tile Data List")] public List<TileBase> passTiles;
}
