using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDroneController : CharacterController
{
    private float followRange = 2f;

    [SerializeField] private string spawnTag = "DroneTarget";
    private GameObject _base;

    [SerializeField] private string enemyTag = "Enemy";
    private GameObject enemyTarget;

    private CharStatsHandler charStatsHandler;

    private void Start()
    {
        _base = GameObject.FindWithTag(spawnTag);
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        if (DistanceToTarget(_base.transform) > followRange)
        {
            direction = DirectionToTarget(_base.transform);
        }
        CallMoveEvent(direction);

        enemyTarget = GameObject.FindWithTag(enemyTag);
    }

    private float DistanceToTarget(Transform obj)
    {
        return Vector3.Distance(transform.position, obj.position);
    }

    private Vector2 DirectionToTarget(Transform obj)
    {
        return (obj.position - transform.position).normalized;
    }

    private void GetTarget()
    {
        enemyTarget = GameObject.FindWithTag(enemyTag);
    }
}
