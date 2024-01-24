using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : PlayerAnimations
{
    static readonly int IsWalking = Animator.StringToHash("IsWalking");
    static readonly int Attack = Animator.StringToHash("Attack");

    Rigidbody2D _rigidbody;
    float absVelY;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        controller.OnMoveEvent += Move;
        controller.OnAttackEvent += Attacking;
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > 0.5f);   
    }

    private void Attacking(float notUse)
    {
        animator.SetTrigger(Attack);
    }
}
