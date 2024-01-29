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

    // public void ShootBullet(Vector2 startPos, Vector2 dir)
    // {
    //     GameObject obj = objectPool.SpawnFromPool("Projectile");
    //
    //     obj.transform.position = startPos;
    //     RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
    //     attackController.InitializeAttack(dir,attackData);
    //
    //     obj.SetActive(true);
    // }
    
    public void ShootBullet(Vector2 startPos, Vector2 dir,RangedAttackData attackData)
    {
        GameObject obj = objectPool.SpawnFromPool(attackData.bulletNameTag);

        obj.transform.position = startPos;
        RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        attackController.InitializeAttack(dir,attackData,this);

        obj.SetActive(true);
    }
}
