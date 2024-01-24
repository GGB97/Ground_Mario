using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    ProjectileManager _projectileManager;
    CharacterController _controller;

    //[SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPos;
    Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
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

    private void OnShoot(float obj)
    {
        CreateProjectile();
    }

    void CreateProjectile()
    {
        _projectileManager.ShootBullet(projectileSpawnPos.position, _aimDirection);
    }
}
