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
    protected MonsterStatsHandler _MonsterStatsHandler { get; private set; }

    protected virtual void Awake()
    {
        _MonsterStatsHandler = GetComponent<MonsterStatsHandler>();
    }
    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    void HandleAttackDelay()
    {
        //if (Stats.CurrentStats.attackSO == null)
        //    return;

        if (_timeSinceLastAttact <= _MonsterStatsHandler.CurrentStates.attackSO.delay)
        {
            _timeSinceLastAttact += Time.deltaTime;
        }
        if(isAttacking && _timeSinceLastAttact > _MonsterStatsHandler.CurrentStates.attackSO.delay)
        {
            _timeSinceLastAttact = 0f;
            CallAttackEvent(_MonsterStatsHandler.CurrentStates.attackSO.delay);
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
