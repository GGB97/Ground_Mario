using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileID
{
    unbreakable,
    soil,
    invisible,
    chest,
    reward
}
public enum TileType
{
    Terrain,
    Invisible,
    Unbreakable,
}

public enum CoinValue
{
    Coin_10,
    Coin_30,
    Coin_50,
    NoValue = 99
}

[CreateAssetMenu]
public class CustomRuleTile : RuleTile
{
    [SerializeField] private TileID tileID;
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

    public TileID GetTileID()
    {
        return tileID;
    }
}