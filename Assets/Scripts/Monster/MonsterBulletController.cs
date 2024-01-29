using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletController : MonoBehaviour
{
     [SerializeField] private LayerMask levelCollisionLayer;
    
    private RangedAttackData _rangedAttackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestroy = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); //나를 포함해 자식까지 검사
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }
   
    void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _rangedAttackData.duration) //시간이 지나면 사라지는거 왜 여깄냐하면 속도 증가하기전에 먼저 검사하는 거임.
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody2D.velocity = _direction * _rangedAttackData.speed;
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * _rangedAttackData.size; //들어있는 사이즈에 따라 투사체 크기가 달라짐.
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))) 
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
        }
        else if (_rangedAttackData.target.value == (_rangedAttackData.target.value | (1<<collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-_rangedAttackData.power);
                if (_rangedAttackData.isInKnockBack)
                {
                    Movement movement = collision.GetComponent<Movement>();
                    if (movement != null)
                    {
                        movement.ApplyKnockback(transform,_rangedAttackData.knockbackPower,_rangedAttackData.knockbackTime);
                    }
                }
                DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
            }
        }
    }


    /// <summary>
    /// 초기화하는 메서드
    /// </summary>
    /// <param name="direction" />
    /// <param name="attackData"></param>
    /// <param name="projectileManager"></param>
    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager) 
    {
        _projectileManager = projectileManager;
        _rangedAttackData = attackData;
        _direction = direction;
        
        UpdateProjectileSprite();
        //_trailRenderer.Clear();
        _currentDuration = 0;
        //_spriteRenderer.color = attackData.projectileColor; 

        transform.right = _direction;
        _isReady = true;
        
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            //_projectileManager.CreateImpactParticlesAtPosition(position,_rangedAttackData);
        }
        gameObject.SetActive(false); 
    }
}
