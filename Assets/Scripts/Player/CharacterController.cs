using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<float> OnAttackEvent;

    float attackDelay = .2f;
    float _timeSinceLastAttact = float.MaxValue;
    protected bool isAttacking {  get; set; }

    protected virtual void Awake()
    {

    }
    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    void HandleAttackDelay()
    {
        //if (Stats.CurrentStats.attackSO == null)
        //    return;

        if (_timeSinceLastAttact <= attackDelay)
        {
            _timeSinceLastAttact += Time.deltaTime;
        }
        if(isAttacking && _timeSinceLastAttact > attackDelay)
        {
            _timeSinceLastAttact = 0f;
            CallAttackEvent(attackDelay);
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
       OnMoveEvent?.Invoke(direction);
    }
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallAttackEvent(float delay)
    {
        OnAttackEvent?.Invoke(delay);
    }
}
