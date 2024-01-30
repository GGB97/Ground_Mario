using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : PlayerAnimations
{
    static readonly int IsWalking = Animator.StringToHash("IsWalking");
    static readonly int Attack = Animator.StringToHash("Attack");
    static readonly int Hit = Animator.StringToHash("Hit");

    [SerializeField] bool isBase;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentsInChildren<Animator>();
        if (healthSystem != null)
        {
            healthSystem.OnDamage += damaged;
            healthSystem.OnInvincibilityEnd += InvinciblilityEnd;
        }
            
        controller.OnMoveEvent += Move;
        if (!isBase)
            controller.OnAttackEvent += Attacking;
    }

    private void damaged()
    {
        foreach (var anim in animator)
            anim.SetBool(Hit,true);
    }
    
    private void InvinciblilityEnd()
    {
        foreach (var anim in animator)
            anim.SetBool(Hit,false);
    }

    private void Move(Vector2 obj)
    {
        foreach (var anim in animator)
            anim.SetBool(IsWalking, obj.magnitude > 0.5f);
    }

    private void Attacking(AttackSO notuse)
    {
        foreach (var anim in animator)
            anim.SetTrigger(Attack);
    }
}
