using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    ProjectileManager _projectileManager;
    CharacterController _controller;
    CharStatsHandler _stats;

    [SerializeField] Transform projectileSpawnPos;
    Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<CharStatsHandler>();
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

    void OnShoot(AttackSO attackSO)
    {
        RangedAttackData rangedAttackData = _stats.CurrentStates.attackSO as RangedAttackData;
        float projecttilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = rangedAttackData.numberofPorjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projecttilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngel;

        float angle; float randomSpread;
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            angle = minAngle + projecttilesAngleSpace * i;
            randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackData, angle);
        }
    }

    void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        Debug.Log("debug");
        _projectileManager.ShootBullet(
            projectileSpawnPos.position, // 발사 위치
            RotateVector2(_aimDirection, angle), // 회전각
            rangedAttackData // 발사 정보
            );
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            //파티클 생성
            //_projectileManager.CreateImpactParticleesAtPosition(position, _attackData);
        }

        gameObject.SetActive(false);
    }

    static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
