using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    PlayerMovement player;
    Camera _camera;
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void OnMove(InputValue value) // 키를 누를 때 1, 땔 때 0
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;

        player._isFlying = moveInput.y > 0.5f;
        if(player._isFlying)
        {
            if (player.flyingDuration > 0)
                moveInput.y *= 1.5f;
            else
                moveInput.y = 0;
        }

        CallMoveEvent(moveInput);
    }
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }
    public void OnFire(InputValue value)
    {
        isAttacking = value.isPressed;
    }
}
