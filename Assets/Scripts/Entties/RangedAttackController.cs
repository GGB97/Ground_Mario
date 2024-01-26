using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] LayerMask levelCollisionLayer;

    RangedAttackData _rangedAttackData;
    float _currentDuration;
    Vector2 _direction;
    bool _isReady;

    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    ProjectileManager _projectileManager;

    public bool fxOnDestroy = true; // 충돌 후 파티클 터질건지 였는데 -> hit 애니메이션을 재생할건지로 사용중

    private void Awake()
    {

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _rangedAttackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _rangedAttackData.speed;
    }

    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _projectileManager = projectileManager;
        _rangedAttackData = attackData;
        _direction = direction;

        UpdateProjectileSprite();
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        transform.right = direction;

        _isReady = true;
    }

    void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * _rangedAttackData.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
        }
        else if (_rangedAttackData.target.value == (_rangedAttackData.target.value | (1 << collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-_rangedAttackData.power);
                if (_rangedAttackData.isInKnockBack)
                {
                    Movement movement = collision.GetComponent<Movement>();
                    if (movement != null)
                    {
                        movement.ApplyKnockback(transform, _rangedAttackData.knockbackPower, _rangedAttackData.knockbackTime);
                    }
                }
                DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
            }
        }
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            if (_animator != null)
            {
                _direction = Vector2.zero;
                _animator.SetBool("IsHit", true);
            }
        }
        else
            gameObject.SetActive(false);
    }
}
