using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;

    ObjectPool objectPool;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    public void ShootBullet(Vector2 startPos, Vector2 dir)
    {
        GameObject obj = objectPool.SpawnFromPool("Arrow");

        obj.transform.position = startPos;
        RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        attackController.InitializeAttack(dir);

        obj.SetActive(true);
    }
}
