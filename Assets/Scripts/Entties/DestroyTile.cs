using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    public Vector3Int targetPos;

    public void DestroyTarget()
    {
        GameManager.Instance.tilemapManager.DestroyTarget(targetPos);
    }
}
