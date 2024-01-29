using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMonser_Range : GroundMonsterControllrer
{
    [SerializeField] [Range(0, 100f)] private float AttackRange;
    [SerializeField] private string targetTag = "Player";
    private SpriteRenderer _characterRenderer;

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
        //_healthSystem.OnDamage += OnDamage;
    }


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        isAttacking = false;
        Vector2 direction = Vector2.zero;
        direction=DirectionToTarget();
        if (Getdistance() < AttackRange)
        {
            Rotate(direction);
            CallMoveEvent(Vector2.zero);
            CallLookEvent(direction);
            isAttacking = true;
        }
        else
        {
            CallMoveEvent(direction);
            Rotate(direction);    
        }
        
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterRenderer.flipX = Mathf.Abs(rotZ) < 90;
    }
   
    public void initiallize()
    {
        if (!isFirst)
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
        
            _healthSystem.InitializeHealth();    
        }
    }
}