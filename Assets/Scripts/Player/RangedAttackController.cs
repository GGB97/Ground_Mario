using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] LayerMask levelCollisionLayer;

    float _currentDuration;
    Vector2 _direction;
    bool _isReady;

    Rigidbody2D _rigidbody;
    Animator _animator;

    public bool fxOnDestroy = true;

    float duration = 4f;
    float speed = 10f;

    private void Awake()
    {
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

        if(_currentDuration > duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * speed;
    }

    public void InitializeAttack(Vector2 direction)
    {
        _direction = direction;

        _currentDuration = 0;

        transform.right = direction;

        _isReady = true;

        if (_animator != null)
            _animator.SetBool("IsHit", false);

        speed = 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            if (_animator != null)
                _animator.SetBool("IsHit", true);

            speed = 0;
        }
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
}
