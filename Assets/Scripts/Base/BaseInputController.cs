using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseInputController : BaseController
{
    private Camera _camera;
    public bool IsDash = false;
    public bool IsDashable = true;

    // 나중에 바꾸기 - 캐릭터<>기지
    private void Awake()
    {
        _camera = Camera.main;
    }


    public void OnMove(InputValue value)
    {
        if (!IsDash)
        {
            Vector2 moveInput = value.Get<Vector2>().normalized;
            CallMoveEvent(moveInput);
        }
        
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

    public void OnDash(InputValue value)
    {
        if (IsDashable)
        {
            Vector2 dashInput = value.Get<Vector2>().normalized;
            CallDashEvent(dashInput);
        }
    }
}
