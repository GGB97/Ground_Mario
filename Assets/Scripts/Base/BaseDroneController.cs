using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BaseDroneController : CharacterController
{
    private float shootRange = 15f;

    
    [SerializeField] private string enemyTag = "Enemy";
    private Collider2D _closestTarget;

    public LayerMask EnemyLayer;

    private void Start()
    {
        StartCoroutine("CheckTarget");
    }


    private IEnumerator CheckTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            // 범위 안 모두 찾기
            var collisions = Physics2D.OverlapCircleAll(transform.position, shootRange, EnemyLayer);
            float _shortestDistance = float.MaxValue;

            // 최단 거리 찾기
            if (collisions.Length != 0)
            {
                for (int i = 0; i < collisions.Length; i++)
                {
                    if (DistanceToTarget(collisions[i].transform) < _shortestDistance)
                    {
                        _shortestDistance = DistanceToTarget(collisions[i].transform);
                        _closestTarget = collisions[i];
                    }
                }
            }


            if (_closestTarget != null)
            {
                var direction = DirectionToTarget(_closestTarget.transform);
                CallLookEvent(direction);
                isAttacking = true;
            }


        }

    }

  

    private float DistanceToTarget(Transform obj)
    {
        return Vector3.Distance(transform.position, obj.position);
    }

    private Vector2 DirectionToTarget(Transform obj)
    {
        return (obj.position - transform.position).normalized;
    }
}

