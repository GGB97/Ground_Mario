using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Movement : MonoBehaviour
{
    CharacterController _controller;

    Vector2 _moventDirection = Vector2.zero;
    Rigidbody2D _rigidbody;

    Vector2 _knockback = Vector2.zero;
    float knockbackDuration = 0;
    private CharStatsHandler _monsterStatsHandler; 

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _monsterStatsHandler = GetComponent<CharStatsHandler>();
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

    void Move(Vector2 direction)
    {
        _moventDirection = direction;
    }

    void ApplyMovent(Vector2 direction)
    {
        direction = direction * _monsterStatsHandler.CurrentStates.speed;
        if(knockbackDuration > 0)
        {
            direction += _knockback;
        }
        _rigidbody.velocity = direction;
    }
}