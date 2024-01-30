using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMonster_Contact : GroundMonsterControllrer
{
    [SerializeField] [Range(0, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Base";
    private bool _isCollidingWithTarget;
    private SpriteRenderer _characterRenderer;

    private MonsterDisapear _monsterDisapear;
    private HealthSystem _healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private Movement _collidingMovement;
    
    private bool isFirst = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isFirst = false;
        _characterRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthSystem = GetComponent<HealthSystem>();
        _monsterDisapear = GetComponent<MonsterDisapear>();
        //_healthSystem.OnDamage += OnDamage;
    }

    private void OnDamage()
    {
        //TODO : 피격 시 빨간색 애니메이션 넣기
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_isCollidingWithTarget) 
        {
            ApplyHealthChange();
            CallMoveEvent(Vector2.zero);
        }
        else
        {
            Vector2 direction = Vector2.zero;
            direction = DirectionToTarget();
            CallMoveEvent(direction);
            Rotate(direction);
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterRenderer.flipX = Mathf.Abs(rotZ) > 90;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        if (_collidingTargetHealthSystem != null)
        {
          _isCollidingWithTarget = true;
        }

        _collidingMovement = receiver.GetComponent<Movement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;
        
        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        _isCollidingWithTarget = false;
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
    
    public void initiallize()
    {
        
        if (!isFirst)
        {
            foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                Color newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
                renderer.color = newColor;   
            }
        
            foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
            {
                component.enabled = true;
            }
            _monsterDisapear.reset();
            _healthSystem.InitializeHealth();
        }
      
    }
}
