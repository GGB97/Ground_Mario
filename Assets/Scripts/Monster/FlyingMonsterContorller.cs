using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsterContorller : CharacterController
{
    private GameObject _player;
    protected CharStatsHandler _monsterStatsHandler;
   
    protected virtual void Start()
    {
        _player = GameManager.Instance.player;
        _monsterStatsHandler = GetComponent<CharStatsHandler>();
       
    }
   
    
    protected virtual void FixedUpdate()
    {
        
    }

    protected float Getdistance()
    {
        return Vector3.Distance(transform.position, _player.transform.position);
    }
    
   
    protected Vector2 DirectionToTarget()
    {
        return (_player.transform.position - transform.position).normalized; //바라보는 방향이 나옴.
    }
}
