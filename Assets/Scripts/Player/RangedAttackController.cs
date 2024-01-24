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

    public bool fxOnDestroy = true;

    float duration = 4f;
    float speed = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
}
