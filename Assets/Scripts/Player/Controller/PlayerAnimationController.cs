using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : PlayerAnimations
{
    static readonly int IsWalking = Animator.StringToHash("IsWalking");
    static readonly int Attack = Animator.StringToHash("Attack");
    static readonly int Hit = Animator.StringToHash("Hit");

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponentsInChildren<Animator>();
        if (healthSystem != null)
        {
            healthSystem.OnDamage += damaged;
            healthSystem.OnInvincibilityEnd += InvinciblilityEnd;
        }

            
        controller.OnMoveEvent += Move;
    }

    protected void damaged()
    {
        foreach (var anim in animator)
            anim.SetBool(Hit,true);
    }
    
    protected void InvinciblilityEnd()
    {
        foreach (var anim in animator)
            anim.SetBool(Hit,false);
    }

    protected void Move(Vector2 obj)
    {
        foreach (var anim in animator)
            anim.SetBool(IsWalking, obj.magnitude > 0.5f);
    }

    //private void Attacking(AttackSO notuse) // 아직 공격 애니메이션 없음.
    //{
    //    foreach (var anim in animator)
    //        anim.SetTrigger(Attack);
    //}
}
