using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GroundMonster_Bomb : GroundMonsterControllrer
{
    [SerializeField] [Range(0, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool _isCollidingWithTarget;
    private SpriteRenderer _characterRenderer;

    private HealthSystem _healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private Movement _collidingMovement;

    private bool ready=false;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _characterRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDamage += OnDamage;
        OnAttackEvent += Bomb;
    }

    private void OnDamage()
    {
        followRange = 100f;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

       
        if (!ready)
        {
            Vector2 direction = Vector2.zero;
            if (Getdistance() < followRange)
            {
                direction = DirectionToTarget();
            }
        
            CallMoveEvent(direction);
            Rotate(direction);
        }
        
        
    }

    private void Bomb(AttackSO attackSO)
    {
        ready = true;
        CallMoveEvent(Vector2.zero);
        StartCoroutine("Getbomb");
    }

    IEnumerator Getbomb()
    {
        bool hasdamage = false;
        yield return new WaitForSeconds(1f);
        int layermask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player")
            {
                _collidingTargetHealthSystem = colliders[i].GetComponent<HealthSystem>();
                if (_collidingTargetHealthSystem == null)
                {
                    break;
                }
                else
                {
                    hasdamage = true;    
                }
            }
        }
        if (hasdamage) 
        {
            ApplyHealthChange();
        }
        gameObject.SetActive(false);
    }
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterRenderer.flipX = Mathf.Abs(rotZ) < 90;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag))
        {
            return;
        }
        CallAttackEvent(_Stats.CurrentStates.attackSO); // CallAttackEvent(1) 이거에서 수정함.

        //_collidingMovement = receiver.GetComponent<Movement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;
        
        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        //_isCollidingWithTarget = false;
    }

    private void ApplyHealthChange()
    {
        AttackSO attackSo =  _monsterStatsHandler.CurrentStates.attackSO;
        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attackSo.power);
        if (attackSo.isInKnockBack && _collidingMovement != null)
        {
            _collidingMovement.ApplyKnockback(transform,attackSo.knockbackPower,attackSo.knockbackTime);
        }
    }
}
