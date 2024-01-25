using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    private BaseController _controller;
    private BaseInputController _inputController;

    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 forcePower = new Vector2 (50, 0);
    private Rigidbody2D _rigidbody;

    private float dashDelay = 5f;
    private bool IsDash = false;
    private bool IsDashable = true;

    private void Awake()
    {
        _controller = GetComponent<BaseController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputController = GetComponent<BaseInputController>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
        _controller.OnDashEvent += Dash;
    }

    private void FixedUpdate()
    {
        // 더블탭이 아니어도 대쉬가 됨 -> 밸류를 버튼으로 바꿔서 해결
        if (_inputController.IsDash)
        {
            Debug.Log(_movementDirection);
            ApplyMovment(_movementDirection * 20);
            // 밸류를 버튼으로 바꿧더니 대쉬가 안됨
            //_rigidbody.AddForce(forcePower * _movementDirection.x, 0);
            Debug.Log("dash");
            //StartCoroutine(DashDelay());
            _inputController.IsDash = false;
            _inputController.IsDashable = true;
        }
        else if (!_inputController.IsDash)
        {
            ApplyMovment(_movementDirection);
        }
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * 5;

        _rigidbody.velocity = direction;
    }
    private void Dash(Vector2 direction)
    {
        //if (IsDashable)
        //    IsDash = true;
        _inputController.IsDash = true;
        _inputController.IsDashable = false;
    }

    private IEnumerator DashDelay()
    {
        IsDash = false;
        IsDashable = false;
        yield return new WaitForSeconds(dashDelay);
        Debug.Log("dashbale");
        IsDashable = true;
    }
}
