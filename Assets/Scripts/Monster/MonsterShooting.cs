using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterShooting : MonoBehaviour
{
    ProjectileManager _projectileManager;
    CharacterController _controller;
    private CharStatsHandler _monsterStatsHandler;

    //[SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPos;
    Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _monsterStatsHandler = GetComponent<CharStatsHandler>();
    }

    private void Start()
    {
        _projectileManager = ProjectileManager.instance;

        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO notuse)
    {
        RangedAttackData rangedAttackData = _monsterStatsHandler.CurrentStates.attackSO as RangedAttackData;
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = rangedAttackData.numberofPorjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f*rangedAttackData.multipleProjectilesAngel; 
        //10개만큼 쏜다고하면 각도를 MAX 반대쪽으로 꺾어놓는것
        
        for (int i = 0; i < rangedAttackData.spread; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread); 
            angle += randomSpread;
            CreateProjectile(rangedAttackData,angle);    
        }
    }

    private void CreateProjectile(RangedAttackData rangedAttackData,float angle)
    {
        _projectileManager.ShootBullet(projectileSpawnPos.position,RotateVector2(_aimDirection,angle),rangedAttackData);
        //에임으로 어디다 쏘는지는 알지만 벡터를 모르기때문에 바꿔줘야됨.
       
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            //��ƼŬ ����
            //_projectileManager.CreateImpactParticleesAtPosition(position, _attackData);
        }

        gameObject.SetActive(false);
    }
    private static Vector2 RotateVector2(Vector2 v, float degree) //벡터를 회전시키려면 쿼터니언 * 벡터를 해야됨.
    {
        return Quaternion.Euler(0, 0, degree) * v; //v를 회전시키는 모양새, 굉장히 자주 사용되기때문에 기억해두면 좋음
    }
}