using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster_Movement : MonoBehaviour
{
    CharacterController _controller;

    Vector2 _moventDirection = Vector2.zero;
    Rigidbody2D _rigidbody;

    Vector2 _knockback = Vector2.zero;
    float knockbackDuration = 0;
    private MonsterStatsHandler _monsterStatsHandler; 
    [SerializeField] private Transform[] SkyWay;
    Transform newdes;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _monsterStatsHandler = GetComponent<MonsterStatsHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
        newdes = SkyWay[Random.Range(0, SkyWay.Length)];
    }

    private void FixedUpdate()
    {
        //ApplyMovent(_moventDirection);
    }

    void Move(Vector2 direction)
    {
        newdes = GetDestination();
    }

    IEnumerator GetMoving(Transform transform)
    {
        if (Mathf.Abs(transform.position.x - this.transform.position.x) > 1f)
        {
            this.transform.Translate(transform.position.x*Time.deltaTime,transform.position.y*Time.deltaTime,0);
            yield return null;
        }
        else
        {
            yield break;
        }
        
            
            
    }

    // void ApplyMovent(Vector2 direction)
    // {
    //     direction = direction * _monsterStatsHandler.CurrentStates.speed;
    //   
    //     _rigidbody.velocity = direction;
    // }
    
    protected Transform GetDestination()
    {
        if (newdes == SkyWay[0])
            newdes = SkyWay[1];
        else newdes = SkyWay[0];
        return newdes;
    }
}
