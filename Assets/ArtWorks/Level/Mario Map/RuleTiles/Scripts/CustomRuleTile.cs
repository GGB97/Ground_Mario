using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CustomRuleTile : RuleTile
{
    public enum TileType
    {
        Terrain,
        Invisible,
        Unbreakable,
    }
    
    [SerializeField] private string tileID;
    public TileType tileType;
    public bool isBreakable;
    public int weight;
    public float hardness;
    
    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        switch (neighbor)
        {
            case TilingRule.Neighbor.This:
                return other is CustomRuleTile;
            case TilingRule.Neighbor.NotThis:
                return other is not CustomRuleTile;
        }

        return base.RuleMatch(neighbor, other);
    }

    public string GetTileID()
    {
        return tileID;
    }
}