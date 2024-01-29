using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDroneController : BaseController
{
    private float followRange = 2f;

    [SerializeField] private string targetTag = "DroneTarget";
    private GameObject _base;
    private void Start()
    {
        _base = GameObject.FindWithTag(targetTag);
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        if (DistanceToBase() > followRange)
        {
            direction = DirectionToBase();
        }
        CallMoveEvent(direction);
    }

    private float DistanceToBase()
    {
        return Vector3.Distance(transform.position, _base.transform.position);
    }

    private Vector2 DirectionToBase()
    {
        return (_base.transform.position - transform.position).normalized;
    }
}
