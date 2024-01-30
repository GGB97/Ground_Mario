using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseAnimationController : PlayerAnimationController
{
    static readonly int Death = Animator.StringToHash("Death");
    [SerializeField] SpriteRenderer weaponSprite;
    Animator basAnimatro;
    protected override void Awake()
    {
        base.Awake();
        healthSystem = GetComponentInChildren<HealthSystem>();
        basAnimatro = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        healthSystem.OnDeath += OnDeath;
    }

    protected void OnDeath()
    {
        GetComponent<PlayerInput>().enabled = false;
        weaponSprite.enabled = false;
        GameManager.Instance.CallStateChangeEvent(GameState.Fail);
        basAnimatro.SetTrigger(Death);
    }
}
