using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected CharacterController _controller;

    protected Vector2 _moventDirection = Vector2.zero;
    protected Rigidbody2D _rigidbody;

    protected Vector2 _knockback = Vector2.zero;
    protected float knockbackDuration = 0;
    protected CharStatsHandler _stats;

    protected virtual void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _stats = GetComponent<CharStatsHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovent(_moventDirection);
        if(knockbackDuration > 0)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }                                             
    }
    
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power; // �˹� ����
    }

    protected virtual void Move(Vector2 direction)
    {
        _moventDirection = direction;
    }

    void ApplyMovent(Vector2 direction)
    {
        direction = direction * _stats.CurrentStates.speed;
        if(knockbackDuration > 0)
        {
            direction += _knockback;
        }
        _rigidbody.velocity = direction;
    }
}
