using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingMonster_Range : FlyingMonsterContorller
{
    [SerializeField] [Range(0, 100f)] private float AttackRange;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] [Range(0, 10)] private float movingTime;
    private float currenttime;
    private SpriteRenderer _characterRenderer;

    private HealthSystem _healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private Movement _collidingMovement;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _characterRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthSystem = GetComponent<HealthSystem>();
        currenttime = movingTime;
        //_healthSystem.OnDamage += OnDamage;
        StartCoroutine("checkTime");
    }


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        
        base.FixedUpdate();
        
    }

    
    IEnumerator checkTime() //시간 체크를 위한 FixedUpdate 를 만든 것
    {
        while (true)
        {
            currenttime += Time.fixedDeltaTime;
            isAttacking = false;
            Vector2 direction = Vector2.zero;
            direction=DirectionToTarget();
            if (currenttime > movingTime)
            {
                CallMoveEvent(direction);
                Rotate(direction);
                currenttime = 0;
            }
            if (Getdistance() < AttackRange)
            {
                Rotate(direction);
                CallLookEvent(direction);
                isAttacking = true;
            }
            
            yield return new WaitForFixedUpdate(); //주기는 픽스트업데이트에 맞춰서
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterRenderer.flipX = Mathf.Abs(rotZ) < 90;
    }

    private void ApplyHealthChange()
    {
        AttackSO attackSo = _monsterStatsHandler.CurrentStates.attackSO;
        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attackSo.power);
        if (attackSo.isInKnockBack && _collidingMovement != null)
        {
            _collidingMovement.ApplyKnockback(transform, attackSo.knockbackPower, attackSo.knockbackTime);
        }
    }
    
    public void initiallize()
    {
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 255);
            renderer.color = newColor;   
        }
        
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = true;
        }
    }
}
