using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMonsterControllrer : CharacterController
{
    
    private GameObject _player;
    protected MonsterStatsHandler _monsterStatsHandler;

   protected virtual void Start()
    {
        _player = GameManager.Instance.player;
        _monsterStatsHandler = GetComponent<MonsterStatsHandler>();
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
