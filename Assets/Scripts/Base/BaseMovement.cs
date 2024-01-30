using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public static BaseMovement Instance;

    private CharacterController _controller;
    private CharStatsHandler _stats;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public float dashDelay = 5f;
    private bool IsDash = false;
    public bool IsDashable = true;

    private void Awake()
    {
        Instance = this;
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<CharStatsHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsDashable)
        {
            Dash();
        }
        // 더블탭이 아니어도 대쉬가 됨 -> 밸류를 버튼으로 바꿔서 해결
        if (IsDash)
        {
            ApplyMovment(_movementDirection * 20);
            // 밸류를 버튼으로 바꿧더니 대쉬가 안됨
            //_rigidbody.AddForce(forcePower * _movementDirection.x, 0);
            IsDash = false;
            StartCoroutine(DashDelay());
        }
        else if (!IsDash)
        {
            ApplyMovment(_movementDirection);
        }

    }

    private void OnDirection(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x != 0)
            _spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
        OnDirection(_movementDirection);
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * _stats.CurrentStates.speed;
        _rigidbody.velocity = direction;
    }
    private void Dash()
    {
        IsDash = true;
        IsDashable = false;
    }

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDelay);
        IsDashable = true;
    }

}
