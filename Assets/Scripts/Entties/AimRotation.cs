using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer armRenderer;
    [SerializeField] protected Transform armPivot;

    [SerializeField] protected SpriteRenderer charRenderer;

    protected CharacterController _controller;

    protected void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    protected void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDiredction)
    {
        RotateArm(newAimDiredction);
    }

    protected virtual void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        charRenderer.flipX = armRenderer.flipY;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
